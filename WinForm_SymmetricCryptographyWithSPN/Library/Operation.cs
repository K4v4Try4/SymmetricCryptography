using System.Text;
using System.Text.RegularExpressions;

namespace Library
{
    /// <summary>
    /// Provides helper methods for text and binary operations,
    /// including conversions between text, binary, and decimal values.
    /// </summary>
    public static class Operation
    {
        /// <summary>
        /// Converts a text string into its binary representation.
        /// </summary>
        /// <param name="text">The text to convert.</param>
        /// <returns>A binary string representing the input text.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="text"/> is null or empty.
        /// </exception>
        public static String TextToBinary(String text)
        {
            if (String.IsNullOrEmpty(text))
                throw new ArgumentException("Text cannot be null or empty.", nameof(text));

            SpecialCharacters sc = new SpecialCharacters();
            Char[] charText = text.ToCharArray();
            StringBuilder binaryText = new StringBuilder();

            foreach (Char c in charText)
            {
                Int32 temp;

                if (sc.IsIn(c)) temp = sc.GetDecimalValue(c);
                else temp = Operation.CharToInt(c);
                String bin = DecimalToBinary(temp);
                binaryText.Append(bin);
            }

            return binaryText.ToString();
        }

        /// <summary>
        /// Converts a character to its decimal ASCII value.
        /// </summary>
        /// <param name="character">The character to convert.</param>
        /// <returns>The decimal representation of the character.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the character is outside the range 0–255.
        /// </exception>
        private static Int32 CharToInt(Char character)
        {
            if (character < 0 || character > 255)
                throw new ArgumentOutOfRangeException(nameof(character), "Character must be in the range of 0 to 255.");
            return ((Int32)character);
        }

        /// <summary>
        /// Converts a decimal ASCII value into its corresponding character.
        /// </summary>
        /// <param name="number">The decimal number to convert.</param>
        /// <returns>The character representation of the decimal number.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the number is outside the range 0–255.
        /// </exception>
        private static Char IntToChar(Int32 number)
        {
            if (number < 0 || number > 255)
                throw new ArgumentOutOfRangeException(nameof(number), "Number must be in the range of 0 to 255.");
            return ((Char)number);
        }

        /// <summary>
        /// Converts a decimal value into an 8-bit binary string.
        /// </summary>
        /// <param name="number">The decimal number to convert.</param>
        /// <returns>An 8-bit binary string representing the number.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the number is outside the range 0–255.
        /// </exception>
        private static String DecimalToBinary(Int32 number)
        {
            if (number < 0 || number > 255)
                throw new ArgumentOutOfRangeException(nameof(number), "Number must be in the range of 0 to 255.");
            // Converts the number to binary.
            String bin = Convert.ToString(number, 2);
            if (bin.Length < 8)
                // Adds missing zeros to the left up to 8 characters.
                bin = bin.PadLeft(8, '0');
            return bin;
        }

        /// <summary>
        /// Pads three binary strings with leading zeros so they all have the same length,
        /// adjusted to the nearest multiple of 12.
        /// </summary>
        /// <param name="binary1">The first binary string.</param>
        /// <param name="binary2">The second binary string.</param>
        /// <param name="binary3">The third binary string.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when any binary string is null, empty, or contains invalid characters.
        /// </exception>
        public static void AddMissingZeros(ref String binary1, ref String binary2, ref String binary3)
        {
            if (String.IsNullOrEmpty(binary1) || String.IsNullOrEmpty(binary2) || String.IsNullOrEmpty(binary3) || !Regex.IsMatch(binary1, "^[01]+") || !Regex.IsMatch(binary2, "^[01]+") || !Regex.IsMatch(binary3, "^[01]+"))
                throw new ArgumentException("Binary string must not be null, empty, or contain characters other than 0 and 1.");
            String[] someStr = { binary1, binary2, binary3 };
            // Finds the longest string length.
            Int32 longest = someStr.OrderByDescending(x => x.Length).First().Length;
            // If the longest string is not a multiple of 12, it will be adjusted to the next multiple of 12.
            if (longest % 12 != 0) longest = ((longest / 12) + 1) * 12;
            binary1 = binary1.PadLeft(longest, '0');
            binary2 = binary2.PadLeft(longest, '0');
            binary3 = binary3.PadLeft(longest, '0');
        }

        /// <summary>
        /// Pads a binary string with leading zeros so its length is a multiple of 12.
        /// </summary>
        /// <param name="binary">The binary string to pad.</param>
        public static void AddMissingZeros(ref String binary)
        {
            if (binary.Length % 12 != 0)
                binary = binary.PadLeft(((binary.Length / 12) + 1) * 12, '0');
        }

