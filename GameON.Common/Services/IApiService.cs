using GameON.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameON.Common.Services
{
    public interface IApiService
    {
        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller);

        Task<Response> MakeReviewAsync<ReviewRequest>(string urlBase, string servicePrefix, string controller, ReviewRequest model, string tokenType, string accessToken);

        Task<Response> GetVideoGame(string urlBase, string servicePrefix, string controller);

        Task<Response> GetUserProfile(string urlBase, string servicePrefix, string controller);

        Task<Response> GetReview(string urlBase, string servicePrefix, string controller);

        Task<Response> AddEditGameList<GameListRequest>(string urlBase, string servicePrefix, string controller, GameListRequest model, string tokenType, string accessToken);

        Task<Response> GetGameListForUser<GameListForUserRequest>(string urlBase, string servicePrefix, string controller, GameListForUserRequest model);

        Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request);

        Task<Response> GetUserByEmail(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, EmailRequest request);


        Task<Response> ChangePasswordAsync(string urlBase, string servicePrefix, string controller, ChangePasswordRequest changePasswordRequest, string tokenType, string accessToken);

        Task<Response> RecoverPasswordAsync(string urlBase, string servicePrefix, string controller, EmailRequest emailRequest);

    }
}
