using eventos.Data;
using eventos.DTO;
using eventos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace eventos.Controllers
{
    
    public class GestaoController : Controller
    {
        
        private readonly ApplicationDbContext database;
        public GestaoController(ApplicationDbContext database){
            this.database = database;
        } 
        public IActionResult Index(){
            var eventos = database.Eventos.Include(p => p.Genero).Include(p => p.Casa).Where(P => P.Status == true).ToList();
            return View(eventos);
        }
        [Authorize(Policy = "Poli")]
        public IActionResult Generos(){
            var generos = database.Generos.Where(gene => gene.Status == true).ToList();
            return View(generos);
        }
        [Authorize(Policy = "Poli")]
        public IActionResult NovoGenero(){
            return View();
        }
        [Authorize(Policy = "Poli")]
        public IActionResult EditarGenero(int id){
            var genero = database.Generos.First(gene => gene.Id == id);
            GeneroDTO generoView = new GeneroDTO();
            generoView.Id = genero.Id;
            generoView.Nome = genero.Nome;
            return View(generoView);
        }
        [Authorize(Policy = "Poli")]
        public IActionResult Casas(){
            var casas = database.Casas.Where(casa => casa.Status == true).ToList();
            return View(casas);
        }
        [Authorize(Policy = "Poli")]
        public IActionResult NovaCasa(){
            return View();
        }
        [Authorize(Policy = "Poli")]
        public IActionResult EditarCasa(int id){
            var casa = database.Casas.First(casa => casa.Id == id);
            CasaDTO casaView = new CasaDTO();
            casaView.Id = casa.Id;
            casaView.Nome = casa.Nome;
            casaView.Endereco = casa.Endereco;
            return View(casaView);
        }
        [Authorize(Policy = "Poli")]
        public IActionResult Eventos(){
            var eventos = database.Eventos.Include(p => p.Genero).Include(p => p.Casa).Where(P => P.Status == true).Where(c => c.Casa.Status == true).Where(g => g.Genero.Status == true).ToList();
            return View(eventos);
            
        }
        [Authorize(Policy = "Poli")]
        public IActionResult NovoEvento(){
            ViewBag.Generos = database.Generos.Where(genero => genero.Status == true).ToList();
            ViewBag.Casas = database.Casas.Where(casa => casa.Status == true).ToList();
            var casas = database.Casas.FirstOrDefault(c => c.Status);
            var generos = database.Generos.FirstOrDefault(g => g.Status);
            if(casas == null || generos == null){
                return View("ErroCad");
            }
            return View();
        }
        [Authorize(Policy = "Poli")]
        public IActionResult EditarEvento(int id){
            var evento = database.Eventos.Include(eve => eve.Genero).Include(eve => eve.Casa).First(eve => eve.Id == id);
            EventoDTO eventoView = new EventoDTO();
            eventoView.Id = evento.Id;
            eventoView.Nome = evento.Nome;
            eventoView.capacidade = evento.capacidade;
            eventoView.Data = evento.Data;
            eventoView.CasaID = evento.Casa.Id;
            eventoView.GeneroID = evento.Genero.Id;
            eventoView.ValorIngresso = evento.ValorIngresso;
            eventoView.Imagem = evento.imagem;
            ViewBag.Generos = database.Generos.ToList();
            ViewBag.Casas = database.Casas.ToList();
            return View(eventoView);
        }
        [Authorize]
        public IActionResult ComprarEvento(int id){
            var evento = database.Eventos.Include(eve => eve.Genero).Include(eve => eve.Casa).First(eve => eve.Id == id);
            EventoDTO eventoView = new EventoDTO();
            eventoView.Id = evento.Id;
            eventoView.Nome = evento.Nome;
            eventoView.capacidade = evento.capacidade;
            eventoView.Data = evento.Data;
            eventoView.CasaID = evento.Casa.Id;
            eventoView.GeneroID = evento.Genero.Id;
            eventoView.ValorIngresso = evento.ValorIngresso;
            eventoView.Quantidade = evento.Quantidade;
            eventoView.Imagem = evento.imagem.ToString();
            ViewBag.Generos = database.Generos.ToList();
            ViewBag.Casas = database.Casas.ToList();    
            return View(eventoView);
        }
        [Authorize]
        public IActionResult Historico(){
            var compra = database.Compra.ToList();
            return View(compra);
        }
    }
}