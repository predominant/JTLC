using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace JTLC
{
    public class Controller
    {
        public string JenkinsUri { get; set; }

        public SerialPort Port { get; set; }

        public 

        public Controller(string _jenkinsUri, SerialPort _Port)
        {
            this.JenkinsUri = _jenkinsUri;
            this.ComPort = _comPort;
        }

        public void WritePass()
        {

        }
    }
}
