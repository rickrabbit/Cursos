using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class AlunoDAO
    {
        private string stringConexao = ConfigurationManager.AppSettings["ConnectionStringSql"];
        private IDbConnection conexao;

        public AlunoDAO()
        {
            conexao = new SqlConnection(stringConexao);

            conexao.Open();
        }

        public List<Aluno> listaAlunosDB()
        {
            IDbCommand comando = conexao.CreateCommand();
            comando.CommandText = "Select * from Alunos";

            IDataReader resultado = comando.ExecuteReader();

            List<Aluno> listaAlunos = new List<Aluno>();

            while (resultado.Read())
            {
                var aluno = new Aluno();

                aluno.id = Convert.ToInt32(resultado["Id"]);
                aluno.nome = resultado["nome"].ToString();
                aluno.sobrenome = resultado["sobrenome"].ToString();
                aluno.telefone = resultado["telefone"].ToString();
                aluno.ra = Convert.ToInt32(resultado["ra"]);

                listaAlunos.Add(aluno);
            }

            conexao.Close();

            return listaAlunos;
        }


    }
}