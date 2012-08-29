using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Helpers;
using Microsoft.Web.WebPages.OAuth;
using SolveMe.Models;

namespace SolveMe
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            OAuthWebSecurity.RegisterTwitterClient(
                consumerKey: ConfigurationManager.AppSettings.Get("twitterClient:consumerKey"),
                consumerSecret: ConfigurationManager.AppSettings.Get("twitterClient:consumerSecret"));

            OAuthWebSecurity.RegisterFacebookClient(
                appId: ConfigurationManager.AppSettings.Get("facebookClient:appId"),
                appSecret: ConfigurationManager.AppSettings.Get("facebookClient:appSecret"));

            OAuthWebSecurity.RegisterClient(new GitHubClient(
               clientId: ConfigurationManager.AppSettings.Get("githubClient:clientId"),
               clientSecret: ConfigurationManager.AppSettings.Get("githubClient:clientSecret")), "GitHub", null);

            //OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
