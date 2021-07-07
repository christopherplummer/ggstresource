using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text.Json;
using System.Threading.Tasks;
using GgstResource.DataAccess.Interfaces;
using GgstResource.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace GgstResource.DataAccess
{
    public class Repository<T> : IRepository<T> where T : BaseResource
    {
        private readonly string _connectionString;
        private CommandHelper<T> _commandHelper;

        public Repository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("GgstResource");
        }

        public async Task<List<T>> GetAll()
        {
            var resources = new List<T>();
            await using var connection = new NpgsqlConnection(_connectionString);
            try
            {
                await connection.OpenAsync();
                var command = _commandHelper.GetAll(connection);
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    if (!reader.HasRows) continue;
                    var resource = await ReadResource(reader, 2);
                    if (resource != null) resources.Add(resource);
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

        public Task<T> GetByReference(string reference)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> Create(T data)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> Update(string reference, long version, T data)
        {
            throw new System.NotImplementedException();
        }

        #region Public Helper Methods

        public void SupplyTableNameToCommandHelper(string tableName)
        {
            _commandHelper = new CommandHelper<T>(tableName);
        }

        #endregion

        #region Private Helper Methods

        private static async Task<T> ReadResource(DbDataReader reader, int ordinal)
        {
            return await reader.IsDBNullAsync(ordinal) ? null : JsonSerializer.Deserialize<T>(await reader.GetFieldValueAsync<string>(ordinal));
        }

        #endregion
        
    }
}