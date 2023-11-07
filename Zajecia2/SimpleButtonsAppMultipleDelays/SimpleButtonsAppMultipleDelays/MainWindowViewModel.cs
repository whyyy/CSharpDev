namespace SimpleButtonsApp
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public partial class MainWindowViewModel : ObservableObject
    {
        private CancellationTokenSource cancellationTokenSource;
        private ConcurrentDictionary<int, Task> delayTasks;

        [ObservableProperty]
        private string _infoLabel;

        public MainWindowViewModel()
        {
        }

        [RelayCommand]
        async Task StartMultiple()
        {
            this.cancellationTokenSource = new CancellationTokenSource();
            this.delayTasks = new ConcurrentDictionary<int, Task>();
            InfoLabel = "Start multiple button was clicked";
            var token = this.cancellationTokenSource.Token;
            await StartMultipleDelaysAsync(token);
        }

        [RelayCommand]
        async Task Stop()
        {
            await Task.Run(() => this.cancellationTokenSource?.Cancel());
        }

        private async Task StartMultipleDelaysAsync(CancellationToken cancellationToken)
        {
            var longDelayTask = Task.Delay(TimeSpan.FromSeconds(15), cancellationToken);
            var shortDelayTask = Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            var mediumDelayTask = Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);

            delayTasks.TryAdd(0, longDelayTask);
            delayTasks.TryAdd(1, shortDelayTask);
            delayTasks.TryAdd(2, mediumDelayTask);

            try
            {
                while (delayTasks.Count > 0)
                {
                    var delayFinishedTask = await Task.WhenAny(delayTasks.Values);
                    await delayFinishedTask;
                    var key = delayTasks.Where(t => t.Value == delayFinishedTask).Select(k => k.Key).First();
                    delayTasks.Remove(key, out _);
                }
            }
            catch (TaskCanceledException ex)
            {
                InfoLabel = $"Process was cancelled";
            }
        }
    }
}