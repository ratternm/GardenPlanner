using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Capstone.Web.DAL;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private IDatabaseSvc _db = null;

        public HomeController(IDatabaseSvc db)
        {
            _db = db;
            
        }

        // GET: Home
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public ActionResult Register(User model)
        {
            User user = _db.GetUser(model.Email);
            ActionResult result;
            if (!ModelState.IsValid)
            {
                result = View("Register");
            }
            else if(user.Email != null)
            {
                TempData["Failure"] = "Email is already in use.";
                result = View("Register", model);
            }
            else
            {
                DbManager newDb = new DbManager(model.Password);
                model.Salt = newDb.Salt;
                model.Password = newDb.Hash;
                model.RoleId = 2;
                if(_db.AddUser(model) == 1)
                {

                    TempData["Success"] = "Registration successful!";
                    result = RedirectToAction("Login", TempData["Success"]);
                }
                else
                {
                    result = View("Register");
                }
            }
            return result;
        }

        public ActionResult RegisterResult()
        {
            return View();
        }
        

        //Took out User model parameter
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            //Check to make sure User has input required information
            if(!ModelState.IsValid)
            {
                return View("Login", login);
            }

            //Attempt to pull user info from database using input
            User user = _db.GetUser(login.EmailAddress);

            //Check to make sure Email input exists in the database
            if (user.Email == null)
            {
              
                    ModelState.AddModelError("EmailAddress", "Email address not found.");
              
                return View("Login", login);
            }

            //Hash user's input with stored salt and verify passwords match
            DbManager dbM = new DbManager(login.Password, user.Salt);
            var hash = dbM.Hash;
            if (user.Password != hash || user.Salt == null)
            {
                ModelState.AddModelError("Password", "Password does not match our records.");
                return View("Login", login);
            }

            //If the user's information is verified
            else if(user.Password == hash)
            {

                //Store user information in session
                FormsAuthentication.SetAuthCookie(user.Email, true);
                Session[SessionKeys.EmailAddress] = user.Email;
                Session[SessionKeys.RoleId] = user.RoleId;
                Session[SessionKeys.UserId] = user.Id;

                //Check to see if user has created a garden, 
                //If it doesn't exist, redirect to Garden Creation
                var checkGarden = _db.GetUsersGarden(user.Id);
                if(checkGarden.Name == null)
                {
                    return RedirectToAction("CreateGarden");
                }
                //Check Role Id for site permissions
                if((int)Session[SessionKeys.RoleId] == 1)
                {
                    //Admin Login
                    ProfileUpdate profileUpdate = new ProfileUpdate();
                    profileUpdate.User = user;
                    profileUpdate.Garden = _db.GetUsersGarden(Convert.ToInt32(Session[SessionKeys.UserId]));
                    return RedirectToAction("GardenDetail", profileUpdate);
                }
                else if((int)Session[SessionKeys.RoleId] == 2)
                {
                    
                    //Gardener Login

                    ProfileUpdate profileUpdate = new ProfileUpdate();
                    profileUpdate.User = user;
                    profileUpdate.Garden = _db.GetUsersGarden(Convert.ToInt32(Session[SessionKeys.UserId]));
                    return RedirectToAction("GardenDetail");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            //Clear SessionKeys and log out

            FormsAuthentication.SignOut();
            Session.Remove(SessionKeys.EmailAddress);
            Session.Remove(SessionKeys.RoleId);
            Session.Remove(SessionKeys.UserId);

            return RedirectToAction("Login");
        }

        public ActionResult ProfilePage()
        {
            //Verify that user has logged in by checking for session key
            if (Session[SessionKeys.UserId] == null)
            {
                return RedirectToAction("Login");
            }

            //Retrieve user information and pass it to the view
            User user = _db.GetUser(Session[SessionKeys.EmailAddress] as string);
            ProfileUpdate passUser = new ProfileUpdate();
            passUser.User = user;
            int userId = Convert.ToInt32(Session[SessionKeys.UserId]);
            passUser.Garden = _db.GetUsersGarden(userId);

            ////Check to return Admin Profile
            //if(Convert.ToInt32(Session[SessionKeys.RoleId]) == 1)
            //{
            //    passUser.Plants.PlantList = _db.GetPlants();
            //}

            return View("ProfilePage", passUser);
        }

        [HttpPost]
        public ActionResult ProfilePage(ProfileUpdate changes)
        {
            if (Convert.ToInt32(Session[SessionKeys.UserId]) < 1)
            {
                return RedirectToAction("Login");
            }
            if (string.IsNullOrWhiteSpace(changes.User.Email)|| string.IsNullOrWhiteSpace(changes.User.FirstName)
                || string.IsNullOrWhiteSpace(changes.User.LastName)
                || string.IsNullOrWhiteSpace(changes.Garden.Name))
            {
                TempData["Failure"] = "Fields cannot be blank.";
                return RedirectToAction("ProfilePage", changes);
            }

            if(changes.Garden.TempLow > changes.Garden.TempHigh)
            {
                TempData["Failure"] = "Invalid temperature range. High temp must be greater than low temp.";
                return RedirectToAction("ProfilePage", changes);
            }
        
            if(changes.Garden.TempHigh - changes.Garden.TempLow < 5)
            {
                TempData["Failure"] = "Invalid temperature range. Difference must be greater than 5";
                return RedirectToAction("ProfilePage", changes);
            }
            else 
            {
                
                changes.User.Id = Convert.ToInt32(Session[SessionKeys.UserId]);
                //Update user information in database
                _db.UpdateUser(changes);
                _db.UpdateGarden(changes);
                
                //Retrieve updated information from database
                ProfileUpdate passUser = new ProfileUpdate();
                passUser.User = _db.GetUser(changes.User.Email);
                passUser.Garden = _db.GetUsersGarden(changes.User.Id);


                //Update session email to user's new email address
                FormsAuthentication.SetAuthCookie(passUser.User.Email, true);
                Session[SessionKeys.EmailAddress] = passUser.User.Email;

                //Add success message that exists until user leaves page
                TempData["Success"] = "Changes succesfully submitted";

                //Pass updated user information to profile view
                return View("ProfilePage", passUser);

            }
        }

        #region GardenController
       
        //Brings you to the creat garden page
        public ActionResult CreateGarden()
        {
            if (Session[SessionKeys.UserId] == null)
            {
                return RedirectToAction("Login");
            }
            Garden garden = new Garden();
            return View(garden);
        }

        [HttpPost]
        public ActionResult CreateGarden(Garden garden)
        {
            //Verify that user has logged in by checking for session key
            if (Session[SessionKeys.UserId] == null)
            {
                return RedirectToAction("Login");
            }

            //if (!ModelState.IsValid)
            //{
            //    return View("CreateGarden");
            //}

            if (string.IsNullOrWhiteSpace(garden.Name))
            {
                TempData["Failure"] = "Garden Name is required.";
                return RedirectToAction("CreateGarden", garden);
            }

            if (garden.TempLow > garden.TempHigh)
            {
                TempData["Failure"] = "Invalid temperature range. High temp must be greater than low temp.";
                return RedirectToAction("CreateGarden", garden);
            }

            if (garden.TempHigh - garden.TempLow < 5)
            {
                TempData["Failure"] = "Invalid temperature range. Difference must be greater than 5";
                return RedirectToAction("CreateGarden", garden);
            }
            garden.UserId = (int)Session[SessionKeys.UserId];
            //Returns row affected
            int returnRow =_db.AddGarden(garden);
            return RedirectToAction("GardenDetail");
        }

        //See the details of the particular garden
        public ActionResult GardenDetail()
        {
            if (Session[SessionKeys.UserId] == null)
            {
                return RedirectToAction("Login");
            }
            int userId = (int)Session[SessionKeys.UserId];
            Garden garden = _db.GetUsersGarden(userId);
            if(garden.Id == -1)
            {
                return RedirectToAction("CreateGarden");
            }
            GardenDetailViewModel gardenDetail = new GardenDetailViewModel();
            gardenDetail.AssignGarden(garden);
            gardenDetail.GardenPlots = _db.GetGardenPlot(garden);

            foreach (var plot in gardenDetail.GardenPlots)
            {
                plot.Plant = _db.GetPlantFromPlot(Convert.ToString(plot.PlantId));
            }
            return View(gardenDetail);
        }
        #endregion

        #region Plot
        public ActionResult CreatePlot(int ? GardenId)
        {
            CreatePlotView createPlotView = new CreatePlotView();
    
            //Verify that user has logged in by checking for session key
            if (Session[SessionKeys.UserId] == null)
            {
                return RedirectToAction("Login");
            }

            Plot plot = new Plot
            {
                GardenId = Convert.ToInt32(GardenId)
            };
            createPlotView.AssignPlot(plot);
            
            return View(plot);
        }

        [HttpPost]
        public ActionResult CreatePlot(Plot plot)
        {
            //Verify that user has logged in by checking for session key
            if (Session[SessionKeys.UserId] == null)
            {
                return RedirectToAction("Login");
            }

            if (!ModelState.IsValid)
            {
                return View("CreatePlot", plot);
            }

            int returnRow = _db.AddPlot(plot);
            return RedirectToAction("GardenDetail",new {gardenId = plot.GardenId });
        }

        public ActionResult AssignPlants(string plotId, double ? plotCost)
        {
            //Verify that user has logged in by checking for session key
            if (Session[SessionKeys.UserId] == null)
            {
                return RedirectToAction("Login");
            }

            if (Convert.ToInt32(Session[SessionKeys.UserId]) < 1)
            {
                return RedirectToAction("Login");
            }

            AssignPlantViewModel assignPlantView = new AssignPlantViewModel();

            TempData["plotID"] = plotId;
            Garden garden = _db.GetUsersGarden((int)Session[SessionKeys.UserId]);
            Plot plot = _db.GetPlotById(plotId);
            plot.PlotCost = Convert.ToDouble(plotCost);
            assignPlantView.Materials = _db.GetMaterials();
            assignPlantView.Plants = _db.GetPlants();
            assignPlantView.GardenHighTemp = garden.TempHigh;
            assignPlantView.GardenLowTemp = garden.TempLow;
            //assignPlantView.PlotSunlight = plot.HrsOfSun;
            assignPlantView.UserPlot = plot;
            assignPlantView.UserPlot.Plant = _db.GetPlantFromPlot(plotId);
            
            
            return View(assignPlantView);
        }

       
        public ActionResult AssignPlantsToPlot(string plantId)
        {
            //Verify that user has logged in by checking for session key
            if (Session[SessionKeys.UserId] == null)
            {
                return RedirectToAction("Login");
            }

            if (!ModelState.IsValid)
            {
                return View("AssignPlants");
            }

            int returnRow = _db.AssignPlantToPlot((string) TempData["plotId"], plantId);

            return RedirectToAction("GardenDetail");
        }

        public ActionResult EditPlot(string plotId)
        {
            //Verify that user has logged in by checking for session key
            if (Session[SessionKeys.UserId] == null)
            {
                return RedirectToAction("Login");
            }

            Plot plot = _db.GetPlotById(plotId);

            return View(plot);
        }
        
        [HttpPost]
        public ActionResult EditPlot(Plot plot)
        {
            //Verify that user has logged in by checking for session key
            if (Session[SessionKeys.UserId] == null)
            {
                return RedirectToAction("Login");
            }

            int rowAffected = _db.UpdatePlot(plot);

            return RedirectToAction("GardenDetail");
        }

        public ActionResult DeletePlot(string plotId)
        {
            Plot plot = _db.GetPlotById(plotId);
            int rowAffected = _db.DeletePlot(plot);


            return RedirectToAction("GardenDetail");
        }

        [HttpPost]
        public ActionResult DeletePlot(Plot plot)
        {
            

            return RedirectToAction("GardenDetail");
        }

        #endregion
    }
}