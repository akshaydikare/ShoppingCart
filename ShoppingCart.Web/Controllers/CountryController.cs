using ShoppingCart.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Web.Controllers
{
    [Authorize]
    public class CountryController : Controller
    {
        // GET: Country
        public ActionResult Index()
        {
            ShoppingCartEntities Sc = new ShoppingCartEntities();
            ViewBag.CountryList = new SelectList(GetCountryList(), "CountryId", "CountryName");
            return View();
        }

        public List<Country> GetCountryList()
        {
           ShoppingCartEntities Sc = new ShoppingCartEntities();
           List<Country> countries = Sc.Countries.ToList();
           return countries;
        }

        public ActionResult GetStateList(int CountryId)
        {
            ShoppingCartEntities Sc = new ShoppingCartEntities();
            List<State> selectlist = Sc.States.Where(x => x.CountryId == CountryId).ToList();
            ViewBag.StateList = new SelectList(selectlist, "StateId", "StateName");
            return PartialView("DisplayStates");
        }

        public ActionResult GetCityList(int StateId)
        {
            ShoppingCartEntities Sc = new ShoppingCartEntities();
            List<City> Clist = Sc.Cities.Where(x => x.StateId == StateId).ToList();
            ViewBag.CityList = new SelectList(Clist, "CityId", "CityName");
            return PartialView("DisplayCities");
        }

    }
}