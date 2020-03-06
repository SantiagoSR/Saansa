using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Saansa.Modelos;
using SQLite;
namespace Saansa
{
    public class Repositorio
    {
        private readonly SQLiteAsyncConnection conn;

        public string StatusMessage { get; set; }

        public Repositorio(string dbPath)
        {
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<Articulo>().Wait();

        }

        public async Task CrearArticulo(Articulo articulo) 
        {
            try
            {
                if (string.IsNullOrWhiteSpace(articulo.Name))
                    throw new Exception("Name is required");

                var resultado = await conn.InsertOrReplaceAsync(articulo).ConfigureAwait(continueOnCapturedContext: false);
                StatusMessage = $"{resultado} añadiendo articulo [Nombre articulo: {articulo.Name}]";
            }
            catch (Exception ex){
                StatusMessage = $"Fallo al crear articulo: {articulo.Name}. Error: {ex.Message}";
            }
        }

        public Task<List<Articulo>> GetAllItems()
        {
            // Return a list of items saved to the Item table in the database.
            return conn.Table<Articulo>().ToListAsync();
        }
    }
}
