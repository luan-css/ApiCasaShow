using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using eventos.Models;
using eventos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Net.Http;

namespace eventos.Controllers
{
    
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext database;
        public HomeController(ApplicationDbContext database){
            this.database = database;
        } 

        public IActionResult Index(){
           
            var eventos = database.Eventos.Include(p => p.Genero).Include(p => p.Casa).Where(P => P.Status == true).Where(c => c.Casa.Status == true).Where(g => g.Genero.Status == true).ToList();
            return View(eventos);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
