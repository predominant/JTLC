namespace JTLC.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using MahApps.Metro.Controls;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private MainWindowViewModel VM;

        private System.Windows.Forms.NotifyIcon TrayIcon;

        public MainWindow()
        {
            InitializeComponent();
            this.SetupTrayIcon();
            VM = new MainWindowViewModel();
            DataContext = VM;
        }

        private void StartStopButton(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Clicked");
            Button b = ((Button)sender);
            VM.ToggleRunning();
        }

        public void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                this.TrayIcon.Visible = true;
            }
            else if (this.WindowState == WindowState.Normal)
            {
                this.ShowInTaskbar = true;
                this.TrayIcon.Visible = false;
            }
        }

        private void SetupTrayIcon()
        {
            this.TrayIcon = new System.Windows.Forms.NotifyIcon();
            this.TrayIcon.Icon = System.Drawing.Icon.FromHandle(Properties.Resources.TrafficIcon.GetHicon());
            this.TrayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TrayIconDoubleClick);
        }

        private void TrayIconDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.WindowState = WindowState.Normal;
        }
    }
}
