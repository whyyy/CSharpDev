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
        private CancellationTokenSource cancellationTokenSource;
        private ConcurrentDictionary<int, Task> runningTasks = new ConcurrentDictionary<int, Task>();
        private ConcurrentDictionary<int,string> informationToPrint = new ConcurrentDictionary<int, string>();

        [ObservableProperty]
        private string _infoLabel;

        public MainWindowViewModel()
        {
        }

        [RelayCommand]
        async Task StartMultiple()
        {
            this.cancellationTokenSource = new CancellationTokenSource();
            InfoLabel = "Start multiple button was clicked";
            var token = this.cancellationTokenSource.Token;
            var firstInfiniteLoop = StartInfiniteLoop(token);
            var secondInfiniteLoop = StartInfiniteLoop(token);
            runningTasks.TryAdd(0, firstInfiniteLoop);
            runningTasks.TryAdd(1, secondInfiniteLoop);

            while(runningTasks.Count > 0)
            {
                var finishedTask = await Task.WhenAny(runningTasks.Values);
                var taskToRemove = runningTasks.First(t => t.Value == finishedTask);
                runningTasks.TryRemove(taskToRemove);
            }
        }

        [RelayCommand]
        async Task Stop()
        {
            await Task.Run(() => this.cancellationTokenSource?.Cancel());
        }

        public async Task StartInfiniteLoop(CancellationToken cancellationToken)
        {
            try
            {
                while (await IsInfiniteLoopRunningAsync(cancellationToken))
                {
                }
                
            }
            catch (OperationCanceledException ex)
            {
                InfoLabel = $"Process was cancelled";
            }
        }

        public async Task<bool> IsInfiniteLoopRunningAsync(CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        var information = $"Task: {Task.CurrentId}, key: {i}, collection count: {this.informationToPrint.Count}";
                        var hasAdded = this.informationToPrint.TryAdd(i, information);
                        if (hasAdded)
                        {
                            OutputPrinter.Print($"Value: {this.informationToPrint[i]}");
                        }
                    }
                }
                cancellationToken.ThrowIfCancellationRequested();
                return false;
            }, cancellationToken);
        }
    }
}