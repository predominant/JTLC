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

        public MainWindow()
        {
            InitializeComponent();
            VM = new MainWindowViewModel();
            DataContext = VM;

        }

        private void StartStopButton(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Clicked");
            Button b = ((Button)sender);
            VM.ToggleRunning();
        }
    }
}
