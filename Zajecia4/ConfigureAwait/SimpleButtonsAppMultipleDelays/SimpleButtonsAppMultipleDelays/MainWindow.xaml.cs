namespace SimpleButtonsApp
{
    using System.Threading.Tasks;
    using System.Threading;
    using System.Windows;
    using System;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CancellationTokenSource cancellationTokenSource;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartDrawing(object sender, RoutedEventArgs e)
        {
            this.cancellationTokenSource = new CancellationTokenSource();
            StartDrawingLuckyNumber(this.cancellationTokenSource.Token).ConfigureAwait(false);
        }

        private void StopDrawing(object sender, RoutedEventArgs e)
        {
            this.cancellationTokenSource.Cancel();
            this.InfoLabel.Content = "Drawing finished";
        }

        private async Task<bool> StartDrawingLuckyNumber(CancellationToken cancellationToken)
        {
            var random = new Random();
            await Task.Run(() =>
            {
                try
                {
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        //this.Dispatcher.Invoke(() =>
                        //{
                            this.InfoLabel.Content = "Drawing is in progress";
                            this.LoopLabel.Content = $"Lucky number is: {random.Next(1, 99)}";
                        //});
                    }
                    cancellationToken.ThrowIfCancellationRequested();
                }

                catch (OperationCanceledException ex)
                {

                }
                
            }, cancellationToken).ConfigureAwait(false);
            
            return false;
        }
    }
}
