using System;
using System.Collections.Generic;

namespace Utils
{
    public static class CalcUtils
    {
        private static readonly int CharA = Convert.ToInt32('a');

        private static readonly Dictionary<int, string> Units = new Dictionary<int, string>
        {
            {0, ""},
            {1, "K"},
            {2, "M"},
            {3, "B"},
            {4, "T"}
        };

        public static string FormatNumber(double value, bool discardFloating = false)
        {
            if (double.IsNaN(value))
            {
                return "";
            }

            var newVal = value > 0 ? value : (value * -1);

            if (newVal < 1d)
            {
                return "0";
            }

            var n = (int) Math.Log(newVal, 1000);
            var m = newVal / Math.Pow(1000, n);
            var unit = "";

            if (n < Units.Count)
            {
                unit = Units[n];
            }
            else
            {
                var unitInt = n - Units.Count;
                var secondUnit = unitInt % 26;
                var firstUnit = unitInt / 26;
                unit = Convert.ToChar(firstUnit + CharA) + Convert.ToChar(secondUnit + CharA).ToString();
            }

            return (value > 0 ? "" : "-") + (Math.Floor(m * 100) / 100).ToString(discardFloating ? "0" : "0.##") + unit;
        }
    }
}