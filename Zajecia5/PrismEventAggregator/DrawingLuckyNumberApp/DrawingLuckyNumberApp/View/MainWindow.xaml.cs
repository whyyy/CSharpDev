namespace DrawingLuckyNumberApp.View
{
    using System.Windows;
    using ViewModel;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}