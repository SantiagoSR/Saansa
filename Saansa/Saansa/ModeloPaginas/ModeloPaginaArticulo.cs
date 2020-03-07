using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using FreshMvvm;
using Saansa.Modelos;
using Xamarin.Forms;

namespace Saansa.ModeloPaginas
{
    class ModeloPaginaArticulo : FreshBasePageModel
    {
        private Repositorio _repositorio = FreshIOC.Container.Resolve<Repositorio>();


        private Articulo _articulo;

        public string NombreArticulo
        {

            get { return _articulo.Name; }
            set { _articulo.Name = value; RaisePropertyChanged(); }


        }
        public string CodigoBarraArticulo
        {
            get { return _articulo.Barcode; }

            set { _articulo.Barcode = value; RaisePropertyChanged(); }

        }

        public int ItemQuantity
        {
            get { return _articulo.Quantity; }
            set { _articulo.Quantity = value; RaisePropertyChanged(); }
        }

        public override void Init(object initData)
        {
            _articulo = initData as Articulo;
            if (_articulo == null) _articulo = new Articulo();
            base.Init(initData);
            RaisePropertyChanged(nameof(NombreArticulo));
            RaisePropertyChanged(nameof(CodigoBarraArticulo));
        }

        public ICommand SaveCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (_articulo.IsValid())
                    {
                        await _repositorio.CrearArticulo(_articulo);
                        await CoreMethods.PopPageModel(_articulo);
                    }
                });
            }

        }
    }
}
