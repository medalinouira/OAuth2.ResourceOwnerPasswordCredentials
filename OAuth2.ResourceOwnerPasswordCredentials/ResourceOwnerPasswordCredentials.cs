/// Mohamed Ali NOUIRA
/// http://www.mohamedalinouira.com
/// https://github.com/medalinouira
/// Copyright © Mohamed Ali NOUIRA. All rights reserved.

using System;
using System.Net;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using OAuth2.ResourceOwnerPasswordCredentials.Models;

namespace OAuth2.ResourceOwnerPasswordCredentials
{
    public class ResourceOwnerPasswordCredentials
    {
        #region Fields
        private readonly string _authority;
        private readonly AuthInfo _authInfo;
        private readonly HttpClient _httpClient;
        #endregion

        #region Constructor
        public ResourceOwnerPasswordCredentials(string authority, AuthInfo authInfo)
        {
            this._authInfo = authInfo;
            this._authority = authority;
            this._httpClient = new HttpClient();
        }
        #endregion

        #region Methods
        public async Task<AuthResponse> Authenticate()
        {
            if (_authInfo == null)
            {
                throw new NullReferenceException("NullReferenceException: authInfo must be not null !");
            }
            if (string.IsNullOrEmpty(_authority))
            {
                throw new NullReferenceException("NullReferenceException: Authority must be not null !");
            }
            if (string.IsNullOrEmpty(_authInfo.ClientId))
            {
                throw new NullReferenceException("NullReferenceException: ClientId must be not null !");
            }
            if (string.IsNullOrEmpty(_authInfo.Username))
            {
                throw new NullReferenceException("NullReferenceException: Username must be not null !");
            }
            if (string.IsNullOrEmpty(_authInfo.Password))
            {
                throw new NullReferenceException("NullReferenceException: Password must be not null !");
            }

            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _authority);

                var requestParam = "grant_type=password&client_id={0}&response_type={1}&scope={2}&username={3}&password={4}";

                var payload = string.Format(requestParam,
                            WebUtility.UrlEncode(_authInfo.ClientId),
                            WebUtility.UrlEncode(_authInfo.ResponseType),
                            WebUtility.UrlEncode(_authInfo.Scope),
                            WebUtility.UrlEncode(_authInfo.Username),
                            WebUtility.UrlEncode(_authInfo.Password));
                
                request.Content = new StringContent(payload, Encoding.UTF8, "application/x-www-form-urlencoded");                

                using (var response = await _httpClient.SendAsync(request))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Status:  {0}", response.StatusCode);
                        Console.WriteLine("Content: {0}", await response.Content.ReadAsStringAsync());
                    }
                    response.EnsureSuccessStatusCode();

                    var responseAsString = await response.Content.ReadAsStringAsync();
                    var responseAsObject = JsonConvert.DeserializeObject<AuthResponse>(responseAsString);

                    return responseAsObject;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
