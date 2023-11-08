namespace SimpleButtonsAppMultipleDelaysTests
{
    using FluentAssertions;
    using NSubstitute;
    using NUnit.Framework;
    using SimpleButtonsApp;

    public class MainWindowViewModelTests
    {
        private MainWindowViewModel sut;

        [SetUp]
        public void Setup()
        {
            sut = new MainWindowViewModel();
        }

        [Test]
        [Ignore("Infinite loop without returning true, to avoid spamming of new tasks creation")]
        public async Task Should_return_true_when_infinite_loop_is_running()
        {
            //given

            //when

            var result = await sut.IsInfiniteLoopRunningAsync(CancellationToken.None);

            //then

            result.Should().BeTrue();
        }

        [Test]
        public async Task Should_throw_task_canceled_exception_when_cancelled_token()
        {
            //given
            var cancellationTokenSource = Substitute.For<CancellationTokenSource>();
            cancellationTokenSource.Cancel();

            //when

            var result = async () => await sut.IsInfiniteLoopRunningAsync(cancellationTokenSource.Token);

            //then

            await result.Should().ThrowAsync<TaskCanceledException>();
        }

        [Test]
        public async Task Should_catch_exception_when_start_multiple_delays_and_cancelled_token()
        {
            //given
            var cancellationTokenSource = Substitute.For<CancellationTokenSource>();
            cancellationTokenSource.Cancel();

            //when

            var result = async () => await sut.StartInfiniteLoop(cancellationTokenSource.Token);

            //then

            await result.Should().NotThrowAsync();
        }
    }
}