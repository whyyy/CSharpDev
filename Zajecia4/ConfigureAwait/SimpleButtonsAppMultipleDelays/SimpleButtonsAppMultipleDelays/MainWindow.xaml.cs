namespace SimpleButtonsApp
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;

    public partial class MainWindow : Window
    {
        private CancellationTokenSource cancellationTokenSource;
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private async void StartDrawing(object sender, RoutedEventArgs e)
        {
            this.cancellationTokenSource = new CancellationTokenSource();
            await this.StartDrawingLuckyNumber(this.cancellationTokenSource.Token);

        }

        private void StopDrawing(object sender, RoutedEventArgs e)
        {
            this.cancellationTokenSource?.Cancel();
            this.InfoLabel.Content = "Drawing finished";
        }

        private async Task<bool> StartDrawingLuckyNumber(CancellationToken cancellationToken)
        {
            var random = new Random();
            int luckyNumber = 0;

            await Task.Run(() =>
            {
                try
                {
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        //this.Dispatcher.Invoke(() =>
                        //{
                        luckyNumber = random.Next(1, 99);
                        Task.Delay(100, cancellationToken);
                        //});
                    }

                    cancellationToken.ThrowIfCancellationRequested();
                }
                catch (OperationCanceledException ex)
                {

                }
                catch (Exception ex)
                {

                }
                
            }, cancellationToken);

            this.InfoLabel.Content = "Drawing is in progress";
            this.LoopLabel.Content = $"Lucky number is: {luckyNumber}";

            return false;
        }
    }
}
