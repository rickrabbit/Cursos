using App.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApp.Models;

namespace WebApp.Controllers
{
    [EnableCors("*", "*", "*")]
    public class AlunoController : ApiController
    {
        [HttpGet]
        // GET: api/Aluno
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(AlunoModel.listaAlunosDB());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        // GET: api/Aluno/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(AlunoModel.listaAlunosDB().Where(w => w.id == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]        
        // POST: api/Aluno
        public IHttpActionResult Post([FromBody]AlunoDTO aluno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                AlunoModel.InserirDB(aluno);

                return Ok(AlunoModel.listaAlunosDB());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        // PUT: api/Aluno/5
        public IHttpActionResult Put([FromBody]AlunoDTO aluno)
        {
            try
            {
                AlunoModel.AtualizarDB(aluno);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        // DELETE: api/Aluno/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                AlunoModel.DeletarDB(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
