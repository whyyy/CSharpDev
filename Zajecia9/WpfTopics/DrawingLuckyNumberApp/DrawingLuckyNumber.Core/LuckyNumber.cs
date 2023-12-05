namespace DrawingLuckyNumber.Core
{
    public class LuckyNumber
    {
        public LuckyNumber(string value)
        {
            this.Value = value;
        }

        public string Value { get; set; }

        public bool IsEven
        {
            get
            {
                Int32.TryParse(this.Value, out var number);
                return number % 2 == 0;
            }
        }
    }
}