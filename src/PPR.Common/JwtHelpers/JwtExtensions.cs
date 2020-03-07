using System;
using Microsoft.Extensions.Configuration;

namespace PPR.Common.JwtHelpers {

    public class UserDTO {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string ZipCode { get; set; }

        public string DateofBirth { get; set; }

        public int? SignInType { get; set; }

        public short? Status { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool PhoneConfirmed { get; set; }

        public bool IsNotificationsOn { get; set; }

        public string Location { get; set; }

        public string Token { get; set; }

    }
    public static class JwtExtensions {
        public static void GenerateToken (this UserDTO user, IConfiguration configuration) {
            try {
                var token = new JwtTokenBuilder ()
                    .AddSecurityKey (JwtSecurityKey.Create (configuration.GetValue<string> ("JwtSecretKey")))
                    .AddIssuer (configuration.GetValue<string> ("JwtIssuer"))
                    .AddAudience (configuration.GetValue<string> ("JwtAudience"))
                    .AddExpiry (30)
                    .AddClaim ("Id", user.Id.ToString ())
                    .AddRole ("User")
                    .Build ();

                user.Token = token.Value;
            } catch (Exception) {
                throw;
            }
        }
    }
}