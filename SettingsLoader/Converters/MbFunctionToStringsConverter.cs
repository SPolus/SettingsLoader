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
                case 1: result = "Read Coils"; break;

                case 2: result = "Read Discrete"; break;

                case 3: result = "Read Holding Registers"; break;

                case 4: result = "Read Input Registers"; break;

                case 5: result = "Write Single Coil"; break;

                case 6: result = "Write Single Register"; break;

                case 15: result = "Write Multiple Coils"; break;

                case 16: result = "Write Multiple Registers"; break;

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
                case "Read Coils": result = 1; break;

                case "Read Discrete": result = 2; break;

                case "Read Holding Registers": result = 3; break;

                case "Read Input Registers": result = 4; break;

                case "Write Single Coil": result = 5; break;

                case "Write Single Register": result = 6; break;

                case "Write Multiple Coils": result = 15; break;

                case "Write Multiple Registers": result = 16; break;

                default: result = 0; break;
            }

            return result;
        }

    }
}
