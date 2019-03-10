using DummyLogisticsCompanySystem;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPO
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string DoReMiRootUrl = ConfigurationManager.AppSettings["DoReMiRootUrl"];
            var baseAddress = DoReMiRootUrl;

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress + "/api/Product/AutoPO"));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";

            Product product = new Product();

            string parsedContent = JsonConvert.SerializeObject(product);

            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(parsedContent);

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            var response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);

            var content = sr.ReadToEnd();
            if (content == "true")
            {
                System.Console.WriteLine("Auto PO is done.");
            }
            Console.ReadKey();
        }
    }
}