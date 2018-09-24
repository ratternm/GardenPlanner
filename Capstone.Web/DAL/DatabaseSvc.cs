using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class DatabaseSvc : IDatabaseSvc
    {
        private string _connectionString;

        public DatabaseSvc(string connectionString)
        {
            _connectionString = connectionString;
        }

        //public int AddRoleItem(Role item)
        //{
        //    int result = 0;
        //    try
        //    {

        //    }
        //    catch(Exception)
        //    {
        //        throw;
        //    }

        //    return result;
        //}

        /// <summary>
        /// Adds a new User to User table after registration
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        
        #region RoleDAL
        /// <summary>
        /// Retrieving all roles from Role table in database
        /// </summary>
        /// <returns></returns>
        public List<Role> GetRoles()
        {
            List<Role> roleList = new List<Role>();
            const string getRolesSqlQuery = "SELECT * FROM roles;";
            try
            {
                SqlConnection conn = new SqlConnection(_connectionString);
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(getRolesSqlQuery, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        roleList.Add(PopulateRole(reader));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return roleList;
        }

        /// <summary>
        /// This binds the columns from the role table to a Role object.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public Role PopulateRole(SqlDataReader reader)
        {
            return new Role()
            {
                Id = Convert.ToInt32(reader["Id"]),
                RoleName = Convert.ToString(reader["RoleName"])
            };
        }

        /// <summary>
        /// Updates alterations to User's roles from Admin
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public int UpdateRole(string email, string id)
        {
            int result = 0;
            const string roleUpdateSQL = "UPDATE [User] " +
                                         "SET RoleId = @id " +
                                         "WHERE Email = @email;";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(roleUpdateSQL, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@email", email);
                    result = cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
            return result;
        }
        #endregion
        #region UserDAL
        /// <summary>
        /// This Adds the new user to the Db
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int AddUser(User user)
        {
            int result = 0;
            const string insertUserSql = "INSERT INTO [User] (FirstName, LastName, Email, Password, Salt, RoleId) " +
                                         "VALUES(@firstName, @lastName, @email, @password, @salt, @roleId);";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(insertUserSql, conn);
                    cmd.Parameters.AddWithValue("@firstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", user.LastName);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@salt", user.Salt);
                    cmd.Parameters.AddWithValue("@roleId", user.RoleId);
                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        /// <summary>
        /// Retrieves the Users that matching the Email parameter.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>User Object</returns>
        public User GetUser(string email)
        {
            User user = new User();
            const string getUserSqlQuery = "SELECT * FROM [user] WHERE email = @email;";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(getUserSqlQuery, conn);
                    cmd.Parameters.AddWithValue("@email", email);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        user = PopulateUser(reader);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return user;
        }

        public List<User> GetAllUsers()
        {
            List<User> userList = new List<User>();
            const string sqlQuery = "SELECT * FROM [User];";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        userList.Add(PopulateUser(reader));
                    }
                }
            }
            catch
            {
                throw;
            }
            return userList;
        }

        /// <summary>
        /// This binds the columns from the User table to a User object. 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public User PopulateUser(SqlDataReader reader)
        {
            return new User()
            {
                Id = Convert.ToInt32(reader["Id"]),
                Email = Convert.ToString(reader["Email"]),
                FirstName = Convert.ToString(reader["FirstName"]),
                LastName = Convert.ToString(reader["LastName"]),
                RoleId = Convert.ToInt32(reader["RoleId"]),
                Password = Convert.ToString(reader["Password"]),
                Salt = Convert.ToString(reader["Salt"])
            };
        }

        public int UpdateUser(ProfileUpdate profileUpdate)
        {
            int rowsAffected = 0;
            
            const string getUserSqlQuery = "update [USER] SET Email = @newEmail, FirstName = @firstName, LastName = @lastName WHERE Id = @userID;";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(getUserSqlQuery, conn);
                    cmd.Parameters.AddWithValue("@newEmail", profileUpdate.User.Email);
                    cmd.Parameters.AddWithValue("@firstName", profileUpdate.User.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", profileUpdate.User.LastName);
                    cmd.Parameters.AddWithValue("@userID", profileUpdate.User.Id);
                    //SqlDataReader reader = cmd.ExecuteReader();

                    rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                            
                    }
                    //while (reader.Read())
                    //{

                    //    user.User.FirstName = Convert.ToString(reader["FirstName"]);
                    //    user.User.LastName = Convert.ToString(reader["LastName"]);
                    //    user.User.Email = Convert.ToString(reader["Email"]);

                    //}
                }
            }
            catch (Exception)
            {
                throw;
            }
            
            return rowsAffected;
        }
        #endregion
        #region PlotDAL
        /// <summary>
        /// Retrieves all the Plot for the specific garden Id
        /// </summary>
        /// <param name="GardenId"></param>
        /// <returns>List<Plot></returns>
        public List<Plot> GetGardenPlot(Garden GardenId)
        {

            List<Plot> plots = new List<Plot>();
            const string getUserSqlQuery = "SELECT * FROM Plot WHERE GardenId = @GardenId;";
            try
            {
                SqlConnection conn = new SqlConnection(_connectionString);
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(getUserSqlQuery, conn);
                    cmd.Parameters.AddWithValue("@GardenId", GardenId.Id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Plot plot = new Plot();
                        plot = PopulatePlot(reader);
                        plots.Add(plot);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return plots;
        }

        /// <summary>
        /// This binds the columns from the Plot table to a Plot object.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public Plot PopulatePlot(SqlDataReader reader)
        {
            return new Plot()
            {
                Name = Convert.ToString(reader["Name"]),
                Id = Convert.ToInt32(reader["Id"]),
                Width = Convert.ToInt32(reader["Width"]),
                Length = Convert.ToInt32(reader["Length"]),
                GardenId = Convert.ToInt32(reader["GardenId"]),  
                HrsOfSun = Convert.ToInt32(reader["HrsOfSun"]),
                PlantId = Convert.ToInt32(reader["PlantId"])
            };
        }

        public int AddPlot(Plot plot)
        {
            int result = 0;
            const string insertUserSql = "INSERT INTO [Plot] (Name, Width, Length, GardenId, HrsOfSun, PlantId) " +
                                         "VALUES(@Name, @Width, @Length, @Garden, @HrsOfSun, @PlantId);";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(insertUserSql, conn);

                    cmd.Parameters.AddWithValue("@Name", plot.Name);
                    cmd.Parameters.AddWithValue("@Width", plot.Width);
                    cmd.Parameters.AddWithValue("@Length", plot.Length); 
                    cmd.Parameters.AddWithValue("@Garden", plot.GardenId);
                    cmd.Parameters.AddWithValue("@HrsOfSun", plot.HrsOfSun);
                    cmd.Parameters.AddWithValue("@PlantId", plot.PlantId);
                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        /// <summary>
        /// Gets a plot that is associated with the plotID parameter
        /// </summary>
        /// <param name="plotID"></param>
        /// <returns></returns>
        public Plot GetPlotById(string plotID)
        {
            Plot plot = new Plot();
            const string getUserSqlQuery = "SELECT * FROM [Plot] WHERE Id = @PlotId;";
            try
            {
                SqlConnection conn = new SqlConnection(_connectionString);
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(getUserSqlQuery, conn);
                    cmd.Parameters.AddWithValue("@PlotID", plotID);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        plot = PopulatePlot(reader);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return plot;
        }

        public int UpdatePlot(Plot plot)
        {
            int rowsAffected = 0;

            const string getUserSqlQuery = "update [Plot] SET Name = @Name, Width = @Width, Length = @Length, HrsOfSun = @HrsOfSun WHERE Id = @Id;";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(getUserSqlQuery, conn);
                    cmd.Parameters.AddWithValue("@Name", plot.Name);
                    cmd.Parameters.AddWithValue("@Width", plot.Width);
                    cmd.Parameters.AddWithValue("@Length", plot.Length);
                    cmd.Parameters.AddWithValue("@HrsOfSun", plot.HrsOfSun);
                    cmd.Parameters.AddWithValue("@Id", plot.Id);

                    rowsAffected = cmd.ExecuteNonQuery();
               }
            }
            catch (Exception)
            {
                throw;
            }

            return rowsAffected;
        }

        public int DeletePlot(Plot plot)
        {
            int rowsAffected = 0;

            const string getUserSqlQuery = "Delete [Plot] Where Id = @Id;";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(getUserSqlQuery, conn);
                    cmd.Parameters.AddWithValue("@Id", plot.Id);
                    
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return rowsAffected;
        }
            #endregion
            #region GardenDAL
            /// <summary>
            /// Retrieves the Garden for the specific User Id
            /// </summary>
            /// <param name="GardenId"></param>
            /// <returns>List<Plot></returns>
            /// 

            public ProfileUpdate UpdateGarden(ProfileUpdate gardenUpdate)
        {
            ProfileUpdate user = new ProfileUpdate();
            const string getUserSqlQuery = "update Garden SET NAME = @name, TempLow = @tempLow, TempHigh = @tempHigh where UserId = @Id;";
            try
            {
                SqlConnection conn = new SqlConnection(_connectionString);
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(getUserSqlQuery, conn);
                    cmd.Parameters.AddWithValue("@name", gardenUpdate.Garden.Name);
                    cmd.Parameters.AddWithValue("@tempLow", gardenUpdate.Garden.TempLow);
                    cmd.Parameters.AddWithValue("@tempHigh", gardenUpdate.Garden.TempHigh);
                    cmd.Parameters.AddWithValue("@Id", gardenUpdate.User.Id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        
                        user.Garden.Name = Convert.ToString(reader["Name"]);
                        user.Garden.TempHigh = Convert.ToInt32(reader["TempHigh"]);
                        user.Garden.TempLow = Convert.ToInt32(reader["TempLow"]);
               
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            user.User = gardenUpdate.User;
            return user;
        }


        public Garden GetUsersGarden(int userId)
        {
            Garden garden = new Garden();

            const string getUserSqlQuery = "SELECT * FROM Garden WHERE UserId = @UserId;";
            try
            {               
                SqlConnection conn = new SqlConnection(_connectionString);
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(getUserSqlQuery, conn);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        
                        garden = PopulateGarden(reader);
                        
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return garden;
        }

        /// <summary>
        /// This binds the columns from the Garden table to a Garden object.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public Garden PopulateGarden(SqlDataReader reader)
        {
            return new Garden()
            {
                Name = Convert.ToString(reader["Name"]),
                Id = Convert.ToInt32(reader["Id"]),
                TempHigh = Convert.ToInt32(reader["TempHigh"]),
                TempLow = Convert.ToInt32(reader["TempLow"]),
                UserId = Convert.ToInt32(reader["UserId"]),
            };
        }

        public int AddGarden(Garden garden)
        {
            int result = 0;
            const string insertUserSql = "INSERT INTO [garden] (Name, TempLow, TempHigh, UserId) " +
                                         "VALUES(@Name, @TempLow, @TempHigh, @UserId);";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(insertUserSql, conn);

                    cmd.Parameters.AddWithValue("@Name", garden.Name);
                    cmd.Parameters.AddWithValue("@TempLow", garden.TempLow);
                    cmd.Parameters.AddWithValue("@TempHigh", garden.TempHigh);
                    cmd.Parameters.AddWithValue("@UserId", garden.UserId);
                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Gets the Garden object that is associated with that GardenId
        /// </summary>
        /// <param name="gardenId"></param>
        /// <returns></returns>
        public Garden GetGardenById(int gardenId)
        {
            Garden garden = new Garden();
            const string getUserSqlQuery = "SELECT * FROM [garden] WHERE Id = @Id;";
            try
            {
                SqlConnection conn = new SqlConnection(_connectionString);
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(getUserSqlQuery, conn);
                    cmd.Parameters.AddWithValue("@Id", gardenId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        garden = PopulateGarden(reader);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return garden;
        }


        #endregion
        #region PlantDAL
        /// <summary>
        /// Adds a new plant object to table
        /// Only accessible by profiles with Admin role
        /// </summary>
        /// <param name="plant"></param>
        /// <returns></returns>
        public int AddPlant(Plant plant)
        {
            const string insertCommand = "INSERT INTO Plant(Name, SizeSq, TempLow, TempHigh, Cost, SunReqHrs) " +
                                         "VALUES(@Name, @SizeSq, @TempLow, @TempHigh, @Cost, @SunReqHrs);";
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(insertCommand, conn);
                    cmd.Parameters.AddWithValue("@Name", plant.Name);
                    cmd.Parameters.AddWithValue("@SizeSq", plant.SizeSq);
                    cmd.Parameters.AddWithValue("@TempLow", plant.TempLow);
                    cmd.Parameters.AddWithValue("@TempHigh", plant.TempHigh);
                    cmd.Parameters.AddWithValue("@Cost", plant.Cost);
                    cmd.Parameters.AddWithValue("@SunReqHrs", plant.SunReqHrs);
                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Alters plant object currently in Plant table
        /// Only accessible by profiles with Admin role
        /// </summary>
        /// <returns></returns>
        public int EditPlant(Plant plant)
        {
            const string insertCommand = "UPDATE Plant " +
                                         "SET Name = @Name, SizeSq = @SizeSq, TempLow = @TempLow, TempHigh = @TempHigh, Cost = @Cost " +
                                         "WHERE Id = @Id;";
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(insertCommand, conn);
                    cmd.Parameters.AddWithValue("@Name", plant.Name);
                    cmd.Parameters.AddWithValue("@SizeSq", plant.SizeSq);
                    cmd.Parameters.AddWithValue("@TempLow", plant.TempLow);
                    cmd.Parameters.AddWithValue("@TempHigh", plant.TempHigh);
                    cmd.Parameters.AddWithValue("@Cost", plant.Cost);
                    cmd.Parameters.AddWithValue("@SunReqHrs", plant.SunReqHrs);          
                    cmd.Parameters.AddWithValue("@Id", plant.Id); 
                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Retrieves all plants from plant table and puts them into a list of Plant objects
        /// </summary>
        /// <returns></returns>
        public List<Plant> GetPlants()
        {
            List<Plant> plantList = new List<Plant>();
            const string getAllPlantsQuery = "SELECT * FROM Plant;";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(getAllPlantsQuery, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        plantList.Add(PopulatePlant(reader));
                    }
                }
            }
            catch(Exception)
            {
                throw;
            }
            return plantList;
        }

        public Plant GetPlant(string plantId)
        {
            const string getSinglePlantSql = "SELECT * FROM Plant WHERE Id = @plantId;";
            Plant plant = new Plant();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(getSinglePlantSql, conn);
                    cmd.Parameters.AddWithValue("@plantId", plantId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        plant = PopulatePlant(reader);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return plant;
        }

        /// <summary>
        /// This binds the columns from the plant table to a Plant object.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public Plant PopulatePlant(SqlDataReader reader)
        {
            return new Plant()
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = Convert.ToString(reader["Name"]),
                SizeSq = Convert.ToInt32(reader["SizeSq"]),
                TempLow = Convert.ToInt32(reader["TempLow"]),
                TempHigh = Convert.ToInt32(reader["TempHigh"]),
                Cost = Convert.ToInt32(reader["Cost"]),
                SunReqHrs = Convert.ToInt32(reader["SunReqHrs"])
                
            };
        }

        //TODO
        public Plant GetPlantFromPlot(string plotId)
        {
            Plant plant = new Plant();
            const string getUserSqlQuery = "SELECT * FROM Plant WHERE Id = @PlotId;";
            try
            {
                SqlConnection conn = new SqlConnection(_connectionString);
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(getUserSqlQuery, conn);
                    cmd.Parameters.AddWithValue("@PlotId", plotId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                       
                        plant = PopulatePlant(reader);
                      
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return plant;
        }

        public int AssignPlantToPlot(string plotId, string plantId)
        {
            int result = 0;
            const string insertUserSql = "UPDATE [PLOT] SET PlantId = @PlantId WHERE Id = @PlotId;";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(insertUserSql, conn);

                    cmd.Parameters.AddWithValue("@PlantId", plantId);
                    cmd.Parameters.AddWithValue("@PlotId", plotId);
                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }




        #endregion
        #region MaterialDAL

        public List<Materials> GetMaterials()
        {
            List<Materials> materialList = new List<Materials>();
            const string SqlQuery = "SELECT * FROM Materials";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SqlQuery, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        materialList.Add(PopulateMaterial(reader));
                    }
                }
            }
            catch
            {
                throw;
            }

                return materialList;
        }

        /// <summary>
        /// This binds the columns from the Materials table to a Materials object. 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public Materials PopulateMaterial(SqlDataReader reader)
        {
            return new Materials()
            {
                Description = Convert.ToString(reader["MatDescrip"]),
                MatName = Convert.ToString(reader["MatName"]),
                MatCost = Convert.ToDecimal(reader["CostPerSqFt"])
                //MatQty = Convert.ToInt32(reader["MatQty"])
            };
        }

        #endregion
    }
}