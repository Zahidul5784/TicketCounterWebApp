using Microsoft.AspNetCore.Mvc;
using TicketCounterWebApp.Models;

namespace TicketCounterWebApp.ViewComponents
{
    public class ItemList : ViewComponent
    {
        public IViewComponentResult Invoke(List<TicketInfo> data)
        {

            ViewBag.Count = data.Count;
            ViewBag.Total = data.Sum(i => i.TotalPrice);

            return View(data);
        }



    }
}
