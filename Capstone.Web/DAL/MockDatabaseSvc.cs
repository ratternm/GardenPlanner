using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class MockDatabaseSvc 
    {
        //private static Dictionary<int, Role> _roleItems = new Dictionary<int, Role>();
        //private static Dictionary<string, User> _userItems = new Dictionary<string, User>();
        //private static int _roleId = 1;
        //private static int _userId = 1;

        //public MockDatabaseSvc()
        //{
        //    if(_roleItems.Count == 0 && _userItems.Count == 0)
        //    {
        //        DbManager.PopulateDatabase(this);
        //    }
        //}

        //public int AddUser(User user)
        //{
        //    user.Id = _userId++;
        //    _userItems.Add(user.Username, user);
        //    return user.Id;
        //}

        //public int AddRole(Role role)
        //{
        //    role.Id = _roleId++;
        //    _roleItems.Add(role.Id, role);
        //    return role.Id;
        //}

        //public List<Role> GetRole()
        //{
        //    List<Role> items = new List<Role>();
        //    foreach(var item in _roleItems)
        //    {
        //        items.Add(item.Value.Clone());
        //    }
        //    return items;
        //}

        //public User GetUser(string username)
        //{
        //    User item = null;
        //    if(_userItems.ContainsKey(username))
        //    {
        //        item = _userItems[username];
        //    }
        //    else
        //    {
        //        throw new Exception("Item does not exist");
        //    }
        //    return item.Clone();
        //}

        //public int AddRoleItem(Role role)
        //{
        //    role.Id = _roleId++;
        //    _roleItems.Add(role.Id, role);
        //    return role.Id;
        //}

        //public List<Role> GetRoles()
        //{
        //    throw new NotImplementedException();
        //}
    }
}