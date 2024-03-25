﻿using Dapper;
using RNT_APIREST_LONDON.Entities;
using RNT_APIREST_LONDON.Response;
using Sap.Data.Hana;

namespace RNT_APIREST_LONDON.Repositorios
{
    public interface IEtapaRepository
    {
        Task<GenericoDtoResponse> ObtenerAsync();
    }

    public class EtapaRepository : IEtapaRepository
    {
        private readonly string? connectionString;
        private readonly IConfiguration _configuration;

        public EtapaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<GenericoDtoResponse> ObtenerAsync()
        {
            var response = new GenericoDtoResponse();
            IEnumerable<Generico>? listaLineas;
            string query = string.Empty;

            try
            {
                query = "select \"Code\" as \"Codigo\",\"Name\" as \"Descripcion\" from \"@RNT_EDAD\"";
                using var connection = new HanaConnection(connectionString);

                listaLineas = await connection.QueryAsync<Generico>(query);
                response.Success = true;
                response.Listado = listaLineas.ToList();

            }

            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = $"Error al obtener las Etapas: {ex.Message}.";
                throw;
            }

            return response;
        }
    }
}