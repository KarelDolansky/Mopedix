using Plugin.BLE.Abstractions.Contracts;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

public class BleService
{
    private readonly IBluetoothLE _bluetoothLE;
    private readonly IAdapter _adapter;

    public ObservableCollection<IDevice> Devices { get; private set; }

    public BleService(IBluetoothLE bluetoothLE, IAdapter adapter)
    {
        _bluetoothLE = bluetoothLE;
        _adapter = adapter;
        Devices = new ObservableCollection<IDevice>();
    }

    public async Task ScanForDevicesAsync()
    {
        if (_bluetoothLE.State == BluetoothState.On)
        {
            _adapter.DeviceDiscovered += (s, a) => Devices.Add(a.Device);
            await _adapter.StartScanningForDevicesAsync();
        }
    }
}
