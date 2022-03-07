using Microsoft.AspNetCore.Mvc;

namespace Myweb.Second.Controllers.ViewComponents
{
    public class LeftMenu : ViewComponent
    {
        public LeftMenu()
        {

        }
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
