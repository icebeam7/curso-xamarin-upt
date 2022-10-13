using SQLite;

using ProductosApp.Helpers;

namespace ProductosApp.Models
{
    [Table(Constantes.TablaProductos)]
    public class Producto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string ReleaseDate { get; set; }
        public double OriginalPrice { get; set; }
        public double Discount { get; set; }
        public string PictureUrl { get; set; }

        [Ignore]
        public double RealPrice { get => OriginalPrice * (1 - Discount / 100); }
    }
}
