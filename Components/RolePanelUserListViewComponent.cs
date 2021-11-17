using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pizza.Models;
using Pizza.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizza.Components
{
    public class RolePanelUserListViewComponent : ViewComponent
    {
        PizzaContext _db;
        UserManager<User> _userManager;

        public RolePanelUserListViewComponent(PizzaContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke(RolesSort newSort)
        {
            List<Tuple<User, List<string>>> usersWithRoles;
            List<User> users;
            switch (newSort)
            {
                case RolesSort.All:
                    users = _db.Users.ToList();
                    break;
                case RolesSort.Admin:
                    users = _userManager.GetUsersInRoleAsync("admin").Result.ToList();
                    break;
                case RolesSort.Manager:
                    users = _userManager.GetUsersInRoleAsync("manager").Result.ToList();
                    break;
                case RolesSort.User:
                    users = _userManager.GetUsersInRoleAsync("user").Result.ToList();
                    break;
                default:
                    users = _db.Users.ToList();
                    break;
            }
            usersWithRoles = users.Select(u => new Tuple<User, List<string>>(u, _userManager.GetRolesAsync(u).Result.ToList())).ToList();
            return View(usersWithRoles);
        }
    }
}
