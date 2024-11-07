using Plugin.BLE.Abstractions.Contracts;
using System.Collections.ObjectModel;

namespace Mopedix
{
    public partial class MainPage : ContentPage
    {
        private readonly BleService _bleService;

        public ObservableCollection<IDevice> Devices { get; private set; }

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
            await _bleService.ScanForDevicesAsync();
        }
    }
}
