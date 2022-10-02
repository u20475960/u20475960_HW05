using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using u20475960_HW05.ViewModels;

namespace Tutorial.Controllers
{
    public class HomeController : Controller
    {
        private SqlConnection myConnection = new SqlConnection(Globals.ConnectionString);
        public ActionResult Index()
        {
            try
            {
                myConnection.Open();
                //Read all person records for table
                SqlCommand myCommand = new SqlCommand("Select borrows.broughtDate,books.bookId,books.name,authors.surname,types.name as typename,books.pagecount,books.point from books INNER JOIN authors ON books.authorId = authors.authorId INNER JOIN types ON books.typeId = types.typeId INNER JOIN borrows ON books.bookId = borrows.borrowId", myConnection);
                SqlDataReader myReader = myCommand.ExecuteReader();
                Globals.bookList.Clear();
                while(myReader.Read())
                {
                    BookViewModel book = new BookViewModel();

                    book.ID = (int)myReader["bookId"];

                    book.Name = myReader["name"].ToString();
                    book.author.Surname = myReader["surname"].ToString();
                    book.type.Name = myReader["typename"].ToString();
                    book.PageCount = (int)myReader["pagecount"];
                    book.Points = (int)myReader["point"];

                    DateTime now = DateTime.Now;


                    DateTime then = Convert.ToDateTime(myReader["broughtDate"].ToString());

                    int res = DateTime.Compare(then, now);

                    if(res<0)
                    {
                        book.status = "Available";
                    }
                    else
                    {
                        book.status = "Out";

                    }




                    Globals.bookList.Add(book);
                }
            }
            catch(Exception err)
            {
                ViewBag.Status = 0;
            }
            finally
            {
                myConnection.Close();
            }
            return View(Globals.bookList);
        }

        public ActionResult Insert()
        {
            List<TitleViewModel> titles = new List<TitleViewModel>();
            try
            {

                SqlCommand myCommand = new SqlCommand("Select * from Title", myConnection);

                myConnection.Open();
                SqlDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    TitleViewModel title = new TitleViewModel();
                    title.ID = (int)myReader["ID"];
                    title.Description = myReader["Description"].ToString();
                    titles.Add(title);
                }
            }
            catch (Exception err)
            {
                ViewBag.Status = 0;
            }
            finally
            {
                myConnection.Close();
            }


            return View(titles);
        }

        public ActionResult BorrowBook(int id)
        {
            try
            {
                DateTime now = DateTime.Now;

                SqlCommand myInsertCommand = new SqlCommand("UPDATE borrows set takenDate='" + now.ToString() +"',broughtDate='"+now.AddDays(5).ToString() +"'WHERE bookId="+id+";", myConnection);

                myConnection.Open();
                int rowsAffected = myInsertCommand.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                ViewBag.Status = 0;
            }
            finally
            {
                myConnection.Close();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            WrapperViewModel bookDisplay = new WrapperViewModel();
           // bookDisplay.book = Globals.bookList.Where(p => p.ID == id).FirstOrDefault();


            try
            {
                myConnection.Open();
               SqlCommand myCommand = new SqlCommand("Select books.bookId,borrows.borrowId,books.name as bname,borrows.takenDate as btdate,borrows.broughtDate as bbdate,students.name as stuname,students.surname as stusurname from books INNER JOIN borrows ON books.bookId = borrows.bookId INNER JOIN students ON borrows.studentId = students.studentId Where books.bookId= "+id, myConnection);
                //SqlCommand myCommand = new SqlCommand("Select books.bookId,books.name,authors.surname,types.name as typename,books.pagecount,books.point from books INNER JOIN authors ON books.authorId = authors.authorId INNER JOIN types ON books.typeId = types.typeId", myConnection);


                SqlDataReader myReader = myCommand.ExecuteReader();
                Globals.bookList.Clear();

                while (myReader.Read())
                {
                    BookViewModel book = new BookViewModel();

                    string name = myReader["stuname"].ToString();
                    string surname = myReader["stusurname"].ToString();


                    book.ID = (int)myReader["borrowId"];
                    book.status = myReader["bname"].ToString();
                    book.Name = myReader["btdate"].ToString();
                    book.author.Surname = myReader["bbdate"].ToString();
                    book.type.Name = name +" "+surname;





                    Globals.bookList.Add(book);




                    // bookDisplay.borrowed.Add(book);
                }
            }
            catch (Exception err)
            {
                ViewBag.Status = 0;
            }
            finally
            {
                myConnection.Close();
            }

                            return View(Globals.bookList);

          //  return View(bookDisplay.borrowed);
        }

        public ActionResult Student(int id)
        {
            WrapperViewModel bookDisplay = new WrapperViewModel();
            // bookDisplay.book = Globals.bookList.Where(p => p.ID == id).FirstOrDefault();


            try
            {
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand(" select students.studentId,students.name,students.surname,students.class,students.point from students;", myConnection);
                //SqlCommand myCommand = new SqlCommand("Select books.bookId,books.name,authors.surname,types.name as typename,books.pagecount,books.point from books INNER JOIN authors ON books.authorId = authors.authorId INNER JOIN types ON books.typeId = types.typeId", myConnection);


                SqlDataReader myReader = myCommand.ExecuteReader();
                Globals.bookList.Clear();

                while (myReader.Read())
                {
                    BookViewModel book = new BookViewModel();




                    book.ID = (int)myReader["studentId"];
                    book.status = myReader["name"].ToString();
                    book.Name = myReader["surname"].ToString();
                    book.author.Surname = myReader["class"].ToString();
                    book.Points = (int)myReader["point"];





                    Globals.bookList.Add(book);




                    // bookDisplay.borrowed.Add(book);
                }
            }
            catch (Exception err)
            {
                ViewBag.Status = 0;
            }
            finally
            {
                myConnection.Close();
            }

            return View(Globals.bookList);

            //  return View(bookDisplay.borrowed);
        }

        public ActionResult DoUpdate(int id, string name, string surname, int titleID)
        {
            try
            {
                SqlCommand myUpdateCommand = new SqlCommand("Update Person Set Name='" + name + "',Surname='" + surname + "', TitleID = " + titleID+" where ID=" + id, myConnection);

                myConnection.Open();
                int rowsAffected = myUpdateCommand.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                ViewBag.Status = 0;
            }
            finally
            {
                myConnection.Close();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Search(string search)
        {
            try
            {
                myConnection.Open();
                //Read all person records for table
                SqlCommand myCommand = new SqlCommand("Select books.bookId,books.name,authors.surname,types.name as typename,books.pagecount,books.point from books INNER JOIN authors ON books.authorId = authors.authorId INNER JOIN types ON books.typeId = types.typeId Where books.name="+search, myConnection);
                SqlDataReader myReader = myCommand.ExecuteReader();
                Globals.bookList.Clear();
                while (myReader.Read())
                {
                    BookViewModel book = new BookViewModel();

                    book.ID = (int)myReader["bookId"];

                    book.Name = myReader["name"].ToString();
                    book.author.Surname = myReader["surname"].ToString();
                    book.type.Name = myReader["typename"].ToString();
                    book.PageCount = (int)myReader["pagecount"];
                    book.Points = (int)myReader["point"];



                    Globals.bookList.Add(book);
                }
            }
            catch (Exception err)
            {
                ViewBag.Status = 0;
            }
            finally
            {
                myConnection.Close();
            }
            return View(Globals.bookList);
        }





        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}