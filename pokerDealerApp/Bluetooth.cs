using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;

namespace pokerDealerApp
{
    public class Bluetooth
    {
        private SerialDevice serialPort = null;
        DataWriter dataWriteObject = null;

        public async Task<string> ConnectToArduino()
        {
            string deviceName = "Rafi";
            string deviceName2 = "SER";
            DeviceInformation selectedDevice;
            string deviceId = "";
            DeviceInformationCollection devices = await DeviceInformation.FindAllAsync();
            if (devices.Any())
            {
                foreach (var item in devices)
                {
                    if (item.Name == deviceName || item.Name == deviceName2)
                    {
                        selectedDevice = item;
                        deviceId = selectedDevice.Id;
                        break;
                    }
                }
                
               
                serialPort = await SerialDevice.FromIdAsync(deviceId);

                if (serialPort != null)
                {
                    serialPort.BaudRate = 9600;
                    serialPort.ReadTimeout = TimeSpan.FromMilliseconds(2000);
                    serialPort.WriteTimeout = TimeSpan.FromMilliseconds(2000);

                }
            }
            return "";

        }
        public async Task<string> sendData(string sendData)
        {
            dataWriteObject = new DataWriter(serialPort.OutputStream);
            dataWriteObject.WriteString(sendData);
            Task<UInt32> storeAsyncTask = dataWriteObject.StoreAsync().AsTask();
            UInt32 bytesWritten = await storeAsyncTask;
            dataWriteObject.DetachStream();
            dataWriteObject = null;
            return "";
        }
    }
}
