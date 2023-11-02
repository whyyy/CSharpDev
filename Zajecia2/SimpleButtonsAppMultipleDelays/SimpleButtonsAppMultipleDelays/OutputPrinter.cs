using System.Diagnostics;

namespace SimpleButtonsApp
{
    public class OutputPrinter
    {
        public static void Print(string message)
        {
            Trace.WriteLine(message);
        }
    }
}
