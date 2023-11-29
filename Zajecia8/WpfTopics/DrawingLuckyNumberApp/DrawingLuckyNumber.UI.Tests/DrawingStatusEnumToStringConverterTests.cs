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
        
        [TestCase(null, null, "Incorrectly setting drawing status")]
        [TestCase(null, DrawingStatus.Finished, "Incorrectly setting drawing status")]
        [TestCase(DrawingStatus.None, DrawingStatus.None, "Drawing was not done yet")]
        [TestCase(DrawingStatus.Finished, DrawingStatus.None, "Drawing finished")]
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