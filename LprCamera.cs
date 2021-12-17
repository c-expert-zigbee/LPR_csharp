using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace LPR_CSharp
{
    public class LprCamera
    {
        string name = "";
        string ip = "";
        string port = "";
        string userName = "";
        string password = "";

        int connectionInterval;
        int tries;
        bool connection;

        string longPollingPort = null;
        string basicAuth = "";
        bool isAlive = false;
        string streamingData = "";

        string setRenewInterval = null;
        string setRenewTimeout = null;
        bool keepAlive = true;

        public LprCamera(Dictionary<string, string> lprCamera)
        {
            this.setProperties(lprCamera);
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(lprCamera["userName"] + ":" + lprCamera["password"]);
            this.basicAuth = "Basic " + System.Convert.ToBase64String(plainTextBytes);
            tries = 1;
            connectionInterval = 0;
            connection = false;
        }
        public void setProperties(Dictionary<string, string> lprCamera)
        {
            this.name = lprCamera["name"];
            this.ip = lprCamera["ip"];
            this.port = lprCamera["port"];
            this.userName = lprCamera["userName"];
            this.password = lprCamera["password"];
        }

        public void connect()
        {
            this.keepAlive = true;
            try
            {
                tryToConnectInterval();
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine(this.name + " - " + e.StackTrace);
                this.closeConnection();
            }
        }

        public void tryToConnectInterval()
        {
            if (this.connectionInterval == 0)
                return;
            var task = Task.Run(async () => {
                for (; ; )
                {
                    await Task.Delay(10000);
                    startConnection(tries++);
                }
            });
        }

        public void startConnection(int tries)
        {
            closeConnectionIfExists();
            try
            {
                IPAddress ipAd = IPAddress.Parse(ip);
                TcpListener myList = new TcpListener(ipAd, int.Parse(port));
                myList.Start();
                Console.WriteLine(this.name + " - Connecting(" + tries + ")");
            }
            catch (Exception e) {
                Console.WriteLine(" - Error " + e.StackTrace);
            }
        }

        async void tryStartingConnection()
        {
            /*
            try
            {
                lprCamera.StremingData += data;
                lprCamera.defineSocketListeners();
                await lprCamera.getAndSetLongPollingPort();
                lprCamera.connection.connect(lprCamera.longPollingPort, lprCamera.ip, () => {
                    clearInterval(lprCamera.connectionInterval);
                    lprCamera.connectionInterval = null;
                    subscribeToEvents(lprCamera);
                });
            }
            catch (ex)
            {
                console.error(ex.message);
            }
            */
        }


        public void closeConnection()
        {
            /*
            this.connection.end();
            this.connection.destroy();
            this.connection = null;
            this.builtUnsubscribeConfig = false;
            this.stopSetRenewTimeOut();
            this.stopSetRenewInterval();
            */
        }

        public void closeConnectionIfExists()
        {
            if (this.connection)
                closeConnection();
        }
    }

   
}
