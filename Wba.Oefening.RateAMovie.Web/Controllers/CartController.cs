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

            //calculate total

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
                
            };
            //get cart from session
            
            //check if movie in cart => increment quantity
            
            //add movie to cart viewmodel
            
            //add to session
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
