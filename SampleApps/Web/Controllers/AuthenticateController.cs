using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class AuthenticateController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}