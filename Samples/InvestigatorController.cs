using Microsoft.AspNetCore.Mvc;
using sk.Services;

namespace sk.Samples
{
    public class InvestigatorController : Controller
    {
        private readonly IDBService _dbService;
        public InvestigatorController(IDBService dbService)
        {
            _dbService = dbService;
        }
        public IActionResult Index()
        {
            var model = _dbService.GetAllEvents();
            return View("~/Samples/Investigator.cshtml", model);
        }
    }
}
