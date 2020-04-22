using System;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using ZXing;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

using Xamarin.Forms;

namespace Saansa.Views
{
    public class ScannerModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public INavigation Navigation { get; set; }

        public ICommand ScannerCommand { get; set; }

        private string textoCodigo;

        public string TextoCodigo
        {
            get => textoCodigo;
            set
            {
                textoCodigo = value;
                OnPropertyChanged();
            }
        }


        public ScannerModel(INavigation navigation)
        {
            Navigation = navigation;
            ScannerCommand = new Command(async () => await ScanCode());
            textoCodigo = "";
        }

        async Task ScanCode()
        {
            //Espera 10 segundos después de un scan para volver a escanear
            var scanningOptions = new MobileBarcodeScanningOptions { DelayBetweenContinuousScans = 10000 };
            //Formatos posibles de código (solamente será de tipo QR)
            scanningOptions.PossibleFormats = new List<BarcodeFormat>()
            {
                ZXing.BarcodeFormat.QR_CODE
            };

            var overlay = new ZXingDefaultOverlay
            {
                ShowFlashButton = false,
                TopText = "Enfoca el código QR con la cámara",
                Opacity = 0.7
            };
            overlay.BindingContext = overlay;

            var scannerPage = new ZXingScannerPage(scanningOptions, overlay)
            {
                Title = "Escáner",
                DefaultOverlayShowFlashButton = true,
            };

            await Navigation.PushAsync(scannerPage);

            scannerPage.OnScanResult += (scanResult) =>
            {
                scannerPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    NuevaVenta.crearListaConQR(scanResult.Text);
                    await Navigation.PopAsync();
                    //Ir al carrito de ventas
                    await Navigation.PushAsync(new CarritoDeVentas());
                });
            };
        }
    }
}
