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
        // GET: api/Aluno
        public IEnumerable<Aluno> Get()
        {
            return Aluno.listaAlunos();
        }

        // GET: api/Aluno/5
        public Aluno Get(int id)
        {
            return Aluno.listaAlunos().Where(w=> w.id == id).FirstOrDefault();
        }

        // POST: api/Aluno
        public List<Aluno> Post([FromBody]Aluno aluno)
        {
            Aluno.Inserir(aluno);

            return Aluno.listaAlunos();
        }

        // PUT: api/Aluno/5
        public void Put([FromBody]Aluno aluno)
        {
            Aluno.Atualizar(aluno);
        }

        // DELETE: api/Aluno/5
        public void Delete(int id)
        {
            Aluno.Deletar(id);
        }
    }
}
