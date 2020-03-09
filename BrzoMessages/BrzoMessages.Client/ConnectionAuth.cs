﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BrzoMessages.Client
{
    internal class ConnectionAuth : IDisposable
    {
        private readonly string keyAccess;
        private readonly string privateKey;
        private HttpClient client;

        internal ConnectionAuth(string keyAccess, string privateKey)
        {
            this.keyAccess = keyAccess;
            this.privateKey = privateKey;

            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {privateKey}");
        }
        internal string Authenticate()
        {
            try
            {
                var result = client.PostAsync($"{Config.AUTH_URL}?token={keyAccess}", null).Result;
                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception("Não foi possivel conectar");
                }
                return result.Content.ReadAsStringAsync().Result;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
