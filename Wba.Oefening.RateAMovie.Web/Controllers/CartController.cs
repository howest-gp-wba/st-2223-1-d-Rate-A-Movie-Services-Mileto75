using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Wba.Oefening.RateAMovie.Web.Models;
using Wba.Oefening.RateAMovie.Web.ViewModels;

namespace Wba.Oefening.RateAMovie.Web.Controllers
{
    public class CartController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var cartIndexViewModel = new CartIndexViewModel();
            cartIndexViewModel.Items = new List<MovieItem>();
            //get cart viewmodel from session
            if(HttpContext.Session.Keys.Any(k => k.Equals("cartItems")))
            {
                //get cart from session
                var serializedCartIndexViewModel = HttpContext.Session.GetString("cartItems");
                //deserialise
                cartIndexViewModel = JsonConvert.DeserializeObject<CartIndexViewModel>(serializedCartIndexViewModel);
            }
            //pass to the view
            return View(cartIndexViewModel);
        }
        [HttpGet]
        public IActionResult Add(long id) 
        {
            //get the movie
            //check if null
            //get cart from session
            //add movie to cart viewmodel
            //add to session
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Remove(long id)
        {
            //get the movie
            //check if null
            //get cart from session
            //remove movie from cart viewmodel
            //add cart to session
            return RedirectToAction(nameof(Index));
        }
    }
}
