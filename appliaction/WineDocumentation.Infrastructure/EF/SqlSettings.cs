using System;

namespace WineDocumentation.Infrastructure.EF
{
    public class SqlSettings
    {
        public string ConnectionString { get; set; }
        public bool InMemory { get; set; }

        public SqlSettings(string connectionString, bool inMemory = false)
        {
            ConnectionString = connectionString;
            InMemory = inMemory;
        }
      
    }
}