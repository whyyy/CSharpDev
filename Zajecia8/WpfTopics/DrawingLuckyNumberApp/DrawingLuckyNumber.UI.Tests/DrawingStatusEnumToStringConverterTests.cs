namespace DrawingLuckyNumberApp.UI.Tests
{
    using System.Globalization;
    using Converters;
    using DrawingLuckyNumber.Core;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class DrawingStatusEnumToStringConverterTests
    {
        private readonly DrawingStatusEnumToStringConverter sut = DrawingStatusEnumToStringConverter.Converter;
        
        [TestCase(null, null, "")]
        [TestCase(null, DrawingStatus.Finished, "")]
        [TestCase(DrawingStatus.None, DrawingStatus.None, "")]
        [TestCase(null, DrawingStatus.Finished, "")]
        [TestCase(DrawingStatus.Finished, DrawingStatus.Finished, "Drawing finished")]
        [TestCase(DrawingStatus.InProgress, DrawingStatus.Finished, "Drawing is in progress")]
        public void Drawing_status_converter_tests(object value, object parameter, string convertedText)
        {
            //given

            //when
            var result = this.sut.Convert(value: value, 
                                          targetType: null, 
                                          parameter: parameter,
                                          culture: CultureInfo.InvariantCulture);
            //then
            result.Should().Be(convertedText);
        }
    }
}