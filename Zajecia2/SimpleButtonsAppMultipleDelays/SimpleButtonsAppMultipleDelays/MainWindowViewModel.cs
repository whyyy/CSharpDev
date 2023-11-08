namespace SimpleButtonsApp
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public partial class MainWindowViewModel : ObservableObject
    {
        private CancellationTokenSource cancellationTokenSource;
        private List<Task> runningTasks = new List<Task>();
        private string textToEdit;
        private readonly object editionLock = new object();

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
            runningTasks.Add(firstInfiniteLoop);
            runningTasks.Add(secondInfiniteLoop);

            while(runningTasks.Count > 0)
            {
                var finishedTask = await Task.WhenAny(runningTasks);
                runningTasks.Remove(finishedTask);
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
            catch (TaskCanceledException ex)
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
                    lock (editionLock)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            this.textToEdit = $"Task: {Task.CurrentId}; Count: {i}";
                            OutputPrinter.Print( this.textToEdit);
                        }
                    }
                }
                cancellationToken.ThrowIfCancellationRequested();
                return false;
            }, cancellationToken);
        }
    }
}