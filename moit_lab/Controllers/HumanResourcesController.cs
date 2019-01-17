using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using moit_lab.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace moit_lab.Controllers
{
    public class HumanResourcesController : Controller
    {
        private readonly HumanResourcesContext _context;

        public HumanResourcesController(HumanResourcesContext context)
        {
            _context = context;

        }

        public IActionResult StaffList() => View(_context.StaffMember.ToList());

        // GET: /<controller>/
        //public IActionResult Index()
        //{
            

        //    return View();
        //}
    }
}
