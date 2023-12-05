namespace DrawingLuckyNumberApp.UI.Controls.ValidationRules
{
    using System.Globalization;
    using System.Windows.Controls;

    public class RangeValueValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value,
                                                  CultureInfo cultureInfo)
        {
            try
            {
                if (value.ToString().Length > 0)
                {
                    Int32.Parse(value.ToString());
                }
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, $"Unable to parse: '{value}' to int");
            }

            return ValidationResult.ValidResult;
        }
    }
}