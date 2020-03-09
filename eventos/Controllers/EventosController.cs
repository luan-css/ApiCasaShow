using System.ComponentModel.DataAnnotations;
using System.Linq;
using eventos.Data;
using eventos.DTO;
using eventos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eventos.Controllers
{

    [Authorize]
    public class EventosController : Controller
    {
        
        private readonly ApplicationDbContext database;
        public EventosController(ApplicationDbContext database){
            this.database = database;
        } 
        [Authorize(Policy = "Poli")]
        [HttpPost]
        public IActionResult Salvar(EventoDTO eventoTemporario){
            if(ModelState.IsValid){
                Evento evento = new Evento();
                evento.Nome = eventoTemporario.Nome;
                evento.capacidade = eventoTemporario.capacidade;
                evento.Genero = database.Generos.First(genero => genero.Id == eventoTemporario.GeneroID);
                evento.Casa = database.Casas.First(casa => casa.Id == eventoTemporario.CasaID);
                evento.ValorIngresso = eventoTemporario.ValorIngresso;
                evento.Data = eventoTemporario.Data;
                evento.Quantidade = eventoTemporario.Quantidade;
                evento.imagem = eventoTemporario.Imagem;
                evento.Status = true;
                database.Eventos.Add(evento);
                database.SaveChanges();
                return RedirectToAction("Eventos" , "Gestao");
            }else{
                ViewBag.Generos = database.Generos.ToList();
                ViewBag.Casas = database.Casas.ToList();
                return View("../Gestao/NovoEvento");
            }
        }
        [Authorize(Policy = "Poli")]
        [HttpPost]
        public IActionResult Atualizar(EventoDTO eventoTemporario, CompraDTO compratemporaria){
            if(ModelState.IsValid){
                var evento = database.Eventos.First(eve => eve.Id == eventoTemporario.Id);
                Compra compra = new Compra();
                evento.Nome = eventoTemporario.Nome;
                evento.Casa = database.Casas.First(casa => casa.Id == eventoTemporario.CasaID);
                evento.Genero = database.Generos.First(gene => gene.Id == eventoTemporario.GeneroID);
                evento.imagem = eventoTemporario.Imagem;
                evento.ValorIngresso = eventoTemporario.ValorIngresso;
                evento.Data = eventoTemporario.Data;
                evento.capacidade = eventoTemporario.capacidade;
                compra.Quantidade = compratemporaria.Quantidade;
                if(compra.Quantidade > 0){
                    evento.capacidade -= compratemporaria.Quantidade;
                }
                database.SaveChanges();
                return RedirectToAction("Eventos","Gestao");
                
            }else{
                return RedirectToAction("Produtos","Gestao");
            }
        }
        [Authorize(Policy = "Poli")]
        [HttpPost]
        public IActionResult Deletar(int id){
            if(id > 0){
                var evento = database.Eventos.First(eve => eve.Id == id);
                evento.Status = false;
                database.SaveChanges();
            }
            return RedirectToAction("Eventos", "Gestao");
        }


[Authorize(Policy = "Poli")]
        [HttpPost]
        public IActionResult AtualizarEstoque(CompraDTO compratemporaria){

            if(ModelState.IsValid){
                Compra compra = new Compra();
                compra.Nome = compratemporaria.Nome;
                compra.ValorIngresso = compratemporaria.ValorIngresso;
                compra.capacidade = compratemporaria.capacidade;
                compra.Quantidade = compratemporaria.Quantidade;
                compra.usuario = compratemporaria.usuario;
                compra.total = compratemporaria.total;
                return RedirectToAction("Index","Home");
                
            }else{
                return RedirectToAction("Index","Home");
            }
        }
        [HttpPost]
        public IActionResult SalvarEstoque(CompraDTO compratemporaria, EventoDTO eventoTemporario){
            if(ModelState.IsValid){
                Compra compra = new Compra();
                var evento = database.Eventos.First(eve => eve.Id == eventoTemporario.Id);
                compra.Nome = compratemporaria.Nome;
                compra.ValorIngresso = compratemporaria.ValorIngresso;
                compra.capacidade = compratemporaria.capacidade;
                compra.Quantidade = compratemporaria.Quantidade;
                compra.usuario = compratemporaria.usuario;
                compra.total = compratemporaria.total;
                compra.imagem = compratemporaria.imagem;
                evento.capacidade = eventoTemporario.capacidade;
                compra.usuario =  User.Identity.Name;
                database.Compra.Add(compra);
                database.SaveChanges();
                if(compra.Quantidade > 0 && compra.Quantidade <= compra.capacidade){
                    compra.total = (compra.Quantidade*compra.ValorIngresso);
                    evento.capacidade = compra.capacidade -= compra.Quantidade;
                    database.SaveChanges();
                }
                
                database.Update(evento);
                return RedirectToAction("Index","Home");
            }else{
                return RedirectToAction("Index","Home");
            }
        }
    }
}