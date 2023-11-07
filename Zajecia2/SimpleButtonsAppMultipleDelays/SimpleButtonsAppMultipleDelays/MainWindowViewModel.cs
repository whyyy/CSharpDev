namespace SimpleButtonsApp
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using System.Threading;
    using System.Threading.Tasks;

    public partial class MainWindowViewModel : ObservableObject
    {
        private CancellationTokenSource cancellationTokenSource;

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
            await StartMultipleDelaysAsync(token);
        }

        [RelayCommand]
        async Task Stop()
        {
            await Task.Run(() => this.cancellationTokenSource?.Cancel());
        }

        public async Task StartMultipleDelaysAsync(CancellationToken cancellationToken)
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
                    return true;
                }
                throw new TaskCanceledException();
            });
        }
    }
}