namespace GgstResource.Api.Postgres.SqlStrings
{
    public class CharacterSql
    {
        public const string GetAll = "SELECT * FROM public.characters;";
        public const string GetById = "SELECT * FROM public.characters WHERE reference = @reference;";
        public const string Create = "INSERT INTO public.characters (reference, data) VALUES (@reference, @data);";
        public const string Update = "UPDATE public.characters SET data = @data, version = (version + 1) WHERE reference = @reference;";
    }
}