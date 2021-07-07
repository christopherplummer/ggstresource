using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text.Json;
using System.Threading.Tasks;
using GgstResource.Api.Interfaces;
using GgstResource.Api.Models.Request;
using GgstResource.Api.Postgres;
using GgstResource.DataAccess;
using GgstResource.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace GgstResource.Api.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly string _connectionString;
        private readonly CommandHelper<Character> _commandHelper;

        public CharacterRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("GgstResource");
            _commandHelper = new CommandHelper<Character>("character");
        }
        
        public async Task<List<Character>> GetAll()
        {
            var resources = new List<Character>();
            await using var connection = new NpgsqlConnection(_connectionString);
            try
            {
                await connection.OpenAsync();
                var command = CharacterCommandHelper.GetAll(connection);
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    if (!reader.HasRows) continue;
                    var character = await ReadCharacter(reader, 2);
                    if (character != null) resources.Add(character);
                }
                await reader.CloseAsync();
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

            return resources;
        }

        public async Task<Character> GetByReference(string reference)
        {
            Character resource = null;
            await using var connection = new NpgsqlConnection(_connectionString);
            try
            {
                await connection.OpenAsync();
                var command = CharacterCommandHelper.GetByReference(connection, reference);
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    if (!reader.HasRows) continue;
                    resource = await ReadCharacter(reader, 2);
                }
                await reader.CloseAsync();
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

            return resource;
        }

        public async Task<Character> Create(CharacterCreateRequest request)
        {
            Character resource = null;
            await using var connection = new NpgsqlConnection(_connectionString);
            try
            {
                var newCharacter = new Character { Name = request.Name, Reference = request.Reference, Version = 0, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow};
                await connection.OpenAsync();
                var command = CharacterCommandHelper.Create(connection, newCharacter);
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    if (!reader.HasRows) continue;
                    resource = await ReadCharacter(reader, 0);
                }

                await reader.CloseAsync();
                return resource;
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