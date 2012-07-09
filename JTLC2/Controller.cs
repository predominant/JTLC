namespace JTLC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO.Ports;
    using System.Linq;
    using System.Net;
    using System.Runtime.Serialization.Json;
    using System.ServiceModel;
    using System.Text;
    using System.Timers;
    using JTLC.DataContracts;

    public class Controller
    {
        public string JenkinsUri { get; set; }

        public string PortName { get; set; }

        public SerialPort Port { get; set; }

        private Timer timer;

        public Controller()
        {
            this.timer = new Timer();
            this.timer.Interval = 15000;
            this.timer.Elapsed += this.TimerPoll;
        }

        public Controller(string _jenkinsUri, string _portName)
            : this()
        {
            this.JenkinsUri = _jenkinsUri;
            this.PortName = _portName;
        }

        ~Controller()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            if (this.Port != null && this.Port.IsOpen)
            {
                this.Port.Close();
                this.Port.Dispose();
            }

            this.timer.Stop();
            this.timer.Dispose();
        }

        public void Start()
        {
            this.timer.Start();
        }

        public void Stop()
        {
            this.timer.Stop();
        }

        private void TimerPoll(object sender, EventArgs e)
        {
            JenkinsResponse data = this.GetJenkinsData(this.JenkinsUri);
            JenkinsStatus status = JenkinsStatus.Unknown;
            if (data != null)
            {
                status = this.GetJenkinsStatus(data);
            }

            this.Write(this.GetIsBuilding(data) ? TrafficCode.Building : TrafficCode.BuildComplete);

            switch (status)
            {
                case JenkinsStatus.Pass:
                    this.Write(TrafficCode.Pass);
                    break;
                case JenkinsStatus.Fail:
                    this.Write(TrafficCode.Fail);
                    break;
            }
        }

        private JenkinsResponse GetJenkinsData(string uri)
        {
            JenkinsResponse r = null;
            HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return null;
                }

                DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(JenkinsResponse));
                r = s.ReadObject(response.GetResponseStream()) as JenkinsResponse;
            }

            return r;
        }

        private JenkinsStatus GetJenkinsStatus(JenkinsResponse r)
        {
            JenkinsStatus result = JenkinsStatus.Unknown;

            foreach (Job j in r.Jobs)
            {
                if (j.Color.StartsWith("red"))
                {
                    result = JenkinsStatus.Fail;
                    break;
                }

                if (j.Color.StartsWith("blue"))
                {
                    result = JenkinsStatus.Pass;
                }
            }

            return result;
        }

        private bool GetIsBuilding(JenkinsResponse r)
        {
            foreach (Job j in r.Jobs)
            {
                if (j.Color.EndsWith("anime"))
                {
                    return true;
                }
            }

            return false;
        }

        public static string[] ListComPorts()
        {
            string[] ports = null;
            try
            {
                ports = SerialPort.GetPortNames();
            }
            catch (Win32Exception e)
            {
                Console.WriteLine(e);
                // TODO: Report Error
            }

            return ports;
        }

        public void Write(string _str)
        {
            Console.WriteLine("Write(\"" + _str + "\")");
            this.ConnectPort();
            try
            {
                this.Port.Write(_str);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // TODO: Report Error
            }
        }

        private void ConnectPort()
        {
            try
            {
                if (this.Port == null)
                {
                    Console.WriteLine("  => Port is null, creating new port");
                    this.Port = new SerialPort(this.PortName);
                }
                if (!this.Port.IsOpen)
                {
                    Console.WriteLine("  => Port is closed, opening");
                    this.Port.Open();
                    this.Port.BaudRate = 9600;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // TODO: Report Error
            }
        }
    }
}
