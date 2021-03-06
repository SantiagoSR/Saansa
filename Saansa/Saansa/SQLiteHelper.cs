﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            db.CreateTableAsync<Modelos.ArticuloCarrito>().Wait();
        }

        //Insert and Update new record
        public Task<int> SaveItemAsyncCarrito(Modelos.ArticuloCarrito aCarrito)
        {   //articulo.Cantidad
            if (aCarrito.Cantidad != 0)
            {
                //probando esto 
                //var nuevoArticulo = GetItemAsync(articulo.Producto);
                return db.UpdateAsync(aCarrito);
            }
            else
            {
                return db.InsertAsync(aCarrito);
            }
        }

        public Task<int> SaveItemAsync(Modelos.Articulo articulo)
        {   

            if (articulo.Ayudante != 0)
            {
                

                return db.UpdateAsync(articulo);
            }
            else
            {
                return db.InsertAsync(articulo);
            }
        }

        public Task<int> DeleteItemAsuncCarrito(Modelos.ArticuloCarrito aCarrito)
        {
            return db.DeleteAsync(aCarrito);
        }

        //Delete
        public Task<int> DeleteItemAsync(Modelos.Articulo articulo)
        {
            return db.DeleteAsync(articulo);
        }

        public Task<List<Modelos.ArticuloCarrito>> GetItemsAsyncCarrito()
        {
            return db.Table<Modelos.ArticuloCarrito>().ToListAsync();
        }
        //Read All Items
        public Task<List<Modelos.Articulo>> GetItemsAsync()
        {
            return db.Table<Modelos.Articulo>().ToListAsync();
        }

        //Read Item //INT
        public Task<Modelos.ArticuloCarrito> GetItemAsyncCarrito(string nombre)
        {
            return db.Table<Modelos.ArticuloCarrito>().Where(i => i.Producto.Equals(nombre)).FirstOrDefaultAsync();
        }

        public Task<Modelos.Articulo> GetItemAsync(string nombre)
        {
            return db.Table<Modelos.Articulo>().Where(i => i.Producto.Equals(nombre)).FirstOrDefaultAsync();
        }

        public Task<Modelos.Articulo> GetItemAsyncCodigo(string id)
        {
            return db.Table<Modelos.Articulo>().Where(i => i.Id.Equals(id)).FirstOrDefaultAsync();
        }
        public Task<List<Modelos.Articulo>> GetItemsAsyncCategory(string category)
        {
            return db.Table<Modelos.Articulo>().Where(i => i.MasterCategory.Equals(category)).ToListAsync();

        }

    }
}