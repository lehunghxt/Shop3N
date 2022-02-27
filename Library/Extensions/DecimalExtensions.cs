namespace Library.Extensions
{
    using System;
    using System.Globalization;

    /// <summary>
    /// The decimal extensions.
    /// </summary>
    public static class DecimalExtensions
    {
        /// <summary>
        /// The to o data string.
        /// </summary>
        /// <param name="number">
        /// The number.
        /// </param>
        /// <param name="formattingStyle">
        /// The formatting style.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToODataString(this decimal number, ValueFormatter.FormattingStyle formattingStyle)
        {
            var value = number.ToString("F", CultureInfo.InvariantCulture);
            return string.Format(@"{0}M", value);
        }

        /// <summary>
        /// The to o data string.
        /// </summary>
        /// <param name="number">
        /// The number.
        /// </param>
        /// <param name="stringFormat">
        /// 0,0.2
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Format(this decimal number, string stringFormat)
        {
            if (stringFormat.Length > 1)
            {
                var pattern = "{0:#,##0.#####}";
                if (stringFormat.Length == 3)
                {
                    pattern = "#,##0.";

                    int tp = 0;
                    int.TryParse(stringFormat[2].ToString(), out tp);
                    if (tp > 0)
                    {
                        var pad = 6 + tp;
                        pattern = "{0:" + pattern.PadRight(pad, '0') + "}";
                    }
                    else
                    {
                        pattern = "{0:#,##0}";
                    }
                }

                var newnum = string.Format(pattern, number);

                var revalue = string.Empty;
                var arr = newnum.Split('.');
                if (arr.Length > 0) revalue = arr[0].Replace(',', stringFormat[0]);
                if (arr.Length > 1 && Convert.ToInt32(arr[1]) > 0) revalue += stringFormat[1] + arr[1];
                return revalue;
            }
            return stringFormat;
        }
    }
}