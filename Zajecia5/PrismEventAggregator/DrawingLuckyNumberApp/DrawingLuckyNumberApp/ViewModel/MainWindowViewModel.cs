using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Prism.Commands;
using Prism.Mvvm;

namespace DrawingLuckyNumberApp.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {
        private CancellationTokenSource cancellationTokenSource;
        private string infoLabel;
        private string loopLabel;

        public string InfoLabel
        {
            get => this.infoLabel;
            set => SetProperty(ref this.infoLabel, value);
        }

        public string LoopLabel
        {
            get => this.loopLabel;
            set => SetProperty(ref this.loopLabel, value);
        }

        public DelegateCommand StartDrawingCommand { get; private set; }
        public DelegateCommand StopDrawingCommand { get; private set; }

        private async void StartDrawing(object sender, RoutedEventArgs e)
        {
            this.cancellationTokenSource = new CancellationTokenSource();
            await this.StartDrawingLuckyNumber(this.cancellationTokenSource.Token);

        }

        private void StopDrawing(object sender, RoutedEventArgs e)
        {
            this.cancellationTokenSource?.Cancel();
            this.InfoLabel = "Drawing finished";
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

            this.InfoLabel = "Drawing is in progress";
            this.LoopLabel = $"Lucky number is: {luckyNumber}";

            return false;
        }
    }
}