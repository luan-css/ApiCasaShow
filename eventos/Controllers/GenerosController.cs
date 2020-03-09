using eventos.Data;
using eventos.DTO;
using eventos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace eventos.Controllers
{
    [Authorize(Policy = "Poli")]
    public class GenerosController : Controller
    {
        

        private readonly ApplicationDbContext database;
        public GenerosController(ApplicationDbContext database){
            this.database = database;
        } 

        [HttpPost]
        public IActionResult Salvar(GeneroDTO generoTemporario){
            if(ModelState.IsValid){
                Genero genero = new Genero();
                genero.Nome = generoTemporario.Nome;
                genero.Status = true;
                database.Generos.Add(genero);
                database.SaveChanges();
                return RedirectToAction("Generos", "Gestao");

            }else{
                return View("../Gestao/NovoGenero");
            }
        }
        [HttpPost]
        public IActionResult Atualizar(GeneroDTO generoTemporario){
            if(ModelState.IsValid){
                  var genero = database.Generos.First(p => p.Id == generoTemporario.Id);
                  genero.Nome = generoTemporario.Nome;
                  database.SaveChanges();
                  return RedirectToAction("Generos","Gestao");  
            }else{
                return RedirectToAction("Generos","Gestao");
            }
        }
        [HttpPost]
        public IActionResult Deletar(int id){
            if(id > 0){
                var genero = database.Generos.First(gene => gene.Id == id);
                genero.Status = false;
                database.SaveChanges();
            }
            return RedirectToAction("Generos","Gestao"); 
        }
    }
}