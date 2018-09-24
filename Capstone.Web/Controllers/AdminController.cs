using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DAL;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers
{
    public class AdminController : Controller
    {
        private IDatabaseSvc _db;

        public AdminController(IDatabaseSvc db)
        {
            _db = db;
        }

        /// <summary>
        /// Connects to AddPlant view
        /// </summary>
        /// <returns></returns>
        public ActionResult AddPlant()
        {
            if (Session[SessionKeys.UserId] == null || (int)Session[SessionKeys.RoleId] == 2)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        /// <summary>
        /// Takes input from AddPlant view form and adds plant to database if valid
        /// </summary>
        /// <param name="plant"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddPlant(Plant plant)
        {
            if (Session[SessionKeys.UserId] == null || (int)Session[SessionKeys.RoleId] == 2)
            {
                return RedirectToAction("Login", "Home");
            }
            ActionResult result;
            if (!ModelState.IsValid)
            {
                result = View("AddPlant");
            }
            else
            {
                if (_db.AddPlant(plant) == 1)
                {
                    result = RedirectToAction("ProfilePage", "Home");
                }
                else
                {
                    result = View("AddPlant");
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult SelectPlant(string id)
        {
            UpdatePlantViewModel updatePlantVM = new UpdatePlantViewModel();
            if (Session[SessionKeys.UserId] == null || (int)Session[SessionKeys.RoleId] == 2)
            {
                return RedirectToAction("Login", "Home");
            }
            if(id == null)
            {
                updatePlantVM.PlantList = _db.GetPlants();
                return View("SelectPlant", updatePlantVM);
            }
            updatePlantVM.PlantList = _db.GetPlants();
            updatePlantVM.Plant = _db.GetPlant(id);
            return View("SelectPlant", updatePlantVM);
        }

        [HttpPost]
        public ActionResult UpdatePlant(Plant plant)
        {
            if (Session[SessionKeys.UserId] == null || (int)Session[SessionKeys.RoleId] == 2)
            {
                return RedirectToAction("Login", "Home");
            }
            if (plant.Cost == null || plant.Name == null || plant.SizeSq == null || plant.TempHigh == null || plant.TempLow == null)
            {
                return RedirectToAction("SelectPlant");
            }
            else
            {
                _db.EditPlant(plant);
                return RedirectToAction("ProfilePage", "Home");
            }
        }

        public ActionResult ChangeAccess(string email)
        {
            if (Session[SessionKeys.UserId] == null || (int)Session[SessionKeys.RoleId] != 1)
            {
                return RedirectToAction("Login", "Home");
            }
            ChangeAccessViewModel accessVM = new ChangeAccessViewModel();
            accessVM.UserList = _db.GetAllUsers();
            if(email != null)
            {
                accessVM.UserSelected = _db.GetUser(email);
            }

            return View("ChangeAccess", accessVM);
        }

        [HttpPost]
        public ActionResult ChangeAccess(string access, string email)
        {
            //Validate login or access
            if (Session[SessionKeys.UserId] == null || (int)Session[SessionKeys.RoleId] != 1)
            {
                return RedirectToAction("Login", "Home");
            }

            if(access == null || email == null)
            {
                TempData["Failure"] = "All necessary information was not received.";
                return View("ChangeAccess", TempData["Failure"]);
            }
            else
            {
                _db.UpdateRole(email, access);
                TempData["Success"] = "Role successfully updated!";
                return RedirectToAction("ChangeAccess", TempData["Success"]);
            }
        }
    }
}