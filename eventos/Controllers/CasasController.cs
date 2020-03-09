using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using eventos.Data;
using eventos.DTO;
using eventos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eventos.Controllers

{
    [Authorize(Policy = "Poli")]
    public class CasasController : Controller
    {
        
        private readonly ApplicationDbContext database;
        public CasasController(ApplicationDbContext database){
            this.database = database;
        } 
        [HttpPost]
        public IActionResult Salvar(CasaDTO casatemporaria){
           if(ModelState.IsValid){
               Casa casa = new Casa();
               casa.Nome = casatemporaria.Nome;
               casa.Endereco = casatemporaria.Endereco;
               casa.Status = true;
               database.Casas.Add(casa);
               database.SaveChanges();
               return RedirectToAction("Casas", "Gestao");
           }else{
               return View("../Gestao/NovaCasa");
           }
        }
        [HttpPost]
        public IActionResult Atualizar(CasaDTO casatemporaria){
            if(ModelState.IsValid){
                var casa = database.Casas.First(c => c.Id == casatemporaria.Id);
                casa.Nome = casatemporaria.Nome;
                casa.Endereco = casatemporaria.Endereco;
                database.SaveChanges();
                return RedirectToAction("Casas","Gestao");
            }else{
                return RedirectToAction("Casas","Gestao");
            }
        }
        [HttpPost]

        public IActionResult Deletar(int id){
            if(id > 0){
                var casa = database.Casas.First(casa => casa.Id == id);
                casa.Status = false;
                database.SaveChanges();
            }
            return RedirectToAction("Casas" , "Gestao");
        }
    }
}