namespace SimpleButtonsApp
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public partial class MainWindowViewModel : ObservableObject
    {
        private Random random;
        private CancellationTokenSource cancellationTokenSource;
        private ConcurrentDictionary<int, Task> runningTasks = new ConcurrentDictionary<int, Task>();

        [ObservableProperty]
        private string infoLabel;

        [ObservableProperty]
        private string loopLabel;

        public MainWindowViewModel()
        {
            this.random = new Random();
        }

        [RelayCommand]
        async Task StartDrawing()
        {
            this.cancellationTokenSource = new CancellationTokenSource();
            InfoLabel = "Start Drawing button was clicked";
            var token = this.cancellationTokenSource.Token;
            var firstInfiniteLoop = StartDrawingLoop(token);
            var secondInfiniteLoop = StartDrawingLoop(token);
            this.runningTasks.TryAdd(0, firstInfiniteLoop);
            this.runningTasks.TryAdd(1, secondInfiniteLoop);

            while(runningTasks.Count > 0)
            {
                var finishedTask = await Task.WhenAny(this.runningTasks.Values);
                var taskToRemove = this.runningTasks.First(t => t.Value == finishedTask);
                this.runningTasks.TryRemove(taskToRemove);
            }
        }

        [RelayCommand]
        async Task Stop()
        {
            await Task.Run(() => this.cancellationTokenSource?.Cancel());
        }

        public async Task StartDrawingLoop(CancellationToken cancellationToken)
        {
            try
            {
                while (await Task.Run(() => StartDrawingLuckyNumber(cancellationToken)))
                {
                }
                
            }
            catch (OperationCanceledException ex)
            {
                this.InfoLabel = $"Process was cancelled";
            }
        }

        public async Task<bool> StartDrawingLuckyNumber(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                this.LoopLabel = $"Lucky number is: {this.random.Next(1, 99)}";
            }
            cancellationToken.ThrowIfCancellationRequested();
            return false;
        }
    }
}