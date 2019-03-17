/// Mohamed Ali NOUIRA
/// http://www.mohamedalinouira.com
/// https://github.com/medalinouira
/// Copyright © Mohamed Ali NOUIRA. All rights reserved.

using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using OAuth2.ResourceOwnerPasswordCredentials;
using OAuth2.ResourceOwnerPasswordCredentials.Models;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                await Authenticate();
            }).GetAwaiter().GetResult();
        }

        static async Task Authenticate()
        {
            var authInfo = new AuthInfo();
            authInfo.Scope = Constants.Scope;
            authInfo.ClientId = Constants.ClientId;
            authInfo.Username = Constants.Username;
            authInfo.Password = Constants.Password;
            authInfo.ResponseType = Constants.ResponseType;

            ResourceOwnerPasswordCredentials resourceOwnerPasswordCredentials = new ResourceOwnerPasswordCredentials(Constants.Authority, authInfo);
            var result = await resourceOwnerPasswordCredentials.Authenticate();

            Console.WriteLine(JsonConvert.SerializeObject(result));
            Console.ReadLine();
        }
    }
}
