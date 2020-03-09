using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using eventos.Data;
using eventos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eventos.Controllers
{
    [Route("api/v1/eventos")]
    [ApiController]
    public class ApiEventosController : ControllerBase
    {
        private readonly ApplicationDbContext database;

        public ApiEventosController(ApplicationDbContext database)
        {
            this.database = database;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var produtos = database.Eventos.Include(p => p.Genero).Include(p => p.Casa).ToList();
            return Ok(produtos);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var produtos = database.Eventos.Include(p => p.Genero).Include(p => p.Casa).First(p => p.Id == id);
            return Ok(produtos);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Evento eventos = database.Eventos.First(p => p.Id == id);
                database.Remove(eventos);
                database.SaveChanges();
                return Ok("Deletado com sucesso");
            }
            catch (Exception e)
            {
                Response.StatusCode = 404;
                return new ObjectResult("");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] EventoTemp etemp)
        {
            if (etemp.Nome.Length <= 1)
            {
                Response.StatusCode = 400;
                return new ObjectResult(new { msg = "O  nome da casa precisa ter mais de um caracter." });
            }
            if (etemp.Nome == null)
            {
                Response.StatusCode = 400;
                return new ObjectResult(new { msg = "O  nome da casa é obrigatorio." });
            }
            Evento p = new Evento();
            p.Nome = etemp.Nome;
            p.capacidade = etemp.capacidade;
            p.imagem = etemp.Imagem;
            p.Data = etemp.Data;
            p.ValorIngresso = etemp.ValorIngresso;
            p.Genero = database.Generos.First(genero => genero.Id == etemp.GeneroID);
            p.Casa = database.Casas.First(casa => casa.Id == etemp.CasaID);
            p.Status = etemp.Status;
            database.Eventos.Add(p);
            database.SaveChanges();
            Response.StatusCode = 201;
            return new ObjectResult("Criado com sucesso");

        }
        [HttpGet("capacidade/asc")]
        public IActionResult GetAsc()
        {
            try
            {
                var evento = database.Eventos.ToList().OrderBy(e => e.capacidade);
                return Ok(evento);
            }
            catch
            {
                Response.StatusCode = 404;
                return new ObjectResult("Não encontrado");
            }
        }
        [HttpGet("capacidade/desc")]
        public IActionResult Getcap()
        {
            try
            {
                var evento = database.Eventos.ToList().OrderByDescending(e => e.capacidade);
                return Ok(evento);
            }
            catch
            {
                Response.StatusCode = 404;
                return new ObjectResult("Não encontrado");
            }
        }
        [HttpGet("data/asc")]
        public IActionResult Dataasc()
        {
            try
            {
                var evento = database.Eventos.ToList().OrderBy(e => e.Data);
                return Ok(evento);
            }
            catch
            {
                Response.StatusCode = 404;
                return new ObjectResult("Não encontrado");
            }
        }
        [HttpGet("data/desc")]
        public IActionResult Datadesc()
        {
            try
            {
                var evento = database.Eventos.ToList().OrderByDescending(e => e.Data);
                return Ok(evento);
            }
            catch
            {
                Response.StatusCode = 404;
                return new ObjectResult("Não encontrado");
            }
        }
        [HttpGet("nome/asc")]
        public IActionResult Nomeasc()
        {
            var casa = database.Casas.ToList();
            var genero = database.Generos.ToList();
            try
            {
                
                var evento = database.Eventos.ToList().OrderBy(e => e.Nome);
                return Ok(evento);
            }
            catch
            {
                Response.StatusCode = 404;
                return new ObjectResult("Não encontrado");
            }
        }
        [HttpGet("nome/desc")]
        public IActionResult Nomedesc()
        {    var casa = database.Casas.ToList();
            var genero = database.Generos.ToList();
            try
            {
                
                var evento = database.Eventos.ToList().OrderByDescending(e => e.Nome);
                return Ok(evento);
            }
            catch
            {
                Response.StatusCode = 404;
                return new ObjectResult("Não encontrado");
            }
        }
        [HttpGet("preco/asc")]
        public IActionResult Precoasc()
        {
            try
            {
                var evento = database.Eventos.ToList().OrderBy(e => e.ValorIngresso);
                return Ok(evento);
            }
            catch
            {
                Response.StatusCode = 404;
                return new ObjectResult("Não encontrado");
            }
        }
        [HttpGet("preco/desc")]
        public IActionResult Precodesc()
        {
            try
            {
                var evento = database.Eventos.ToList().OrderByDescending(e => e.ValorIngresso);
                return Ok(evento);
            }
            catch
            {
                Response.StatusCode = 404;
                return new ObjectResult("Não encontrado");
            }
        }
        [HttpPatch]
        public IActionResult Patch([FromBody] EventoTemp eventoTemp)
        {
                if (eventoTemp.Id > 0)
                {
                    var evento = database.Eventos.First(ctemp => ctemp.Id == eventoTemp.Id);
                    if (evento != null)
                    {
                        evento.Nome = eventoTemp.Nome != null ? eventoTemp.Nome : evento.Nome;
                        evento.capacidade = eventoTemp.capacidade != 0 ? eventoTemp.capacidade : evento.capacidade;
                        if(eventoTemp.CasaID != 0){
                            evento.Casa = database.Casas.First(c => c.Id == eventoTemp.CasaID);
                        }else{
                            evento.Casa = evento.Casa;
                        }
        
                        evento.Data = eventoTemp.Data != null ? eventoTemp.Data : evento.Data;
                        evento.ValorIngresso = eventoTemp.ValorIngresso != 0 ? eventoTemp.ValorIngresso : evento.ValorIngresso;
                        evento.imagem = eventoTemp.Imagem != null ? eventoTemp.Imagem : evento.imagem;
                        if(eventoTemp.GeneroID != 0){
                            evento.Genero = database.Generos.First(c => c.Id == eventoTemp.GeneroID);
                        }else{
                            evento.Genero = evento.Genero;
                        }
                        
                        
                        database.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        Response.StatusCode = 400;
                        return new ObjectResult(new { msg = "1O id do produto é invalido" });
                    }
                }
                else
                {
                    Response.StatusCode = 400;
                    return new ObjectResult(new { msg = "3O id do produto é invalido" });
                }
            
        }



        public class EventoTemp
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            [Required(ErrorMessage = "Digite a capacidade do local")]
            public int capacidade { get; set; }
            [Required(ErrorMessage = "Digite a data e a hora do local")]
            public DateTime Data { get; set; }
            [Required(ErrorMessage = "Digite o valor do ingresso")]
            public float ValorIngresso { get; set; }
            [Required(ErrorMessage = "Escolha a casa de show")]
            public int CasaID { get; set; }
            [Required(ErrorMessage = "Escolha o genero")]
            public int GeneroID { get; set; }
            public string Imagem { get; set; }
            [Required]
            public bool Status { get; set; }
        }
    }
}