namespace ProductosApp.Models
{
    public class Producto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ReleaseDate { get; set; }
        public double OriginalPrice { get; set; }
        public double Discount { get; set; }
        public string PictureUrl { get; set; }
        public double RealPrice { get => OriginalPrice * (1 - Discount / 100); }
    }
}
