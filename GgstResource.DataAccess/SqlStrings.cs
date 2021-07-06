namespace GgstResource.DataAccess
{
    public class SqlStrings
    {
        public SqlStrings(string table)
        {
            GetAll = $"SELECT * FROM public.{table};";
            GetById = $"SELECT * FROM public.{table} WHERE reference = @reference;";
            Create = $"INSERT INTO public.{table} (reference, data) VALUES (@reference, @data) RETURNING data;";
            Update = $"UPDATE public.{table} SET data = @data, version = (version + 1) WHERE reference = @reference;";
        }
        public string GetAll { get;  }
        public string GetById { get; }
        public string Create { get;  }
        public string Update { get;  }
    }
}