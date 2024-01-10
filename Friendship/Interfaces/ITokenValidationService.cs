using System;
namespace Friendship.Interfaces
{
	public interface ITokenValidationService
	{
		Task<bool> ValidateTokenAsync(string token);
	}
}

