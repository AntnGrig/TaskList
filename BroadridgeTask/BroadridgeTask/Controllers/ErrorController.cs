using System.Web.Mvc;

namespace BroadridgeTask.Controllers

{
    public class ErrorController : BaseController
    {
        [ValidateInput(false)]
        public ActionResult ErrorCommon(string errorMessage)
        {
            ViewBag.ErrorMessage = "В приложении произошла непредвиденная ошибка. " + (string.IsNullOrEmpty(errorMessage) ? "" : errorMessage);
            return View("_Error");
        }

        public ActionResult ErrorPageNotFound()
        {
            ViewBag.ErrorMessage = "Извините 404 - страница не найдена.";
            return View("_Error");
        }
    }
}