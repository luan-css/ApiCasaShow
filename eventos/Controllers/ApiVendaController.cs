using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using eventos.Data;
using eventos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eventos.Controllers
{
    [Route("api/v1/vendas")]
    [ApiController]
    public class ApiVendaController : ControllerBase
    {
        private readonly ApplicationDbContext database;

        public ApiVendaController(ApplicationDbContext database)
        {
            this.database = database;
        }
        [HttpGet]
        public IActionResult Compras()
        {
            var compra = database.Compra.ToList();
            return Ok(compra);
        }
        [HttpGet("id")]
        public IActionResult compraID(int id){
            var compra = database.Compra.ToList().Where(c => c.Id == id);
            return Ok(compra);
        }
    }
}