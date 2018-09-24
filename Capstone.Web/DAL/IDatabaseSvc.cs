using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public interface IDatabaseSvc
    {
        //Role
        //int AddRoleItem(Role item);
        List<Role> GetRoles();
        int UpdateRole(string email, string id);

        //User
        int AddUser(User user);
        User GetUser(string username);
        int UpdateUser(ProfileUpdate profileUpdate);
        List<User> GetAllUsers();

        //Plot
        List<Plot> GetGardenPlot(Garden GardenId);
        int AddPlot(Plot plot);
        Plot GetPlotById(string plotID);
        int UpdatePlot(Plot plot);

        //Garden
        Garden GetUsersGarden(int userId);
        int AddGarden(Garden garden);
        Garden GetGardenById(int gardenId);
        ProfileUpdate UpdateGarden(ProfileUpdate gardenUpdate);
        int DeletePlot(Plot plot);

        //Plant
        int AddPlant(Plant plant);
        int EditPlant(Plant plant);
        List<Plant> GetPlants();
        Plant GetPlant(string plantId);
        Plant GetPlantFromPlot(string plotId);
        int AssignPlantToPlot(string plantId, string plotId);

        //Material
        List<Materials> GetMaterials();
    }

}
