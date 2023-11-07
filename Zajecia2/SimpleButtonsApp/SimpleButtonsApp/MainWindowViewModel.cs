using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleButtonsApp
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private CancellationTokenSource _cancellationTokenSource;

        private TimeSpan _timeout = TimeSpan.FromSeconds(10);

        [ObservableProperty]
        private string _infoLabel;

        public MainWindowViewModel()
        {
        }

        [RelayCommand]
        async Task Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            InfoLabel = "Start button was clicked";
            var token = _cancellationTokenSource.Token;
            try
            {
                await StartDelayAsync(token);
            }
            catch (OperationCanceledException ex)
            {
                InfoLabel = "Process was cancelled";
            }
            finally 
            {
                _cancellationTokenSource.Dispose(); 
            }
        }

        [RelayCommand]
        async Task Stop(CancellationToken cancellationToken)
        {
            await Task.Run(() =>_cancellationTokenSource.Cancel(), cancellationToken);
        }

        private async Task StartDelayAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(_timeout, cancellationToken);
        }
    }
}