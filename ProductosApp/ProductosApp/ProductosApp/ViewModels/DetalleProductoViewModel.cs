using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using ProductosApp.Models;

namespace ProductosApp.ViewModels
{
    public class DetalleProductoViewModel : BaseViewModel
    {
        private Producto producto;

        public Producto Producto
        {
            get { return producto; }
            set { SetProperty(ref producto, value); }
        }

        public DetalleProductoViewModel(Producto producto)
        {
            this.producto = producto;
        }
    }
}
