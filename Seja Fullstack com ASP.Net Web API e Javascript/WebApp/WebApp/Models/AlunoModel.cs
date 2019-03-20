using App.Domain;
using App.Repository;
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
    public class AlunoModel
    {
        public static List<AlunoDTO> listaAlunos()
        {
            var caminhoArquivo = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/base.json");

            var json = System.IO.File.ReadAllText(caminhoArquivo);

            var listaAlunos = JsonConvert.DeserializeObject<List<AlunoDTO>>(json);

            return listaAlunos;
        }

        public static List<AlunoDTO> listaAlunosDB()
        {
            AlunoDAO alunoDB = new AlunoDAO();

            return alunoDB.listaAlunosDB();
        }

        public static bool ReescreverArquivo(List<AlunoDTO> listaAlunos)
        {
            var caminhoArquivo = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/base.json");

            var json = JsonConvert.SerializeObject(listaAlunos, Formatting.Indented);
            System.IO.File.WriteAllText(caminhoArquivo, json);

            return true;
        }

        public static AlunoDTO Inserir(AlunoDTO aluno)
        {
            var listaAluno = AlunoModel.listaAlunos();

            var maxId = listaAluno.Max(m => m.id);
            aluno.id = maxId + 1;
            listaAluno.Add(aluno);

            AlunoModel.ReescreverArquivo(listaAluno);

            return aluno;
        }

        public static void InserirDB(AlunoDTO aluno)
        {
            AlunoDAO alunoDB = new AlunoDAO();

            alunoDB.InserirAlunoDB(aluno);

        }

        public static AlunoDTO Atualizar(AlunoDTO aluno)
        {
            var listaAluno = AlunoModel.listaAlunos();

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

        public static void AtualizarDB(AlunoDTO aluno)
        {
            AlunoDAO alunoDB = new AlunoDAO();

            alunoDB.AtualizarAlunoDB(aluno);

        }

        public static bool Deletar(int id)
        {
            var listaAluno = AlunoModel.listaAlunos();

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

        public static void DeletarDB(int id)
        {
            AlunoDAO alunoDB = new AlunoDAO();

            alunoDB.DeletarDB(id);

        }


    }
}