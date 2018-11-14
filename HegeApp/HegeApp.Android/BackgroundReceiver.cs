using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

namespace HegeApp.Droid
{
    [BroadcastReceiver]
    class BackgroundReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            PowerManager pm = (PowerManager)context.GetSystemService(Context.PowerService);
            PowerManager.WakeLock wakelock = pm.NewWakeLock(WakeLockFlags.Partial, "BackgroundReceiver");
            wakelock.Acquire();

            int i = 0;
            MessagingCenter.Send<object, string>(this, "UpdateLabel", "Hello from Android " + i);
            i++;

            wakelock.Release();
        }
    }
}