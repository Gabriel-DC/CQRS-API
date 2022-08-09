using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace CqrsApi.Api.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok($"Server is running! Version: {Assembly.GetExecutingAssembly().GetName().Version}");
        }

        [HttpGet("error")]
        public IActionResult Error()
        {
            throw new NotImplementedException("Erro espontâneo");
        }
    }
}
