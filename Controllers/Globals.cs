using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using u20475960_HW05.ViewModels;

namespace u20475960_HW05.Controllers
{
    public static class Globals
    {
        public static string ConnectionString = "Data Source=DESKTOP-NUAHJKH\\SQLEXPRESS;Initial Catalog=Person;Integrated Security=True";
        public static List<PersonViewModel> personList = new List<PersonViewModel>();
        public static List<BookViewModel> bookList = new List<BookViewModel>();

    }
}