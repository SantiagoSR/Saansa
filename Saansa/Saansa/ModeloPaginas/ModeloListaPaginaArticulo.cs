using System;
using System.Collections.Generic;
using System.Text;
using FreshMvvm;
using Saansa.Modelos;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Saansa.ModeloPaginas
{
    public class ModeloListaPaginaArticulo : FreshBasePageModel
    {
        private Repositorio _repositorio = FreshIOC.Container.Resolve<Repositorio>();
        private Articulo _selectedItem = null;


        public ObservableCollection<Articulo> Articulos { get; private set; }

        public Articulo ArticuloSeleccionado
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                if (value != null) EditItemCommand.Execute(value);
            }
        }

        public ModeloListaPaginaArticulo()
        {
            Articulos = new ObservableCollection<Articulo>();
        }

        public override void Init(object initData)
        {
            CargarArticulos();
            if (Articulos.Count() < 1)
            {
                CrearPrueba();
            }
        }

        public override void ReverseInit(object returnedData)
        {
            CargarArticulos();
            base.ReverseInit(returnedData);
        }

        public ICommand AddItemCommand
        {
            get
            {
                return new Command(async () => {
                    await CoreMethods.PushPageModel<ModeloPaginaArticulo>();
                });
            }
        }

        public ICommand EditItemCommand
        {
            get
            {
                return new Command(async (articulo) => {
                    await CoreMethods.PushPageModel<ModeloPaginaArticulo>(articulo);
                });
            }
        }

        private void CargarArticulos()
        {
            Articulos.Clear();
            Task<List<Articulo>> traerTareaArticulo = _repositorio.GetAllItems();
            traerTareaArticulo.Wait();
            foreach (var item in traerTareaArticulo.Result)
            {
                Articulos.Add(item);
            }
        }

        private void CrearPrueba()
        {
            var item1 = new Articulo
            {
                Name = "Milk",
                Barcode = "8001234567890",
                Quantity = 10
            };

            var item2 = new Articulo
            {
                Name = "Soup",
                Barcode = "8002345678901",
                Quantity = 5
            };

            var item3 = new Articulo
            {
                Name = "Water",
                Barcode = "8003456789012",
                Quantity = 20
            };

            var task1 = _repositorio.CrearArticulo(item1);
            var task2 = _repositorio.CrearArticulo(item2);
            var task3 = _repositorio.CrearArticulo(item3);

            // Don't proceed until all the async inserts are complete.
            var allTasks = Task.WhenAll(task1, task2, task3);
            allTasks.Wait();

            CargarArticulos();
        }
    }
}
