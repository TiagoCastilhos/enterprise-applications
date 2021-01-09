using System.Collections.Generic;

namespace NerdStoreEnterprise.Identity.API.Models
{
    public sealed class UserLoginResponse
    {
        public string AccessToken { get; }
        public double ExpiresIn { get; }
        public UserToken UserToken { get; }

        public UserLoginResponse(string accessToken, double expiresIn, UserToken userToken)
        {
            AccessToken = accessToken;
            ExpiresIn = expiresIn;
            UserToken = userToken;
        }
    }

    public sealed class UserToken
    {
        public string Id { get; }
        public string Email { get; }
        public IEnumerable<UserClaim> Claims { get; }

        public UserToken(string id, string email, IEnumerable<UserClaim> claims)
        {
            Id = id;
            Email = email;
            Claims = claims;
        }
    }

    public sealed class UserClaim
    {
        public string Type { get; }
        public string Value { get; }

        public UserClaim(string type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}