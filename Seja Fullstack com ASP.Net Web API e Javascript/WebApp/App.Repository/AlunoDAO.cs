using App.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace App.Repository
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

        public List<AlunoDTO> listaAlunosDB()
        {
            List<AlunoDTO> listaAlunos = new List<AlunoDTO>();

            try
            {
                IDbCommand comando = conexao.CreateCommand();
                comando.CommandText = "Select * from Alunos";

                IDataReader resultado = comando.ExecuteReader();

                while (resultado.Read())
                {
                    var aluno = new AlunoDTO()
                    {
                        id = Convert.ToInt32(resultado["Id"]),
                        nome = resultado["nome"].ToString(),
                        sobrenome = resultado["sobrenome"].ToString(),
                        telefone = resultado["telefone"].ToString(),
                        ra = Convert.ToInt32(resultado["ra"])
                    };

                    listaAlunos.Add(aluno);
                }

                return listaAlunos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        public void InserirAlunoDB(AlunoDTO aluno)
        {
            try
            {
                IDbCommand comando = conexao.CreateCommand();
                comando.CommandText = "Insert into Alunos (nome, sobrenome, telefone, ra) values (@nome, @sobrenome, @telefone, @ra)";

                IDbDataParameter paramNome = new SqlParameter("nome", aluno.nome);
                comando.Parameters.Add(paramNome);

                IDbDataParameter paramSobrenome = new SqlParameter("sobrenome", aluno.sobrenome);
                comando.Parameters.Add(paramSobrenome);

                IDbDataParameter paramTelefone = new SqlParameter("telefone", aluno.telefone);
                comando.Parameters.Add(paramTelefone);

                IDbDataParameter paramRa = new SqlParameter("ra", aluno.ra);
                comando.Parameters.Add(paramRa);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        public void AtualizarAlunoDB(AlunoDTO aluno)
        {
            try
            {
                IDbCommand comando = conexao.CreateCommand();
                comando.CommandText = "Update Alunos set nome = @nome, " +
                                                        "sobrenome = @sobrenome, " +
                                                        "telefone = @telefone, " +
                                                        "ra = @ra" +
                                       "where id = @id";

                IDbDataParameter paramId = new SqlParameter("id", aluno.id);
                comando.Parameters.Add(paramId);

                IDbDataParameter paramNome = new SqlParameter("nome", aluno.nome);
                comando.Parameters.Add(paramNome);

                IDbDataParameter paramSobrenome = new SqlParameter("sobrenome", aluno.sobrenome);
                comando.Parameters.Add(paramSobrenome);

                IDbDataParameter paramTelefone = new SqlParameter("telefone", aluno.telefone);
                comando.Parameters.Add(paramTelefone);

                IDbDataParameter paramRa = new SqlParameter("ra", aluno.ra);
                comando.Parameters.Add(paramRa);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        public void DeletarDB(int id)
        {
            try
            {
                IDbCommand comando = conexao.CreateCommand();
                comando.CommandText = "Delete from Alunos where id = @id";

                IDbDataParameter paramId = new SqlParameter("id", id);
                comando.Parameters.Add(paramId);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}