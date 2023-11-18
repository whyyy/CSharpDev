
namespace DrawingLuckyNumber.Infrastructure
{
    using System.ComponentModel;
    using System.Windows;
    using Prism;
    using Prism.Events;
    using Prism.Ioc;

    public class ShellBootstrapper
    {
        public new Container Container { get; set; }
        public static IEventAggregator EventAggregator { get; set; } = new EventAggregator();
    }
}