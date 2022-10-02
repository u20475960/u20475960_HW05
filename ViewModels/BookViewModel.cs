using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u20475960_HW05.ViewModels
{
    public class BookViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }
        public int PageCount { get; set; }
        public int Points { get; set; }
        public string status { get; set; }

        public AuthorViewModel author { get; set; }
        public TypeViewModel type { get; set; }

        public BorrowsViewModel borrows { get; set; }


        public BookViewModel()
        {
            author = new AuthorViewModel();
            type = new TypeViewModel();
            borrows = new BorrowsViewModel();
        }
    }


}