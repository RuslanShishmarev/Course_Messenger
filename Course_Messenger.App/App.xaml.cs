using Course_Messenger.App.Models;

namespace Course_Messenger.App
{
    public partial class App : Application
    {
        public static AuthToken Token { get; set; }

        public static User CurrentUser { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
