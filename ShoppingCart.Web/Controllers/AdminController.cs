using PagedList;
using ShoppingCart.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        ShoppingCartEntities sc = new ShoppingCartEntities();

        public ActionResult CreateCategory()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("login");
            }
            return View();         
        }

        [HttpPost]
        public ActionResult CreateCategory(Category cvm, HttpPostedFileBase imgfile)
        {
            string path = uploadimgfile(imgfile);
            if (path.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            else
            {
                Category category = new Category();
                category.CategoryName = cvm.CategoryName;
                category.CategoryImage = path;
                category.CategoryDescription = cvm.CategoryDescription;
                category.CreatedOn = DateTime.Now;
                category.UpdatedOn = null;
                category.DeletedOn = null;
                category.IsActive = true;
                category.UserId = Convert.ToInt32(Session["UserId"].ToString());
                sc.Category.Add(category);
                sc.SaveChanges();
                return RedirectToAction("ViewCategory");
            }
            return View();
        } //end,,,,,,,,,,,,,,,,,,,


        public ActionResult ViewCategory(int? page)
        {
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = sc.Category.Where(x => x.IsActive == true).OrderByDescending(x => x.UserId).ToList();
            IPagedList<Category> categories = list.ToPagedList(pageindex, pagesize);
            return View(categories);
        }


        public string uploadimgfile(HttpPostedFileBase file)
        {
            Random r = new Random();
            string path = "-1";
            int random = r.Next();
            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("~/Content/uploads"), random + Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = "~/Content/uploads/" + random + Path.GetFileName(file.FileName);

                        //    ViewBag.Message = "File uploaded successfully";
                    }
                    catch (Exception)
                    {
                        path = "-1";
                    }
                }
                else
                {
                    Response.Write("<script>alert('Only jpg ,jpeg or png formats are acceptable....'); </script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Please select a file'); </script>");
                path = "-1";
            }
            return path;
        }
    }
}
    