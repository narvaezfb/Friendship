﻿using System;
using Friendship.Interfaces;

namespace Friendship.Middlewares
{
	public class ValidateTokenMiddleware
	{
        private readonly RequestDelegate _next;
        private readonly ITokenValidationService _tokenValidationService;

        public ValidateTokenMiddleware(RequestDelegate next, ITokenValidationService tokenValidationService)
		{
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _tokenValidationService = tokenValidationService ??
                                      throw new ArgumentNullException(nameof(tokenValidationService));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Extract the token from the Authorization header
                string? authorizationHeader = context.Request.Headers["Authorization"]; 
                string? token = authorizationHeader?.Replace("Bearer ", "");
                
                if (string.IsNullOrEmpty(token))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Token is missing or invalid");
                    return;
                }

                // Use the TokenValidationService to validate the token
                bool isTokenValid = await _tokenValidationService.ValidateTokenAsync(token);

                if (isTokenValid)
                {
                    context.Items["ValidToken"] = token;
                    await _next(context);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Token is invalid");
                    return;
                }
            }
            catch (Exception e)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"Exception occurred: {e}");

                // Respond with a generic error message
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("Internal server error");
            }
        }
     }
}

