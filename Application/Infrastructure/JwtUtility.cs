using System.Linq;

//namespace Application.Infrastructure

namespace Infrastructure
{
	public static class JwtUtility
	{
		static JwtUtility()
		{
		}

		public static string GenerateJwtToken
			(Models.User user, ApplicationSettings.Main mainSettings)
		{
			// Generate token that is valid for 8 hours

			byte[] key =
				System.Text.Encoding.ASCII.GetBytes(mainSettings.SecretKey);

			var symmetricSecurityKey =
				new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key: key);

			var securityAlgorithm =
				Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature;

			var signingCredentials =
				new Microsoft.IdentityModel.Tokens
				.SigningCredentials(key: symmetricSecurityKey, algorithm: securityAlgorithm);

			var tokenDescriptor =
				new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
				{
					Subject =
						new System.Security.Claims.ClaimsIdentity
						(new[]
						{
							//new System.Security.Claims.Claim
							//	(type: "RoleId", value: user.RoleId),

							//new System.Security.Claims.Claim
							//	(type: "LastName", value: user.LastName),

							new System.Security.Claims.Claim
								(type: nameof(user.LastName), value: user.LastName),

							new System.Security.Claims.Claim
								(type: System.Security.Claims.ClaimTypes.Name, value: user.Username),

							new System.Security.Claims.Claim
								(type: System.Security.Claims.ClaimTypes.Email, value: user.EmailAddress),

							new System.Security.Claims.Claim
								(type: System.Security.Claims.ClaimTypes.NameIdentifier, value: user.Id.ToString()),
						}),

					//Issuer = "",
					//Audience = "",

					//Expires =
					//	System.DateTime.UtcNow.AddHours(8),

					Expires =
						System.DateTime.UtcNow.AddMinutes(mainSettings.TokenExpiresInMinutes),

					SigningCredentials = signingCredentials,
				};

			var tokenHandler =
				new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

			Microsoft.IdentityModel.Tokens.SecurityToken
				securityToken = tokenHandler.CreateToken(tokenDescriptor: tokenDescriptor);

			string token =
				tokenHandler.WriteToken(token: securityToken);

			return token;
		}

		public static void AttachUserToContext
			(Microsoft.AspNetCore.Http.HttpContext context,
			Application.Services.IUserService userService, string token, string secretKey)
		{
			try
			{
				var key =
					System.Text.Encoding.ASCII.GetBytes(secretKey);

				var tokenHandler =
					new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

				tokenHandler.ValidateToken(token: token,
					validationParameters: new Microsoft.IdentityModel.Tokens.TokenValidationParameters
					{
						ValidateIssuer = false,
						ValidateAudience = false,
						ValidateIssuerSigningKey = true,

						IssuerSigningKey =
							new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key: key),

						// Set clockskew to zero so tokens expire
						// exactly at token expiration time (instead of 5 minutes later)
						ClockSkew =
							System.TimeSpan.Zero,
					}, out Microsoft.IdentityModel.Tokens.SecurityToken validatedToken);

				var jwtToken =
					validatedToken as System.IdentityModel.Tokens.Jwt.JwtSecurityToken;

				if (jwtToken == null)
				{
					return;
				}

				System.Security.Claims.Claim userIdClaim =
					jwtToken.Claims
					.Where(current => current.Type.ToLower() == "NameId".ToLower())
					.FirstOrDefault();

				// دقت کنید که دستور ذیل کار نمی‌کند
				//.Where(current => current.Type == System.Security.Claims.ClaimTypes.NameIdentifier)

				if (userIdClaim == null)
				{
					return;
				}

				var userId =
					int.Parse(userIdClaim.Value);

				Models.User foundedUser =
					userService.GetById(userId);

				if (foundedUser == null)
				{
					return;
				}

				// Attach user to context on successful jwt validation
				context.Items["User"] = foundedUser;
			}
			catch // (System.Exception ex)
			{
				// Log ex
				//string errorMessage = ex.Message;

				// Do nothing if jwt validation fails
				// user is not attached to context so request won't have access to secure routes
			}
		}
	}
}
