using System.Threading.Tasks;
using System.Collections.Generic;

using SQLite;

using ProductosApp.Models;

namespace ProductosApp.Services
{
    public class ServicioBaseDatosLocal
    {
        private readonly SQLiteAsyncConnection conexion;
        private bool iniciado = false;

        public ServicioBaseDatosLocal(string rutaBD)
        {
            conexion = new SQLiteAsyncConnection(rutaBD);
        }

        private async Task Inicializar()
        {
            await conexion.CreateTableAsync<Producto>();

            iniciado = true;
        }

        // Select * From
        public async Task<List<T>> Obtener<T>() where T : new()
        {
            if (!iniciado)
                await Inicializar();

            var items = await conexion.Table<T>().ToListAsync();
            return items;
        }

        // Insert Into
        public async Task<bool> Agregar<T>(T item) where T : new()
        {
            if (!iniciado)
                await Inicializar();

            var op = await conexion.InsertAsync(item);
            return op == 1;
        }

        // Update... Set
        public async Task<bool> Actualizar<T>(T item) where T : new()
        {
            if (!iniciado)
                await Inicializar();

            var op = await conexion.UpdateAsync(item);
            return op == 1;
        }

        // Delete From
        public async Task<bool> Eliminar<T>(T item) where T : new()
        {
            if (!iniciado)
                await Inicializar();

            var op = await conexion.DeleteAsync(item);
            return op == 1;
        }
    }
}