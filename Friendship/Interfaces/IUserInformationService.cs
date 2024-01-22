using System;
namespace Friendship.Interfaces
{
	public interface IUserInformationService
	{
		public Task<HttpResponseMessage> RetrieveUserInformation(string userId);
	}
}

