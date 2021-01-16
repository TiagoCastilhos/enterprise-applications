using System.Collections.Generic;

namespace NerdStoreEnterprise.WebApp.MVC.Models
{
    public sealed class UserLoginResponse
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserToken UserToken { get; set; }
        public ResponseResult ResponseResult { get; set; }

        public UserLoginResponse()
        {

        }

        public UserLoginResponse(string accessToken, double expiresIn, UserToken userToken)
        {
            AccessToken = accessToken;
            ExpiresIn = expiresIn;
            UserToken = userToken;
        }

        public UserLoginResponse(ResponseResult responseResult)
        {
            ResponseResult = responseResult;
        }
    }

    public sealed class UserToken
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaim> Claims { get; set; }

        public UserToken()
        {

        }

        public UserToken(string id, string email, IEnumerable<UserClaim> claims)
        {
            Id = id;
            Email = email;
            Claims = claims;
        }
    }

    public sealed class UserClaim
    {
        public string Type { get; set; }
        public string Value { get; set; }

        public UserClaim()
        {

        }

        public UserClaim(string type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}