        /// <summary>
        /// Performs a bitwise XOR operation on two binary strings of equal length.
        /// </summary>
        /// <param name="binary1">The first binary string.</param>
        /// <param name="binary2">The second binary string.</param>
        /// <returns>A binary string representing the XOR result.</returns>
        public static String Xor(String binary1, String binary2)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < binary1.Length; i++)
                result.Append(binary1[i] == binary2[i] ? '0' : '1');
            return result.ToString();
        }

        /// <summary>
        /// Splits a binary string into 12-bit chunks.
        /// </summary>
        /// <param name="binaryString">The binary string to split.</param>
        /// <returns>A list of binary substrings, each up to 12 bits long.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the binary string is null, empty, or contains invalid characters.
        /// </exception>
        public static List<String> SplitBinaryString(String binaryString)
        {
            if (string.IsNullOrEmpty(binaryString) || !Regex.IsMatch(binaryString, "^[01]+$"))
                throw new ArgumentException("Binary string must not be null, empty, or contain characters other than 0 and 1.", nameof(binaryString));

            List<string> parts = new List<string>();

            for (int i = 0; i < binaryString.Length; i += 12)
            {
                // Extracts 12 characters or less if we are at the end of the string.
                String part = binaryString.Substring(i, Math.Min(12, binaryString.Length - i));
                parts.Add(part);
            }

            return parts;
        }

        /// <summary>
        /// Converts a binary string into its decimal representation.
        /// </summary>
        /// <param name="binaryString">The binary string to convert.</param>
        /// <returns>The decimal value of the binary string.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the binary string is null, empty, or contains invalid characters.
        /// </exception>
        public static Int32 BinaryToDecimal(String binaryString)
        {
            if (string.IsNullOrEmpty(binaryString) || !Regex.IsMatch(binaryString, "^[01]+$"))
                throw new ArgumentException("Binary string must not be null, empty, or contain characters other than 0 and 1.", nameof(binaryString));
            return Convert.ToInt32(binaryString, 2);
        }

        /// <summary>
        /// Converts a binary string into its text representation.
        /// </summary>
        /// <param name="binaryString">The binary string to convert.</param>
        /// <returns>The text decoded from the binary string.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the binary string is null, empty, or contains invalid characters.
        /// </exception>
        public static String BinaryToText(String binaryString)
        {
            if (String.IsNullOrEmpty(binaryString) || !Regex.IsMatch(binaryString, "^[01]+$"))
                throw new ArgumentException("Binary string must not be null, empty, or contain characters other than 0 and 1.", nameof(binaryString));

            Int32 remainder = binaryString.Length % 8;
            if (remainder != 0)
            {
                Int32 padding = 8 - remainder;
                binaryString = binaryString.PadLeft(binaryString.Length + padding, '0');
            }

            StringBuilder text = new StringBuilder();

            for (Int32 i = 0; i < binaryString.Length; i += 8)
            {
                String part = binaryString.Substring(i, 8);
                Int32 decimalValue = Operation.BinaryToDecimal(part);
                Char character;

                if (decimalValue >= 0 && decimalValue <= 31)
                    character = (char)(0x2400 + decimalValue);
                else
                    character = Operation.IntToChar(decimalValue);

                text.Append(character);
            }

            // Remove leading null characters (0x2400)
            return text.ToString().TrimStart('\u2400');
        }

        /// <summary>
        /// Removes unnecessary leading zeros from a binary string
        /// so its length is reduced to the nearest lower multiple of 12.
        /// </summary>
        /// <param name="binaryString">The binary string to adjust.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when the binary string is null, empty, or contains invalid characters.
        /// </exception>
        public static void RemovingExtraZeros(ref String binaryString)
        {
            if (String.IsNullOrEmpty(binaryString) || !Regex.IsMatch(binaryString, "^[01]+$"))
                throw new ArgumentException("Binary string must not be null, empty, or contain characters other than 0 and 1.", nameof(binaryString));

            // Calculate how many padding zeros to remove to get the previous multiple of 12
            Int32 currentLength = binaryString.Length;
            Int32 targetLength = (currentLength / 12) * 12;

            if (currentLength % 12 == 0)
                return;

            // Remove exactly as many zeros as needed from the beginning
            Int32 zerosToRemove = currentLength - targetLength;
            if (zerosToRemove > 0 && binaryString.StartsWith(new string('0', zerosToRemove)))
                binaryString = binaryString.Substring(zerosToRemove);
        }
    }
}