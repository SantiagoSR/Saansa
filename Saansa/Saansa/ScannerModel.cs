using System;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;

using ZXing;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

using Xamarin.Forms;

namespace Saansa
{
    public class ScannerModel
    {
        public INavigation Navigation { get; set; }

        public ICommand ScannerCommand { get; set; }

        public ScannerModel(INavigation navigation)
        {
            Navigation = navigation;
            ScannerCommand = new Command(async () => await ScanCode());
        }

        async Task ScanCode()
        {
            //Espera 10 segundos despu�s de un scan para volver a escanear
            var scanningOptions = new MobileBarcodeScanningOptions { DelayBetweenContinuousScans = 10000 };
            //Formatos posibles de c�digo (solamente ser� de tipo QR)
            scanningOptions.PossibleFormats = new List<BarcodeFormat>()
            {
                ZXing.BarcodeFormat.QR_CODE
            };

            var overlay = new ZXingDefaultOverlay
            {
                ShowFlashButton = false,
                TopText = "Enfoca el c�digo QR con la c�mara",
                Opacity = 0.5
            };
            overlay.BindingContext = overlay;

            var scannerPage = new ZXingScannerPage(scanningOptions, overlay)
            {
                Title = "Esc�ner",
                DefaultOverlayShowFlashButton = true,
            };

            await Navigation.PushAsync(scannerPage);

            scannerPage.OnScanResult += (scanResult) =>
            {
                scannerPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(() =>
                {
                    scannerPage.Title = $"{DateTime.Now.ToShortTimeString()} | C�digo: {scanResult.Text}";
                });
            };
        }
    }
}
