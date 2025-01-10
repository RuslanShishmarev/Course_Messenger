using Course_Messenger.App.Models;
using Course_Messenger.App.Services;

namespace Course_Messenger.App
{
    public partial class App : Application
    {
        public static AuthToken Token { get; set; }

        public static User CurrentUser { get; set; }

        public static HubChatService HubChatService { get; private set; }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            HubChatService = new HubChatService();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var wnd = base.CreateWindow(activationState);

            wnd.Destroying += async (s, e) =>
            {
                await HubChatService?.Stop();
            };

            return wnd;
        }
    }
}
