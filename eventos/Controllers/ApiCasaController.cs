using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using eventos.Data;
using eventos.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eventos.Controllers
{
    [Route("api/v1/casas")]
    [ApiController]
    public class ApiCasaController : ControllerBase
    {
        private readonly ApplicationDbContext database;

        public ApiCasaController(ApplicationDbContext database){
            this.database = database;
        }
        [HttpGet]
        public IActionResult Getcasa(){
            var casa = database.Casas.ToList();
            return Ok(casa);
        }

        [HttpDelete("{id}")]
        public IActionResult delete(int id){
            try{
                var casa = database.Casas.First(c => c.Id == id);
                database.Remove(casa);
                database.SaveChanges();
                return Ok("Deletado com sucesso");
            }catch(Exception e){
                Response.StatusCode = 404;
                return new ObjectResult("");
            }
        }
        
        [HttpGet("{id}")]
        public IActionResult Getcasa(int id){
            try{
            var casa = database.Casas.First(c => c.Id == id);
            return Ok(casa);
            }catch{
                Response.StatusCode = 404;
                return new ObjectResult(""); 
            }
        }    
        [HttpGet("nome/{nome}")]
        public IActionResult GetcasaNome(string nome){
            try{
            var casa = database.Casas.Where(c => c.Nome == nome).First();
            return Ok(casa);
            }catch(Exception e){
                Response.StatusCode = 404;
                return new ObjectResult("");
            }
        }  
        [HttpGet("Asc")]
        public IActionResult Asc(){
            var casa = database.Casas.ToList().OrderBy(c => c.Nome);
            return Ok(casa);
        }

        [HttpGet("Desc")]
        public IActionResult Desc(){
            var casa = database.Casas.ToList().OrderByDescending(c => c.Nome);
            return Ok(casa);
        }      

        [HttpPost]
        public IActionResult Post([FromBody] CasaTemp ctemp){
                if(ctemp.Nome.Length <= 1){
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "O  nome da casa precisa ter mais de um caracter."});
            }
                if(ctemp.Nome == null){
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "O  nome da casa é obrigatorio."});
                }
            Casa p = new Casa();
            p.Nome = ctemp.Nome;
            p.Endereco = ctemp.Endereco;
            p.Status = ctemp.status;
            database.Casas.Add(p);
            database.SaveChanges();
            Response.StatusCode = 201;
            return new ObjectResult("");
            
        }
        [HttpPatch]
        public IActionResult Patch([FromBody] Casa casa){
              if(casa.Id > 0){
                  try{
                      var p = database.Casas.First(ctemp => ctemp.Id == casa.Id);
                      if(p != null){
                          p.Nome = casa.Nome != null ? casa.Nome : p.Nome;
                          p.Endereco = casa.Endereco != null ? casa.Endereco : p.Endereco;
                          p.Status = casa.Status != true ? casa.Status : p.Status;


                          database.SaveChanges();
                          return Ok();
                      }else{
                            Response.StatusCode = 400;
                            return new ObjectResult(new {msg = "O id do produto é invalido"});
                      }
                  }catch{
                    Response.StatusCode = 400;
                  return new ObjectResult(new {msg = "O id do produto é invalido"});
                  }
              }else{
                  Response.StatusCode = 400;
                  return new ObjectResult(new {msg = "O id do produto é invalido"});
              }
        }
        public class CasaTemp{
            public String Nome{get;set;}
            public string Endereco{get;set;}
            public bool status{get;set;}
        }
    }
}