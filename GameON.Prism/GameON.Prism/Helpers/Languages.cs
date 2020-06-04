using GameON.Prism.Interfaces;
using GameON.Prism.Resources;
using System.Globalization;
using Xamarin.Forms;

namespace GameON.Prism.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            CultureInfo ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            Culture = ci.Name;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        
        public static string Culture { get; set; }

        public static string UserUpdated => Resource.UserUpdated;
        public static string FavoriteVideoGamePlaceHolder => Resource.FavoriteVideoGamePlaceHolder;

        public static string AddedSuccess => Resource.AddedSuccess;

        public static string GameListEmpty => Resource.GameListEmpty;

        public static string Message => Resource.Message;

        public static string ReviewSuccess => Resource.ReviewSuccess;

        public static string ReviewError => Resource.ReviewError;

        public static string ScoreError => Resource.ScoreError;

        public static string EmailError => Resource.EmailError;

        public static string PasswordError => Resource.PasswordError;

        public static string LoginError => Resource.LoginError;

        public static string DocumentError => Resource.DocumentError;

        public static string FirstNameError => Resource.FirstNameError;
       
        public static string LastNameError => Resource.LastNameError;

        public static string PictureSource => Resource.PictureSource;
        
        public static string FromGallery => Resource.FromGallery;
        
        public static string FromCamera => Resource.FromCamera;

        public static string Error => Resource.Error;
        
        public static string ConnectionError => Resource.ConnectionError;
        
        public static string Accept => Resource.Accept;
        
        public static string AddtoList => Resource.AddtoList;

        public static string Cancel => Resource.Cancel;

        public static string Developers => Resource.Developers;

        public static string VideoGamesLabel => Resource.VideoGamesLabel;

        public static string Search => Resource.Search;

        public static string Explore => Resource.Explore;
        
        public static string MyGameList => Resource.MyGameList;
        
        public static string MyReviews => Resource.MyReviews;
        
        public static string ModifyUser => Resource.ModifyUser;
        
        public static string Login => Resource.Login;
        
        public static string Logout => Resource.Logout;
        
        public static string OrLoginLabel => Resource.OrLoginLabel;
        
        public static string Password => Resource.Password;
        
        public static string Email => Resource.Email;
        
        public static string ForgotPassword => Resource.ForgotPassword;
        
        public static string SignupLabel => Resource.SignupLabel;
        
        public static string Firstname => Resource.Firstname;
        
        public static string Lastname => Resource.Lastname;
        
        public static string Document => Resource.Document;
        
        public static string Register => Resource.Register;
        
        public static string Playing => Resource.Playing;
        
        public static string PlantoPlay => Resource.PlantoPlay;
        
        public static string Played => Resource.Played;
        
        public static string Score => Resource.Score;
        
        public static string ReleaseDate => Resource.ReleaseDate;
        
        public static string MakeReview => Resource.MakeReview;
        
        public static string Synopsis => Resource.Synopsis;
        
        public static string Genres => Resource.Genres;
        
        public static string Platforms => Resource.Platforms;

        public static string Review => Resource.Review;
        
        public static string Save => Resource.Save;
        
        public static string EnterEmail => Resource.EnterEmail;
        
        public static string RecoverPassword => Resource.RecoverPassword;

        public static string ChangePassword => Resource.ChangePassword;
        public static string Ok => Resource.Ok;

        public static string PasswordConfirm => Resource.PasswordConfirm;

        public static string PasswordConfirmError1 => Resource.PasswordConfirmError1;

        public static string PasswordConfirmError2 => Resource.PasswordConfirmError2;

        public static string PasswordConfirmPlaceHolder => Resource.PasswordConfirmPlaceHolder;

        public static string ConfirmNewPassword => Resource.ConfirmNewPassword;

        public static string ConfirmNewPasswordError => Resource.ConfirmNewPasswordError;

        public static string ConfirmNewPasswordError2 => Resource.ConfirmNewPasswordError2;

        public static string ConfirmNewPasswordPlaceHolder => Resource.ConfirmNewPasswordPlaceHolder;

        public static string CurrentPassword => Resource.CurrentPassword;

        public static string CurrentPasswordError => Resource.CurrentPasswordError;

        public static string CurrentPasswordPlaceHolder => Resource.CurrentPasswordPlaceHolder;

        public static string NewPassword => Resource.NewPassword;

        public static string NewPasswordError => Resource.NewPasswordError;

        public static string NewPasswordPlaceHolder => Resource.NewPasswordPlaceHolder;




    }
}

