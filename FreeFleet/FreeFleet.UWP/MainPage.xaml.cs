namespace FreeFleet.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new FreeFleet.App());
        }
    }
}
