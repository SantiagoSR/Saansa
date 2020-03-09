using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Saansa
{
    public class SQLiteHelper
    {
        SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Modelos.Articulo>().Wait();
        }

        //Insert and Update new record
        public Task<int> SaveItemAsync(Modelos.Articulo articulo)
        {
            if (articulo.Cantidad != 0)
            {
                return db.UpdateAsync(articulo);
            }
            else
            {
                return db.InsertAsync(articulo);
            }
        }

        //Delete
        public Task<int> DeleteItemAsync(Modelos.Articulo articulo)
        {
            return db.DeleteAsync(articulo);
        }



        //Read All Items
        public Task<List<Modelos.Articulo>> GetItemsAsync()
        {
            return db.Table<Modelos.Articulo>().ToListAsync();
        }

        //Read Item //INT
        public Task<Modelos.Articulo> GetItemAsync(string cantidad)
        {
            return db.Table<Modelos.Articulo>().Where(i => i.Cantidad.Equals(cantidad)).FirstOrDefaultAsync();
        }
    }
}
