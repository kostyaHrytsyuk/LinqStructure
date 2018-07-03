using System;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using LinqStructure.Entities;

namespace LinqStructure
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new WebClient();

            var users = client.DownloadString("https://5b128555d50a5c0014ef1204.mockapi.io/address");

            var desUsers = JsonConvert.DeserializeObject<List<Address>>(users);

            Console.ReadKey();
        }
    }
}
