namespace DrawingLuckyNumberModule.Tests;

using DrawingLuckyNumber.Core.Events;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Prism.Events;
using ViewModels;

public class DrawLuckyNumberViewModelTests
{
    private DrawLuckyNumberViewModel sut;

    private IEventAggregator eventAggregator;

    [SetUp]
    public void Setup()
    {
        this.eventAggregator = Substitute.For<IEventAggregator>();
        this.sut = new DrawLuckyNumberViewModel(this.eventAggregator);
    }

    [Test]
    public void Should_publish_events_when_start_drawing()
    {
        //given
        var drawingTotalTimeInSecondsEvent = Substitute.For<DrawingTotalTimeInSecondsEvent>();
        this.eventAggregator.GetEvent<DrawingTotalTimeInSecondsEvent>().Returns(drawingTotalTimeInSecondsEvent);
        var isDrawingInProgressEvent = Substitute.For<IsDrawingInProgressEvent>();
        this.eventAggregator.GetEvent<IsDrawingInProgressEvent>().Returns(isDrawingInProgressEvent);
        var luckyNumberDrawnEvent = Substitute.For<LuckyNumberDrawnEvent>();
        this.eventAggregator.GetEvent<LuckyNumberDrawnEvent>().Returns(luckyNumberDrawnEvent);

        //when
        this.sut.StartDrawing();

        //then
        isDrawingInProgressEvent.Received().Publish(true);
        luckyNumberDrawnEvent.Received().Publish(Arg.Any<int>());
        drawingTotalTimeInSecondsEvent.Received().Publish(0);
    }

    [Test]
    public async Task Should_return_integer_in_range_when_start_drawing_lucky_number()
    {
        //given
        var cancellationTokenSource = Substitute.For<CancellationTokenSource>(TimeSpan.FromMilliseconds(1000));
        var luckyNumberDrawnEvent = Substitute.For<LuckyNumberDrawnEvent>();
        this.eventAggregator.GetEvent<LuckyNumberDrawnEvent>().Returns(luckyNumberDrawnEvent);

        //when
        var luckyNumber = await this.sut.StartDrawingLuckyNumber(cancellationTokenSource.Token);

        //then
        luckyNumber.Should().BeInRange(1, 99);
    }

    [Test]
    public async Task Should_throw_operation_canceled_exception_when_cancelled_token()
    {
        //given
        var cancellationTokenSource = Substitute.For<CancellationTokenSource>();
        cancellationTokenSource.Cancel();
        var luckyNumberDrawnEvent = Substitute.For<LuckyNumberDrawnEvent>();
        this.eventAggregator.GetEvent<LuckyNumberDrawnEvent>().Returns(luckyNumberDrawnEvent);

        //when
        var result = async () => await this.sut.StartDrawingLuckyNumber(cancellationTokenSource.Token);

        //then
        await result.Should().ThrowAsync<OperationCanceledException>();
    }

    [Test]
    public void Should_publish_event_when_stop_drawing()
    {
        //given
        var isDrawingInProgressEvent = Substitute.For<IsDrawingInProgressEvent>();
        var drawingTotalTimeInSeconds = Substitute.For<DrawingTotalTimeInSecondsEvent>();
        this.eventAggregator.GetEvent<IsDrawingInProgressEvent>().Returns(isDrawingInProgressEvent);
        this.eventAggregator.GetEvent<DrawingTotalTimeInSecondsEvent>().Returns(drawingTotalTimeInSeconds);

        //when
        this.sut.StopDrawing();

        //then
        isDrawingInProgressEvent.Received().Publish(false);
        drawingTotalTimeInSeconds.Received().Publish(0);
    }
}