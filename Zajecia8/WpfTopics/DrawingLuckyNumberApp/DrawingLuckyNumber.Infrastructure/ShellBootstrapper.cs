namespace DrawingLuckyNumber.Infrastructure;

using Prism.Events;

public class ShellBootstrapper
{
    static ShellBootstrapper()
    {
        EventAggregator = new EventAggregator();
    }

    public static IEventAggregator EventAggregator { get; set; }
}