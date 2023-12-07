namespace DrawingLuckyNumberApp.UI.Extensions
{
    using System.Windows.Markup;

    public class RangeInformationMarkup : MarkupExtension
    {
        public RangeInformationMarkup()
        {
        }

        public int SomeNumber { get; set; }
        public string NumberName { get; set; }
        public string Information { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return $"{this.NumberName}, number: {this.SomeNumber} {this.Information}";
        }
    }
}