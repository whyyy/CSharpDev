namespace DrawingLuckyNumberApp.UI.Converters
{
    using System.Globalization;
    using System.Windows.Data;
    using DrawingLuckyNumber.Core;

    public class DrawingStatusEnumToStringConverter : IValueConverter
    {
        public static DrawingStatusEnumToStringConverter Converter { get; set; }

        static DrawingStatusEnumToStringConverter()
        {
            Converter = new DrawingStatusEnumToStringConverter();
        }
        
        public object Convert(object value,
                              Type targetType,
                              object parameter,
                              CultureInfo culture)
        {
            if (value == null || parameter == null) 
                return "Incorrectly setting drawing status";

            if ((DrawingStatus)value == (DrawingStatus)parameter)
            {
                return "Drawing was not done yet";
            }

            var drawingStatus = (DrawingStatus)value;

            return GetDrawingStatusText(drawingStatus);
        }

        public object ConvertBack(object value,
                                  Type targetType,
                                  object parameter,
                                  CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string GetDrawingStatusText(DrawingStatus drawingStatus)
        {
            switch (drawingStatus)
            {
                default:
                case DrawingStatus.None:
                    return string.Empty;
                case DrawingStatus.InProgress:
                    return "Drawing is in progress";
                case DrawingStatus.Finished:
                    return "Drawing finished";
            }
        }
    }
}
