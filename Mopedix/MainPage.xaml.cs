using Microsoft.Maui.Controls;
using Plugin.BLE.Abstractions.Contracts;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Mopedix
{
    public partial class MainPage : ContentPage
    {
        private readonly BleService? _bleService;

        public ObservableCollection<IDevice> Devices { get; private set; }

        // Konstruktor bez parametru
        public MainPage()
        {
            InitializeComponent();
            Devices = new ObservableCollection<IDevice>();
            BindingContext = this;
        }

        // Konstruktor s BleService parametrem
        public MainPage(BleService bleService)
        {
            InitializeComponent();
            _bleService = bleService;
            Devices = _bleService.Devices;
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Žádost o oprávnění pro přístup k lokaci (Bluetooth)
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    // Zobrazit chybu nebo upozornit uživatele, že oprávnění nejsou udělena
                    await DisplayAlert("Chyba", "Aplikace potřebuje oprávnění pro přístup k lokaci", "OK");
                    return;
                }
            }

            if (_bleService != null)
            {
                // Spustit skenování pro zařízení
                await _bleService.ScanForDevicesAsync();
                DevicesListView.ItemsSource = Devices; // Ujistěte se, že ListView je aktualizován
            }
        }
    }
}
