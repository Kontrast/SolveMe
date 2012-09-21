using System;
using System.Collections.Generic;
using System.Web;
using DotNetOpenAuth.AspNet.Clients;
using System.Net;
using System.Collections.Specialized;
using System.Web.Helpers;

namespace SolveMe.Models
{
    /// <summary>
    /// Provide a client to autentificate throw the Github
    /// </summary>
    public class GitHubClient : OAuth2Client
    {
        private const string AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
        private const string TokenEndpoint = "https://github.com/login/oauth/access_token";
        private readonly string _clientId;
        private readonly string _clientSecret;
        private Guid redirectState;
        
        public GitHubClient(string clientId, string clientSecret) : base("github")
        { 
            this._clientId = clientId;
            this._clientSecret = clientSecret;
            redirectState = Guid.NewGuid();
        }

        protected override Uri GetServiceLoginUrl(Uri returnUrl)
        {
            return new Uri(
                AuthorizationEndpoint
                + "?client_id=" + this._clientId
                + "&redirect_uri=" + HttpUtility.UrlEncode(returnUrl.ToString())
                + "&Scope=user"
                + "&state=" + redirectState.ToString()
            );
        }

        protected override string QueryAccessToken(Uri returnUrl, string authorizationCode)
        {
            WebClient client = new WebClient();
            string content = client.DownloadString(
                TokenEndpoint
                + "?client_id=" + this._clientId
                + "&redirect_uri=" + HttpUtility.UrlEncode(returnUrl.ToString())
                + "&client_secret=" + this._clientSecret             
                + "&code=" + authorizationCode
                + "&state=" + redirectState.ToString()
            );

            NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(content);
            if (nameValueCollection != null)
            {
                string result = nameValueCollection["access_token"];
                return result;
            }
            return null;
        }

        protected override IDictionary<string,string> GetUserData(string accessToken)
        {
            WebClient client = new WebClient();
            string content = client.DownloadString(
                "https://api.github.com/user?access_token=" + accessToken
            );
            dynamic data = Json.Decode(content);
            var result = new Dictionary<string, string>();
                result["id"] = data.id.ToString();
                result["name"] = data.login;
            return result;
        }
    }
}