using Bet.API.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;


namespace Bet.API.Controllers
{
    [Route("api/times")]
    public class TimesController : ControllerBase
    {
        private readonly string _connectionString;


        public TimesController()
        {
            _connectionString = "Host=localhost;Port=5432;Username=postgres;Password=pg1234;Database=oficina_net;";
        }



        //api/times?query=gremio
        [HttpGet]
        public IActionResult ListarTodosTimes(string query)
        {
            //Buscar todos ou filtrar

            var times = new List<TimeModel>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sqlQuery = "";

            // Verifica se a query string não está vazia
            if (!string.IsNullOrEmpty(query))
            {
                sqlQuery = $@"SELECT codigo, nome, cidade, estado, dta_fundacao, qtd_socios, nome_presidente, nome_estadio, nome_treinador, site, ind_ativo FROM times WHERE nome LIKE '%{query}%'";
            }
            else
            {
                sqlQuery = "SELECT codigo, nome, cidade, estado, dta_fundacao, qtd_socios, nome_presidente, nome_estadio, nome_treinador, site, ind_ativo FROM times";
            }

            using var cmd = new NpgsqlCommand(sqlQuery, connection);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                times.Add(new TimeModel
                {
                    Codigo = reader.GetInt32(reader.GetOrdinal("codigo")),
                    Nome = reader.GetString(reader.GetOrdinal("nome")),
                    Cidade = reader.GetString(reader.GetOrdinal("cidade")),
                    Estado = reader.GetString(reader.GetOrdinal("estado")),
                    DataFundacao = reader.GetDateTime(reader.GetOrdinal("dta_fundacao")),
                    QuantidadeSocios = reader.GetInt32(reader.GetOrdinal("qtd_socios")),
                    NomePresidente = reader.GetString(reader.GetOrdinal("nome_presidente")),
                    NomeEstadio = reader.GetString(reader.GetOrdinal("nome_estadio")),
                    NomeTreinador = reader.GetString(reader.GetOrdinal("nome_treinador")),
                    Site = reader.GetString(reader.GetOrdinal("site")),
                    IndAtivo = reader.GetBoolean(reader.GetOrdinal("ind_ativo"))
                });
            }

            connection.Close();

            return Ok(times);
        }


        //api/times/1
        [HttpGet("{codigo}")]
        public IActionResult ListarTimeById(int codigo)
        {
            //Buscar o time pelo código
            var times = new List<TimeModel>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sqlQuery = $@"SELECT codigo, nome, cidade, estado, dta_fundacao, qtd_socios, nome_presidente, nome_estadio, nome_treinador, site, ind_ativo FROM times WHERE codigo = {codigo}";

            using var cmd = new NpgsqlCommand(sqlQuery, connection);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                times.Add(new TimeModel
                {
                    Codigo = reader.GetInt32(reader.GetOrdinal("codigo")),
                    Nome = reader.GetString(reader.GetOrdinal("nome")),
                    Cidade = reader.GetString(reader.GetOrdinal("cidade")),
                    Estado = reader.GetString(reader.GetOrdinal("estado")),
                    DataFundacao = reader.GetDateTime(reader.GetOrdinal("dta_fundacao")),
                    QuantidadeSocios = reader.GetInt32(reader.GetOrdinal("qtd_socios")),
                    NomePresidente = reader.GetString(reader.GetOrdinal("nome_presidente")),
                    NomeEstadio = reader.GetString(reader.GetOrdinal("nome_estadio")),
                    NomeTreinador = reader.GetString(reader.GetOrdinal("nome_treinador")),
                    Site = reader.GetString(reader.GetOrdinal("site")),
                    IndAtivo = reader.GetBoolean(reader.GetOrdinal("ind_ativo"))
                });
            }

            connection.Close();

            if(times.Count > 0)
            {
                return Ok(times);
            }
            else
            {
                return NotFound();
            }
        }

        //FromBody - Corpo da requisição
        [HttpPost]
        public ActionResult InserirTime([FromBody] TimeModel time)
        {
            //Validando os campos pelo modelo
            if (!ModelState.IsValid)
            {
                //Se não for válido, retorna um erro 400 Bad Request com os detalhes dos erros de validação
                return BadRequest(ModelState);
            }


            //Validando campo a campo (caso necessário)
            if (time.Nome.Length > 50)
            {
                return BadRequest();
            }

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            using var cmd = new NpgsqlCommand($@"INSERT INTO times (nome, cidade, estado, dta_fundacao, qtd_socios, nome_presidente, nome_estadio, nome_treinador, site, ind_ativo) 
                                    VALUES (@nome, @cidade, @estado, @dta_fundacao, @qtd_socios, @nome_presidente, @nome_estadio, @nome_treinador, @site, @ind_ativo)
                                    RETURNING codigo", connection);
            cmd.Parameters.AddWithValue("nome", time.Nome);
            cmd.Parameters.AddWithValue("cidade", time.Cidade);
            cmd.Parameters.AddWithValue("estado", time.Estado);
            cmd.Parameters.AddWithValue("dta_fundacao", time.DataFundacao);
            cmd.Parameters.AddWithValue("qtd_socios", time.QuantidadeSocios);
            cmd.Parameters.AddWithValue("nome_presidente", time.NomePresidente);
            cmd.Parameters.AddWithValue("nome_estadio", time.NomeEstadio);
            cmd.Parameters.AddWithValue("nome_treinador", time.NomeTreinador);
            cmd.Parameters.AddWithValue("site", time.Site);
            cmd.Parameters.AddWithValue("ind_ativo", time.IndAtivo);

            //Busca o código gerado
            int codigo = (int)cmd.ExecuteScalar();

            time.Codigo = codigo;

            connection.Close();

            //Param: 1 - API que tem os detalhes do objeto, 2 - Objeto anônimo com o parâmetro que a API retorna, 3 - O objeto cadastrado
            return CreatedAtAction(nameof(ListarTimeById), new { codigo = time.Codigo }, time);
        }

        //api/times/1
        [HttpPut("{codigo}")]
        public ActionResult AtualizarTime(int codigo, [FromBody] UpdateTimeModel time)
        {
            if (time.Nome.Length > 60)
            {
                return BadRequest();
            }            

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            using var cmd = new NpgsqlCommand("UPDATE times SET nome = @nome, qtd_socios = @qtd_socios, nome_presidente = @nome_presidente, nome_treinador = @nome_treinador, ind_ativo = @ind_ativo WHERE codigo = @codigo", connection);
            cmd.Parameters.AddWithValue("codigo", codigo);
            cmd.Parameters.AddWithValue("nome", time.Nome);
            cmd.Parameters.AddWithValue("qtd_socios", time.QuantidadeSocios);
            cmd.Parameters.AddWithValue("nome_presidente", time.NomePresidente);
            cmd.Parameters.AddWithValue("nome_treinador", time.NomeTreinador);
            cmd.Parameters.AddWithValue("ind_ativo", time.IndAtivo);
            var rowsAffected = cmd.ExecuteNonQuery();

            connection.Close();

            if (rowsAffected == 0)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(ListarTimeById), new { codigo = codigo }, time);
        }


        //api/times/1
        [HttpDelete("{codigo}")]
        public ActionResult DeletarTime(int codigo)
        {
            //Buscar, se não existir, retorna NotFound

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            using var cmd = new NpgsqlCommand("DELETE FROM times WHERE codigo = @codigo", connection);
            cmd.Parameters.AddWithValue("codigo", codigo);
            var rowsAffected = cmd.ExecuteNonQuery();

            connection.Close();

            if (rowsAffected == 0)
            {
                return NotFound();
            }

            return Ok();
        }


    }
}
