using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FS.Web.Models.Admin
{
    public class UserPaging : ItemPaging
    {
        public List<User> Users {get; set;}

        public UserPaging(List<User> allUsers, int currentPage, int usersPerPage, int numOfAllUsers)
        {
            ItemsPerPage = usersPerPage;
            double a = (double)numOfAllUsers / (double)ItemsPerPage;
            NumOfPages = (int)Math.Ceiling(a);
            if (currentPage != 0)
            {
                if (currentPage < NumOfPages)
                {
                    CurrentPage = currentPage;
                }
                else
                {
                    CurrentPage = NumOfPages;
                }
            }
            else
            {
                CurrentPage = NumOfPages;
            }

            Users = allUsers.Skip((CurrentPage - 1) * ItemsPerPage).Take(ItemsPerPage).ToList();
        }
    }
}