namespace DrawingLuckyNumberModule.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using DrawingLuckyNumber.Core;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;

    public class DrawLuckyNumberViewModel : BindableBase
    {
        private CancellationTokenSource cancellationTokenSource;
        private readonly IEventAggregator eventAggregator;

        public DrawLuckyNumberViewModel(IEventAggregator eventAggregator)
        {
            this.StartDrawingCommand = new DelegateCommand(this.StartDrawing);
            this.StopDrawingCommand = new DelegateCommand(this.StopDrawing);
            this.eventAggregator = eventAggregator;
        }

        public DelegateCommand StartDrawingCommand { get; private set; }
        public DelegateCommand StopDrawingCommand { get; private set; }

        public async void StartDrawing()
        {
            this.eventAggregator.GetEvent<IsDrawingInProgressEvent>().Publish(true);
            this.eventAggregator.GetEvent<LuckyNumberDrawnEvent>().Publish(0);
            this.cancellationTokenSource = new CancellationTokenSource();
            await this.StartDrawingLuckyNumber(this.cancellationTokenSource.Token);
        }

        public void StopDrawing()
        {
            this.eventAggregator.GetEvent<IsDrawingInProgressEvent>().Publish(false);
            this.cancellationTokenSource?.Cancel();
        }

        public async Task<int> StartDrawingLuckyNumber(CancellationToken cancellationToken)
        {
            var random = new Random();
            var luckyNumber = 0;
            await Task.Run(() =>
                           {
                               try
                               {
                                   while (!cancellationToken.IsCancellationRequested)
                                   {
                                       luckyNumber = random.Next(1, 99);
                                       this.eventAggregator.GetEvent<LuckyNumberDrawnEvent>().Publish(luckyNumber);
                                       Task.Delay(100, cancellationToken);
                                   }

                                   cancellationToken.ThrowIfCancellationRequested();
                               }
                               catch (OperationCanceledException ex)
                               {
                                   return luckyNumber;
                               }
                               catch (Exception ex)
                               {
                               }

                               return luckyNumber;
                           }, cancellationToken);

            return luckyNumber;
        }
    }
}