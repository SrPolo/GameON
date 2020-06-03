using Prism;
using Prism.Ioc;
using GameON.Prism.ViewModels;
using GameON.Prism.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GameON.Common.Services;
using Syncfusion.Licensing;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace GameON.Prism
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            SyncfusionLicenseProvider.RegisterLicense("MjYzNzQ0QDMxMzgyZTMxMmUzMGxjVkQxOWpvb3lMQ2d2U1ZXTUN5UmQrc3dadWx0SUJGQ05WS3pxV1hJWk09");

            InitializeComponent();

            await NavigationService.NavigateAsync("/GameONMasterDetailPage/NavigationPage/VideoGamesPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IGeolocatorService, GeolocatorService>();
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<VideoGamesPage, VideoGamesPageViewModel>();
            containerRegistry.RegisterForNavigation<VideoGameDetailPage, VideoGameDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<RecoverPasswordPage, RecoverPasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<ChangePasswordPage, ChangePasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<MakeReviewPage, MakeReviewPageViewModel>();
            containerRegistry.RegisterForNavigation<GameONMasterDetailPage, GameONMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<MyReviewsPage, MyReviewsPageViewModel>();
            containerRegistry.RegisterForNavigation<MyGameListPage, MyGameListPageViewModel>();
            containerRegistry.RegisterForNavigation<ModifyUserPage, ModifyUserPageViewModel>();
            containerRegistry.RegisterForNavigation<GameReviewsPage, GameReviewsPageViewModel>();
            containerRegistry.RegisterForNavigation<ReviewDetailsPage, ReviewDetailsPageViewModel>();
            containerRegistry.RegisterForNavigation<UsersPage, UsersPageViewModel>();
            containerRegistry.RegisterForNavigation<UserProfilePage, UserProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<GamingCentersPage, GamingCentersPageViewModel>();
            containerRegistry.RegisterForNavigation<PlayingGamesPage, PlayingGamesPageViewModel>();
            containerRegistry.RegisterForNavigation<PlayedGamesPage, PlayedGamesPageViewModel>();
            containerRegistry.RegisterForNavigation<PlantoPlayGamesPage, PlantoPlayGamesPageViewModel>();
        }
    }
}
