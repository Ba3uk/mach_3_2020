
using System;

namespace Logic
{
    public static class ColorHelper
    {
        private static int maxEnumValue;
        private static Random random = new Random();

        /// <summary>
        /// Кол-во цветов 
        /// </summary>
        private static int MaxColorValue
        {
            get
            {
                if (maxEnumValue == 0)
                {
                    var colors = Enum.GetValues(typeof(ColorType));
                    maxEnumValue = colors.Length;
                }
                return maxEnumValue;
            }
        }

        /// <summary>
        /// Взять случайный цвет
        /// </summary>
        public static ColorType GetRandomColor()
        {
            return (ColorType)random.Next(0, MaxColorValue);
        }

        public static string GetColorCode(ColorType color)
        {
            switch (color)
            {
                case ColorType.Green: return "#008000";
                case ColorType.Pink: return "#ffc0cb";
                case ColorType.Yellow: return "#ffff00";
                case ColorType.Blue: return "#3A75C4";
                case ColorType.Red: return "#981915";
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}
