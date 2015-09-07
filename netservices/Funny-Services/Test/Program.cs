using FS.Web;
using FS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            FSContext Context = new FSContext();
            foreach (Role r in Context.GetAllRoles())
            {
                Console.WriteLine(r.Name);
            }
            foreach (User u in Context.GetAllUsers())
            {
                Console.WriteLine(u.Login);
            }
            Console.ReadLine();
        }
    }
}
