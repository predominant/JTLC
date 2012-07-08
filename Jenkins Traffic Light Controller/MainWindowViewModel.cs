using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTLC.UI
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private IList<string> _comPorts;
        public IList<string> ComPorts
        {
            get
            {
                return this._comPorts;
            }
            private set
            {
                this._comPorts = value;
                this.OnPropertyChanged("ComPorts");
            }
        }

        private string _selectedComPort;
        public string SelectedComPort
        {
            get
            {
                return this._selectedComPort;
            }
            set
            {
                this._selectedComPort = value;
                this.OnPropertyChanged("SelectedComPort");
            }
        }

        private string _jenkinsUri;
        public string JenkinsUri
        {
            get
            {
                return this._jenkinsUri;
            }
            set
            {
                this._jenkinsUri = value;
                this.OnPropertyChanged("JenkinsUri");
            }
        }

        private string _buttonText = "Start";
        public string ButtonText
        {
            get
            {
                return this._buttonText;
            }
            set
            {
                this._buttonText = value;
                this.OnPropertyChanged("ButtonText");
            }
        }

        private bool _running;
        public bool Running
        {
            get
            {
                return this._running;
            }
            set
            {
                this._running = value;
                this.ButtonText = this._running ? "Stop" : "Start";
                this.OnPropertyChanged("Running");
            }
        }

        private Controller Controller;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            this.LoadComPorts();
            this.Controller = new Controller();
            this.Running = false;
        }

        private void LoadComPorts()
        {
            var ports = Controller.ListComPorts();
            if (ports == null)
            {
                return;
            }

            this.ComPorts = ports;
        }

        public void ToggleRunning()
        {
            this.Running = !this.Running;

            if (this.Running)
            {
                this.Controller.JenkinsUri = this.JenkinsUri;
                this.Controller.PortName = this.SelectedComPort;
                this.Controller.Start();
            }
            else
            {
                this.Controller.Stop();
            }
        }

        private void OnPropertyChanged(string _propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(_propertyName));
            }
        }
    }
}
