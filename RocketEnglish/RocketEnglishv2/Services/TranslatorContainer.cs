﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18449
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Notice: Use of the service proxies that accompany this notice is subject to
//            the terms and conditions of the license agreement located at
//            http://go.microsoft.com/fwlink/?LinkID=202740
//            If you do not agree to these terms you may not use this content.


using System;
using System.Net;  // needed for authentication
using System.Collections.Generic;
using System.Data.Services.Client;
using System.IO;
using System.Web.Script.Serialization;
using System.Text;
using System.Resources;
using System.Web;
using System.Xml;

namespace RocketEnglishv2.Services
{

    public class TranslatorContainer
    {
        private static string GetBingAuthToken(string clientId = null, string clientSecret = null)
{
            string authBaseUrl = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";

            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
            {
                System.ApplicationException ex =
                    new System.ApplicationException("No Info");

                throw ex;
                return null;
            }
            
            var postData = string.Format("grant_type=client_credentials&client_id={0}" +
                                            "&client_secret={1}" +
                                            "&scope=http://api.microsofttranslator.com",
                                            HttpUtility.UrlEncode(clientId),
                                            HttpUtility.UrlEncode(clientSecret));

            // POST Auth data to the oauth API
            string res, token;

            try
            {
                var web = new WebClient();
                web.Encoding = Encoding.UTF8;
                res = web.UploadString(authBaseUrl, postData);
            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }

            var ser = new JavaScriptSerializer();
            var auth = ser.Deserialize<BingAuth>(res);
            if (auth == null)
                return null;
            
            token = auth.access_token;

            return token;
        }


        private class BingAuth
        {
            public string token_type { get; set; }
            public string access_token { get; set; }
        }








        public static string TranslateBing(string text, string fromCulture, string toCulture)
        {
            string serviceUrl = "http://api.microsofttranslator.com/V2/Http.svc/Translate";

            string accessToken = GetBingAuthToken("rocket_english", "R8ZU5w4iBybB+jxGe0AFWGW6C68y/zzP0vtbggrkwkI=");

            string res;

            try
            {
                var web = new WebClient();
                web.Headers.Add("Authorization", "Bearer " + accessToken);
                string ct = "text/plain";
                string postData = string.Format("?text={0}&from={1}&to={2}&contentType={3}",
                                            HttpUtility.UrlEncode(text),
                                            fromCulture, toCulture,
                                            HttpUtility.UrlEncode(ct));

                web.Encoding = Encoding.UTF8;
                res = web.DownloadString(serviceUrl + postData);
            }
            catch (Exception e)
            {
                throw e;
                return null;
            }

            // result is a single XML Element fragment
            var doc = new XmlDocument();
            doc.LoadXml(res);
            return doc.DocumentElement.InnerText;
        }
    }
}