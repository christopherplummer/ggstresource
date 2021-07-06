using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using GgstResource.Api.Models.Request;
using GgstResource.Api.Postgres.SqlStrings;
using GgstResource.Models;
using Npgsql;
using NpgsqlTypes;

namespace GgstResource.Api.Postgres
{
    public static class CharacterCommandHelper
    {
        public static NpgsqlCommand GetAll(NpgsqlConnection connection)
        {
            return new(CharacterSql.GetAll, connection);
        }
        
        public static NpgsqlCommand GetByReference(NpgsqlConnection connection, string reference)
        {
            var command = new NpgsqlCommand(CharacterSql.GetById, connection);
            command.Parameters.Add(Constants.Reference, NpgsqlDbType.Varchar).Value = reference;
            return command;
        }
        
        public static NpgsqlCommand Create(NpgsqlConnection connection, Character character)
        {
            var command = new NpgsqlCommand(CharacterSql.Create, connection);
            command.Parameters.Add(Constants.Reference, NpgsqlDbType.Varchar).Value = character.Reference;
            command.Parameters.Add(Constants.Data, NpgsqlDbType.Json).Value = JsonSerializer.Serialize(character);
            return command;
        }
        
        public static NpgsqlCommand Update(NpgsqlConnection connection)
        {
            return new(CharacterSql.GetAll, connection);
        }
    }
}