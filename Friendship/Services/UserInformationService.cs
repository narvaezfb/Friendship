using System;
using Friendship.Interfaces;
using Newtonsoft.Json.Linq;

namespace Friendship.Services
{
	public class UserInformationService : IUserInformationService
	{
		
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7279";

        public UserInformationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BaseUrl);
        }

        public async Task<HttpResponseMessage> RetrieveUserInformation(string userId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"User/{userId}");
                return response;

            }catch(Exception ex)
            {
                Console.WriteLine($"Error retrieving user Information: {ex.Message}");
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }
        } 
	}
}

