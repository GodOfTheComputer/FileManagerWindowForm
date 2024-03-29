﻿using DevExpress.XtraEditors;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FileManagerWindowForm
{
    public static class HttpGenerator
    {
        public static readonly string httpStart = "https://localhost:44313/";

        public static async Task GenerateVoidHttp(string funcName, UrlParameterContainer urlParamContainer)
        {
            string url = GenerateUrl(funcName, urlParamContainer);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AuthorizationManager.token);

                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    HttpResponseContainer httpResponseContainer = JsonConvert.DeserializeObject<HttpResponseContainer>(response.Content.ReadAsStringAsync().Result);
                    throw new Exception(httpResponseContainer.detail + "/" + "Server error : " + httpResponseContainer.status);
                }
            }
        }

        public static async Task<string> GenerateJsonHttp(string funcName, UrlParameterContainer urlParamContainer)
        {
            string url = GenerateUrl(funcName, urlParamContainer);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AuthorizationManager.token);

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    HttpResponseContainer httpResponseContainer = JsonConvert.DeserializeObject<HttpResponseContainer>(response.Content.ReadAsStringAsync().Result);
                    throw new Exception(httpResponseContainer.detail + "/" + "Server error : " + httpResponseContainer.status);
                }
            }
        }

        private static string GenerateUrl(string funcName, UrlParameterContainer urlParamContainer)
        {
            string url = httpStart + funcName + "?";

            List<string> parameters = new List<string>();

            if (urlParamContainer.parameters != null) parameters = urlParamContainer.parameters;

            for (int i = 0; i < parameters.Count; i++)
            {
                url += parameters[i].Split(' ')[0] + "=" + FilterBasedUrlStandart(parameters[i].Split(' ')[1]) + "&";
            }

            url = url.Substring(0, url.Length - 1);

            return url;
        }

        private static string FilterBasedUrlStandart(string param)
        {
            string newStr = "";

            for(int i = 0; i < param.Length; i++)
            {
                string hex = Convert.ToInt32(param[i]).ToString("X");

                if (((int)param[i] < 65 || (int)param[i] > 90) &&
                    ((int)param[i] < 97 || (int)param[i] > 122) &&
                    ((int)param[i] < 48 || (int)param[i] > 57) &&
                    (int)param[i] != 45 &&
                    (int)param[i] != 56 &&
                    (int)param[i] != 95 &&
                    (int)param[i] != 126) newStr += "%" + hex;
                else newStr += param[i];
            }

            return newStr;
        }
    }
}
