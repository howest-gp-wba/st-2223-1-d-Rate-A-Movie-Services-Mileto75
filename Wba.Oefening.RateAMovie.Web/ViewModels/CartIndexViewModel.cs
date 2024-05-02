using Wba.Oefening.RateAMovie.Web.Models;

namespace Wba.Oefening.RateAMovie.Web.ViewModels
{
    public class CartIndexViewModel
    {
        public List<MovieItem> Items { get; set; }
        public decimal Total { get; set; }
    }
}
