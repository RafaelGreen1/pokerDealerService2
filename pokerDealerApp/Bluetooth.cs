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
namespace pokerDealerApp
{
    public class Bluetooth
    {
        public DeviceInformationCollection deviceCollection;
        public DeviceInformation selectedDevice;
        public RfcommDeviceService deviceService;
        public string deviceName = "SER"; // Specify the device name to be selected; You can find the device name from the webb under bluetooth 
        public StreamSocket streamSocket = new StreamSocket();


        public async Task<string> ConnectToArduino()
        {
            try
            {
                string device1 = RfcommDeviceService.GetDeviceSelector(RfcommServiceId.SerialPort);
                deviceCollection = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(device1);

            }
            catch (Exception exception)
            {
                return exception.Message;
            }
            foreach (var item in deviceCollection)
            {
                if (item.Name == deviceName)
                {
                    selectedDevice = item;
                    break;
                }
            }

            if (selectedDevice == null)
            {
                return "Cannot find the device specified; Please check the device name";
            }
            else
            {
                deviceService = await RfcommDeviceService.FromIdAsync(selectedDevice.Id);

                if (deviceService != null)
                {
                    //connect the socket   
                    try
                    {
                        await streamSocket.ConnectAsync(deviceService.ConnectionHostName, deviceService.ConnectionServiceName);
                    }
                    catch (Exception ex)
                    {
                        return "Cannot connect bluetooth device:" + ex.Message;
                    }

                }
                else
                {
                    return "Didn't find the specified bluetooth device.";
                }
            }
            return "";
        
        }
        public async Task<string> sendData(string sendData)
        {
            if (deviceService != null)
            {
                //send data
                if (string.IsNullOrEmpty(sendData))
                {
                    return "No data!";
                }
                else
                {
                    DataWriter dwriter = new DataWriter(streamSocket.OutputStream);
                    UInt32 len = dwriter.MeasureString(sendData);
                    dwriter.WriteUInt32(len);
                    dwriter.WriteString(sendData);
                    await dwriter.StoreAsync();
                    await dwriter.FlushAsync();
                    return "";
                }

            }
            else
            {
                return "Bluetooth is not connected correctly!";
            }

        }
    }
}
