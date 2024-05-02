using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Wba.Oefening.RateAMovie.Web.Data;
using Wba.Oefening.RateAMovie.Web.Models;
using Wba.Oefening.RateAMovie.Web.ViewModels;

namespace Wba.Oefening.RateAMovie.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly MovieContext _movieContext;

        public CartController(MovieContext movieContext)
        {
            _movieContext = movieContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //create viewmodel
            var cartIndexViewModel = new CartIndexViewModel();
            cartIndexViewModel.Items = new List<MovieItem>();

            //get cart viewmodel from session if exists
            if (HttpContext.Session.Keys.Any(k => k.Equals("cartItems")))
            {
                //get the serialized viewmodel
                var serializedCartIndexViewModel = HttpContext.Session.GetString("cartItems");
                //deserialize it
                cartIndexViewModel = JsonConvert
                    .DeserializeObject<CartIndexViewModel>(serializedCartIndexViewModel);
                //calculate total
                cartIndexViewModel.Total = cartIndexViewModel.Items.Sum(i => i.Price);
            }

            //pass to the view
            return View(cartIndexViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Add(long id) 
        {
            //create viewmodel
            var cartIndexViewModel = new CartIndexViewModel();
            cartIndexViewModel.Items = new List<MovieItem>();
            //get the movie
            var movie = await _movieContext
                .Movies.FirstOrDefaultAsync(m => m.Id == id);
            //check if null
            if(movie == null)
            {
                return NotFound();
            };
            //get cart from session
            if(HttpContext.Session.Keys.Any(k => k.Equals("cartItems")))
            {
                //get the serialized viewmodel
                var serializedCartIndexViewModel = HttpContext.Session.GetString("cartItems");
                //deserialize it
                cartIndexViewModel = JsonConvert
                    .DeserializeObject<CartIndexViewModel>(serializedCartIndexViewModel);
            }
            //check if movie in cart => increment quantity
            var item = cartIndexViewModel.Items.FirstOrDefault(m => m.Id == id);
            if (item != null)
            {
                //increment quantity
                item.Quantity++;
                item.Price = item.Quantity * 25;
            }
            else
            {
                //add movie to cart viewmodel
                cartIndexViewModel.Items.Add(
                    new MovieItem
                    {
                        Id = id,
                        Title = movie.Title,
                        Price = 25,
                        Quantity = 1
                    });
            }
            //add to session
            HttpContext.Session
                .SetString("cartItems", JsonConvert.SerializeObject(cartIndexViewModel));
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Remove(long id)
        {
            //create viewmodel
            
            //get the movie
            
            //check if null
            
            //get cart from session
            
            //if no cart in session return notfound();
            
            //check if movie in cart => decrement quantity
            
            //add cart to session
            
            return RedirectToAction(nameof(Index));
        }
    }
}
