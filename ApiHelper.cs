using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;

namespace Intranet2.Helpers
{
    public class ApiHelper<T>
    {
        private HttpClient client = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
        private WindowsIdentity identity { get; set; }

        public ApiHelper(IIdentity identity) 
        {
            this.identity = (WindowsIdentity)identity;
        }

        static JsonSerializerSettings _settings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                    NullValueHandling = NullValueHandling.Ignore
                };
            }
        }

        public string BaseUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["BaseApiUri"] + "api/";
            }
        }

        public async Task<T> GetSingleItem(string apiPath)
        {
            return await WindowsIdentity.RunImpersonated(identity.AccessToken, async () =>
            {
                var response = await client.GetAsync(BaseUrl + apiPath).ConfigureAwait(false);
                return await ReadValueFromResponse(response);
            });
        }

        public async Task<List<T>> GetMultipleItemsAsList(string apiPath)
        {
            return (await GetMultipleItems(apiPath)).ToList();
        }

        public async Task<IEnumerable<T>> GetMultipleItems(string apiPath)
        {
            return await WindowsIdentity.RunImpersonated(identity.AccessToken, async () =>
            {
                var response = await client.GetAsync($"{BaseUrl}{apiPath}").ConfigureAwait(false);
                return await ReadMultipleValuesFromResponse(response);
            });
        }

        public async Task<HttpResponseMessage> PostAsync(string apiPath, Dictionary<string, string> values = null)
        {
            // optional post without values, a perfectly valid option
            if (values == null)
            {
                values = new Dictionary<string, string> { };
            }
            
            var content = new FormUrlEncodedContent(values);

            string path = string.Format("{0}{1}{2}",
                BaseUrl, 
                apiPath, 
                "?" + string.Join("&", values.Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value))));

            var response = await client.PostAsync(path, content);
            return response;
        }

        //public async Task<T> PostAsyncWithResponse(string apiPath, object modelParam)
        //{
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    return await WindowsIdentity.RunImpersonated(identity.AccessToken, async () =>
        //    {
        //        var response = await client.PostAsync(BaseUrl + apiPath, modelParam);
        //        return await ReadValueFromResponse(response);
        //    });
        //}

        //public async Task<IEnumerable<T>> PostAsyncWithMultipleResponse(string apiPath, object modelParam)
        //{
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    return await WindowsIdentity.RunImpersonated(identity.AccessToken, async () =>
        //    {
        //        var response = await client.PostAsync(BaseUrl + apiPath, modelParam);
        //        return await ReadMultipleValuesFromResponse(response);
        //    });
        //}

        //public async Task<List<T>> PostAsyncWithMultipleResponseAsList(string apiPath, object modelParam)
        //{
        //    return (await PostAsyncWithMultipleResponse(apiPath, modelParam)).ToList();
        //}

        private async Task<T> ReadValueFromResponse(HttpResponseMessage response)
        {
            T result = default(T);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = JsonConvert.DeserializeObject<T>(x.Result, _settings);
                });
            }
            return result;
        }

        private async Task<T[]> ReadMultipleValuesFromResponse(HttpResponseMessage response)
        {
            T[] result = default(T[]);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = JsonConvert.DeserializeObject<T[]>(x.Result, _settings);
                });
            }
            return result;
        }


    }
}