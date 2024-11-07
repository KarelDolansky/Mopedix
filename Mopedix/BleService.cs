using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

public class BleService
{
    public ObservableCollection<IDevice> Devices { get; private set; }
    private readonly IAdapter _adapter;

    public BleService(IBluetoothLE bluetoothLE, IAdapter adapter)
    {
        _adapter = adapter;
        Devices = new ObservableCollection<IDevice>();

        _adapter.DeviceDiscovered += OnDeviceDiscovered;
    }

    private void OnDeviceDiscovered(object sender, DeviceEventArgs e)
    {
        if (!Devices.Contains(e.Device))
        {
            Devices.Add(e.Device);
        }
    }

    public async Task ScanForDevicesAsync()
    {
        Devices.Clear();
        await _adapter.StartScanningForDevicesAsync();
    }
}
