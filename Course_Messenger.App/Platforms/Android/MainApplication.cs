using Android.App;
using Android.Runtime;

namespace Course_Messenger.App
{
    //#if DEBUG
    //    [Application(AllowBackup = false, Debuggable = true, UsesCleartextTraffic = true)]  // connect to local service
    //#else                                       // on the host for debugging,
    //    [Application] 
    //#endif
    [Application(AllowBackup = false, Debuggable = true, UsesCleartextTraffic = true)]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
