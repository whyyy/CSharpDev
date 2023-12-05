namespace DrawingLuckyNumber.Core.Events
{
    using System.Collections.ObjectModel;
    using Prism.Events;

    public class AllLuckyNumbersDrawnEvent : PubSubEvent<ObservableCollection<LuckyNumber>>
    {
    }
}