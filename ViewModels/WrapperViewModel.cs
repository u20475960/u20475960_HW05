using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace u20475960_HW05.ViewModels
{
    public class WrapperViewModel
    {
        public PersonViewModel person;
        public BookViewModel book;
        public List<BorrowsViewModel> borrowed = new List<BorrowsViewModel>();
        public List<TitleViewModel> titles = new List<TitleViewModel>();
    }
}