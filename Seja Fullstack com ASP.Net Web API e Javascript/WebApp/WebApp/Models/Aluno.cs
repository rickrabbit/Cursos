using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Aluno
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string telefone { get; set; }
        public int ra { get; set; }

        public static List<Aluno> listaAlunos()
        {
            var caminhoArquivo = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/base.json");

            var json = System.IO.File.ReadAllText(caminhoArquivo);

            var listaAlunos = JsonConvert.DeserializeObject<List<Aluno>>(json);

            return listaAlunos;
        }

        public static List<Aluno> listaAlunosDB()
        {
            AlunoDAO alunoDB = new AlunoDAO();

            return alunoDB.listaAlunosDB();
        }

        public static bool ReescreverArquivo(List<Aluno> listaAlunos)
        {
            var caminhoArquivo = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/base.json");

            var json = JsonConvert.SerializeObject(listaAlunos, Formatting.Indented);
            System.IO.File.WriteAllText(caminhoArquivo, json);

            return true;
        }

        public static Aluno Inserir(Aluno aluno)
        {
            var listaAluno = Aluno.listaAlunos();

            var maxId = listaAluno.Max(m => m.id);
            aluno.id = maxId + 1;
            listaAluno.Add(aluno);

            Aluno.ReescreverArquivo(listaAluno);

            return aluno;
        }

        public static Aluno Atualizar(Aluno aluno)
        {
            var listaAluno = Aluno.listaAlunos();

            var itemIndex = listaAluno.FindIndex(f => f.id == aluno.id);

            if (itemIndex >= 0)
            {
                listaAluno[itemIndex] = aluno;
            }
            else
            {
                return null;
            }

            ReescreverArquivo(listaAluno);
            return aluno;
        }

        public static bool Deletar(int id)
        {
            var listaAluno = Aluno.listaAlunos();

            var itemIndex = listaAluno.FindIndex(f => f.id == id);

            if (itemIndex >= 0)
            {
                listaAluno.RemoveAt(itemIndex);
            }
            else
            {
                return false;
            }

            ReescreverArquivo(listaAluno);
            return true;
        }




    }
}