using System.Collections.Generic;
using System.Text.Json;
using GgstResource.Models;
using Npgsql;
using NpgsqlTypes;

namespace GgstResource.DataAccess
{
    public class CommandHelper<T> where T : BaseResource
    {
        private readonly SqlStrings _sqlStrings;
        
        public CommandHelper(string table)
        {
            _sqlStrings = new SqlStrings(table);
        }
        public NpgsqlCommand GetAll(NpgsqlConnection connection)
        {
            return new(_sqlStrings.GetAll, connection);
        }
        
        public NpgsqlCommand GetByReference(string reference, NpgsqlConnection connection)
        {
            var command = new NpgsqlCommand(_sqlStrings.GetById, connection);
            command.Parameters.Add(Constants.Reference, NpgsqlDbType.Varchar).Value = reference;
            return command;
        }
        
        public NpgsqlCommand Create(T data, NpgsqlConnection connection)
        {
            var command = new NpgsqlCommand(_sqlStrings.Create, connection);
            command.Parameters.Add(Constants.Reference, NpgsqlDbType.Varchar).Value = data.Reference;
            command.Parameters.Add(Constants.Data, NpgsqlDbType.Json).Value = JsonSerializer.Serialize(data);
            return command;
        }
        
        public NpgsqlCommand Update(T data, NpgsqlConnection connection)
        {
            return new(_sqlStrings.GetAll, connection);
        }
    }
}