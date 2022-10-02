using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u20475960_HW05.ViewModels
{
    public class TypeViewModel
    {
        public string Name { get; set; }

        public string takenDate { get; set; }
        public string broughtDate { get; set; }

        public string borrowedby { get; set; }
        public int borrowId { get; set; }
        public int studentId { get; set; }
        public int bookId { get; set; }
    }
}
