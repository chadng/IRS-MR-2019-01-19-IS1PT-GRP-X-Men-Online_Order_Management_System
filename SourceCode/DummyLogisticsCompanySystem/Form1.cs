using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// This is the code for your desktop app. Press Ctrl+F5 (or go to Debug > Start Without Debugging) to
// run your app.

namespace DummyLogisticsCompanySystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Click on the link below to continue learning how to build a desktop app using WinForms!
            System.Diagnostics.Process.Start("http://aka.ms/dotnet-get-started-desktop");
        }

        private void btnShipped_Click(object sender, EventArgs e)
        {
            string DoReMiRootUrl = ConfigurationManager.AppSettings["DoReMiRootUrl"];
            var baseAddress = DoReMiRootUrl;

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress + "/api/Shipment/Shipped"));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";

            Shipment shipment = new Shipment();
            shipment.TrackingNumber = this.tbTrackingNumber.Text;
            shipment.IsFullShipment = true;
            string parsedContent = JsonConvert.SerializeObject(shipment);

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
                MessageBox.Show("Updated Successfully");
            }
        }
    }
}