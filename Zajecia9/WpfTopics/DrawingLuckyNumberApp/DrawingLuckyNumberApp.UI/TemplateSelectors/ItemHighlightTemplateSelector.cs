namespace DrawingLuckyNumberApp.UI.TemplateSelectors
{
    using System.Windows;
    using System.Windows.Controls;
    using DrawingLuckyNumber.Core;

    public class ItemHighlightTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EvenTemplate { get; set; }
        public DataTemplate OddTemplate { get; set; }
        public string PropertyToEvaluate
        {
            get; set;
        }

        public override DataTemplate SelectTemplate(
            object item,
            DependencyObject container)
        {
            var luckyNumber = (LuckyNumber)item;
            var type = luckyNumber?.GetType();
            var propertyInfo = type?.GetProperty(this.PropertyToEvaluate);

            if (propertyInfo != null && (bool)propertyInfo.GetValue(luckyNumber, null)! == true)
            {
                return this.EvenTemplate;
            }
            return this.OddTemplate;
        }
    }
}