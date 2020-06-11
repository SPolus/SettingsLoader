using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace SettingsLoader.Converters
{
    public class MbFunctionToStringsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<int> functions)
            {
                return ConvertListOfIntToListOfString(functions);
            }

            if (value is int function)
            {
                return ConvertIntToString(function);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<string> functions)
            {
                return ConvertListOfStringToListOfInt(functions);
            }

            if (value is string function)
            {
                return ConvertStringToInt(function);
            }

            return null;
        }

        private IEnumerable<string> ConvertListOfIntToListOfString(IEnumerable<int> functions)
        {
            List<string> results = new List<string>();

            foreach (var function in functions)
            {
                results.Add(ConvertIntToString(function));
            }

            return results;
        }

        private string ConvertIntToString(int function)
        {
            string result;

            switch (function)
            {
                case 1: result = "01: Read Coils (0x01)"; break;

                case 2: result = "02: Read Discrete (0x02)"; break;

                case 3: result = "03: Read Holding Registers (0x03)"; break;

                case 4: result = "04: Read Input Registers (0x04)"; break;

                case 5: result = "05: Write Single Coil (0x05)"; break;

                case 6: result = "06: Write Single Register (0x06)"; break;

                case 15: result = "15: Write Multiple Coils (0x0F)"; break;

                case 16: result = "16: Write Multiple Registers (0x10)"; break;

                default: result = ""; break;
            }

            return result;
        }

        private IEnumerable<int> ConvertListOfStringToListOfInt(IEnumerable<string> functions)
        {
            List<int> results = new List<int>();

            foreach (var function in functions)
            {
                results.Add(ConvertStringToInt(function));
            }

            return results;
        }

        private int ConvertStringToInt(string function)
        {
            int result;

            switch (function)
            {
                case "01: Read Coils (0x01)": result = 1; break;

                case "02: Read Discrete (0x02)": result = 2; break;

                case "03: Read Holding Registers (0x03)": result = 3; break;

                case "04: Read Input Registers (0x04)": result = 4; break;

                case "05: Write Single Coil (0x05)": result = 5; break;

                case "06: Write Single Register (0x06)": result = 6; break;

                case "15: Write Multiple Coils (0x0F)": result = 15; break;

                case "16: Write Multiple Registers (0x10)": result = 16; break;

                default: result = 0; break;
            }

            return result;
        }

    }
}
