using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text.Json;
using System.Threading.Tasks;
using GgstResource.Api.Interfaces;
using GgstResource.Api.Models.Request;
using GgstResource.Api.Postgres;
using GgstResource.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace GgstResource.Api.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly string _connectionString;

        public CharacterRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("GgstResource");
        }
        
        public async Task<List<Character>> GetAll()
        {
            var connection = new NpgsqlConnection(_connectionString);
            try
            {
                await connection.OpenAsync();
                var command = CharacterCommandHelper.GetAll(connection);
                var response = new List<Character>();
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    if (!reader.HasRows) continue;
                    var character = await ReadCharacter(reader, 2);
                    response.Add(character);
                }

                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        public async Task<Character> GetByReference(string reference)
        {
            var connection = new NpgsqlConnection(_connectionString);
            try
            {
                var character = new Character();
                await connection.OpenAsync();
                var command = CharacterCommandHelper.GetByReference(connection, reference);
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    if (!reader.HasRows) continue;
                    character = await ReadCharacter(reader, 2);
                }

                return character;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        public async Task<Character> Create(CharacterCreateRequest request)
        {
            var connection = new NpgsqlConnection(_connectionString);
            try
            {
                var tempCharacter = new Character { Name = request.Name, Reference = request.Reference};
                await connection.OpenAsync();
                var command = CharacterCommandHelper.Create(connection, tempCharacter);
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    if (!reader.HasRows) continue;
                    tempCharacter = await ReadCharacter(reader, 0);
                }

                return tempCharacter;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        public Task<Character> Update(string reference, long version, CharacterUpdateRequest request)
        {
            throw new System.NotImplementedException();
        }

        private static async Task<Character> ReadCharacter(DbDataReader reader, int ordinal)
        {
            return await reader.IsDBNullAsync(ordinal) ? null : JsonSerializer.Deserialize<Character>(await reader.GetFieldValueAsync<string>(ordinal));
        }
    }
}