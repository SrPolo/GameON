using Xamarin.Forms;

namespace GameON.Prism.Views
{
    public partial class VideoGameDetailPage : ContentPage
    {
        public VideoGameDetailPage()
        {
            InitializeComponent();
            popup.PopupView.ShowFooter = false;
            popup.PopupView.HeaderTitle = "";
        }

    }
}
