using Fast_food.Models.MyModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fast_food.MyHelpers
{
    public class MyHttpClient
    {
        private HttpClient httpClient;
        private Uri uri;
        private HttpResponseMessage response;
        private string json = "";
        private StringContent content;

        public MyHttpClient()
        {
            httpClient = new HttpClient();
        }

        // hado ila bghit n returner JObject --------------------------------------------------------------------------
        public async Task<JObject> sendHttpGetAsyncJson(string link)
        {
            response = null;

            uri = new Uri(string.Format(link, string.Empty));
            response = await httpClient.GetAsync(uri);

            return responseChecker(response);
        }


        public async Task<JObject> sendHttpPostAsyncJson(string link, Object o)
        {
            response = null;

            uri = new Uri(string.Format(link, string.Empty));
            json = JsonConvert.SerializeObject(o);
            content = new StringContent(json, Encoding.UTF8, "application/json");
            response = await httpClient.PostAsync(uri, content);

            return responseChecker(response);
        }


        public async Task<JObject> sendHttpPutAsyncJson(string link, Object o)
        {
            response = null;

            uri = new Uri(string.Format(link, string.Empty));
            json = JsonConvert.SerializeObject(o);
            content = new StringContent(json, Encoding.UTF8, "application/json");
            response = await httpClient.PutAsync(uri, content);

            return responseChecker(response);
        }


        public async Task<JObject> sendHttpDeleteAsyncJson(string link)
        {
            response = null;

            uri = new Uri(string.Format(link, string.Empty));
            response = await httpClient.DeleteAsync(uri);

            return responseChecker(response);
        }


        private JObject responseChecker(HttpResponseMessage response)
        {
            JObject jRes;
            string result = "";

            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsStringAsync().Result;

                try
                {
                    jRes = JObject.Parse(result);
                    jRes.Add(new JProperty("HttpClient parsing error", false));
                    jRes.Add(new JProperty("HttpClient error", false));
                }
                catch (Exception e)
                {
                    jRes = new JObject();
                    jRes.Add(new JProperty("HttpClient parsing error", true));
                    jRes.Add(new JProperty("HttpClient error", false));
                }
            }
            else
            {
                jRes = new JObject();
                jRes.Add(new JProperty("HttpClient parsing error", false));
                jRes.Add(new JProperty("HttpClient error", true));
            }

            return jRes;
        }




        // hado ila bghit n returner List<O> --------------------------------------------------------------------------
        public async Task<List<O>> sendHttpGetAsyncList<O>(string link)
        {
            response = null;

            uri = new Uri(string.Format(link, string.Empty));
            response = await httpClient.GetAsync(uri);

            return responseCheckerList<O>(response);
        }


        public async Task<List<O>> sendHttpPostAsyncList<O>(string link, Object o)
        {
            response = null;

            uri = new Uri(string.Format(link, string.Empty));
            json = JsonConvert.SerializeObject(o);
            content = new StringContent(json, Encoding.UTF8, "application/json");
            response = await httpClient.PostAsync(uri, content);

            return responseCheckerList<O>(response);
        }


        public async Task<List<O>> sendHttpPutAsyncList<O>(string link, Object o)
        {
            response = null;

            uri = new Uri(string.Format(link, string.Empty));
            json = JsonConvert.SerializeObject(o);
            content = new StringContent(json, Encoding.UTF8, "application/json");
            response = await httpClient.PutAsync(uri, content);

            return responseCheckerList<O>(response);
        }


        public async Task<List<O>> sendHttpDeleteAsyncList<O>(string link)
        {
            response = null;

            uri = new Uri(string.Format(link, string.Empty));
            response = await httpClient.DeleteAsync(uri);

            return responseCheckerList<O>(response);
        }


        private List<O> responseCheckerList<O>(HttpResponseMessage response)
        {
            List<O> list;
            string result = "";

            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsStringAsync().Result;

                try
                {
                    list = JsonConvert.DeserializeObject<List<O>>(result);
                }
                catch (Exception e)
                {
                    list = new List<O>();
                }
            }
            else
            {
                list = new List<O>();
            }

            return list;
        }




        // hado ila bghit n returner Object --------------------------------------------------------------------------
        public async Task<Object> sendHttpGetAsyncObject<O>(string link)
        {
            response = null;

            uri = new Uri(string.Format(link, string.Empty));
            response = await httpClient.GetAsync(uri);

            return responseCheckerObject<O>(response);
        }


        public async Task<Object> sendHttpPostAsyncObject<O>(string link, Object o)
        {
            response = new HttpResponseMessage();

            uri = new Uri(string.Format(link, string.Empty));
            json = JsonConvert.SerializeObject(o);
            content = new StringContent(json, Encoding.UTF8, "application/json");
            response = await httpClient.PostAsync(uri, content);

            return responseCheckerObject<O>(response);
        }


        public async Task<Object> sendHttpPutAsyncObject<O>(string link, Object o)
        {
            response = new HttpResponseMessage();

            uri = new Uri(string.Format(link, string.Empty));
            json = JsonConvert.SerializeObject(o);
            content = new StringContent(json, Encoding.UTF8, "application/json");
            response = await httpClient.PutAsync(uri, content);

            return responseCheckerObject<O>(response);
        }


        public async Task<Object> sendHttpDeleteAsyncObject<O>(string link)
        {
            response = new HttpResponseMessage();

            uri = new Uri(string.Format(link, string.Empty));
            response = await httpClient.DeleteAsync(uri);

            return responseCheckerObject<O>(response);
        }


        private Object responseCheckerObject<O>(HttpResponseMessage response)
        {
            O o;
            string result = "";

            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsStringAsync().Result;

                try
                {
                    o = JsonConvert.DeserializeObject<O>(result);
                    return o;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }




    }
}
