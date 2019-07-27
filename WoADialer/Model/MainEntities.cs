﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Calls;
using Internal.Windows.Calls;
using Windows.Devices.Enumeration;
using Windows.Devices.Sensors;
using Windows.Devices.Haptics;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.Calls.Background;
using Windows.UI.Notifications.Management;
using WoADialer.Background;
using Windows.UI.Notifications;

namespace WoADialer.Model
{
    internal static class MainEntities
    {
        public static bool Initialized { get; private set; }
        public static CallManager CallManager { get; private set; }
        public static PhoneLine DefaultLine { get; private set; }
        public static PhoneCallStore CallStore { get; private set; }
        public static PhoneCallHistoryStore CallHistoryStore { get; private set; }
        public static ProximitySensor ProximitySensor { get; private set; }
        public static ToastNotifier ToastNotifier { get; private set; }
        
        public static async Task Initialize()
        {
            if (!Initialized)
            {
                DeviceInformationCollection devices;
                CallManager = await CallManager.GetCallManagerAsync();
                CallStore = await PhoneCallManager.RequestStoreAsync();
                CallHistoryStore = await PhoneCallHistoryManager.RequestStoreAsync(PhoneCallHistoryStoreAccessType.AllEntriesReadWrite);
                devices = await DeviceInformation.FindAllAsync(ProximitySensor.GetDeviceSelector());
                ProximitySensor = devices.Count > 0 ? ProximitySensor.FromId(devices.First().Id) : null;
                ToastNotifier = ToastNotificationManager.CreateToastNotifier();
                await TaskManager.RegisterBackgroudTasks();
                try
                {
                    DefaultLine = await PhoneLine.FromIdAsync(await CallStore.GetDefaultLineAsync());
                }
                catch
                {

                }
                Initialized = true;
            }
        }
    }
}
