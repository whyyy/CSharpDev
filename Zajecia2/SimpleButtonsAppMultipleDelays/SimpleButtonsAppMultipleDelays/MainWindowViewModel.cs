﻿namespace SimpleButtonsApp
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using System;
    using System.Collections.Generic;
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

        private async Task StartMultipleDelaysAsync(CancellationToken cancellationToken)
        {
            var longDelayTask = Task.Delay(TimeSpan.FromSeconds(15), cancellationToken);
            var shortDelayTask = Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            var mediumDelayTask = Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);

            var delayTasks = new List<Task> { shortDelayTask, mediumDelayTask, longDelayTask };

            delayTasks.ForEach(t => OutputPrinter.Print($"Id: {t.Id}, status: {t.Status}, time: {DateTime.Now}"));

            try
            {
                while (delayTasks.Count > 0)
                {
                    var delayFinishedTask = await Task.WhenAny(delayTasks);

                    await delayFinishedTask;
                    OutputPrinter.Print($"Finished task, id: {delayFinishedTask.Id}, "
                                        + $"status: {delayFinishedTask.Status}, "
                                        + $"time: {DateTime.Now}");
                    delayTasks.Remove(delayFinishedTask);
                }
            }
            catch (TaskCanceledException ex)
            {
                InfoLabel = $"Process was cancelled";
                OutputPrinter.Print("Remaining tasks:");
                delayTasks.ForEach(t => OutputPrinter.Print($"Id: {t.Id}, status: {t.Status}, time: {DateTime.Now}"));
            }
        }
    }
}