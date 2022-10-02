using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u20475960_HW05.ViewModels
{
    public class PersonViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public TitleViewModel Title { get; set; }

        public PersonViewModel()
        {
            Title = new TitleViewModel();
        }
    }
}