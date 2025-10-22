using System.Text;
using System.Text.RegularExpressions;

namespace Library
{
    /// <summary>
    /// Provides basic cryptographic functionality based on custom
    /// substitution (S-Box) and permutation (P-Box) transformations.
    /// </summary>
    /// <remarks>
    /// This class implements a toy block cipher using two rounds
    /// of XOR operations, substitution, and permutation.
    /// </remarks>
    public sealed class Cryptography
    {
        private readonly Dictionary<Int32, Int32> pBox, sBox, reverseSBox, reversePBox;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cryptography"/> class
        /// and configures the S-Box and P-Box mappings, including their inverses.
        /// </summary>
        public Cryptography()
        {
            this.pBox = new Dictionary<Int32, Int32>()
            {
                { 0, 4 }, { 1, 6 }, { 2, 1 },
                { 3, 11 }, { 4, 8 }, { 5, 5 },
                { 6, 3 }, { 7, 2 }, { 8, 10 },
                { 9, 7 }, { 10, 0 }, { 11, 9 }
            };
            this.sBox = new Dictionary<Int32, Int32>()
            {
                { 0, 4 }, { 1, 2 }, { 2, 6 }, { 3, 1 },
                { 4, 7 }, { 5, 0 }, { 6, 5 }, { 7, 3 },
            };

            // Key-value inversion.
            this.reversePBox = this.pBox.ToDictionary(x => x.Value, x => x.Key);
            this.reverseSBox = this.sBox.ToDictionary(x => x.Value, x => x.Key);
        }

        /// <summary>
        /// Applies a P-Box permutation to a binary string.
        /// </summary>
        /// <param name="binaryString">The binary string to permute.</param>
        /// <param name="useInverse">
        /// <see langword="true"/> to apply the inverse permutation; otherwise <see langword="false"/>.
        /// </param>
        /// <returns>The permuted binary string.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the binary string is null, empty, or contains invalid characters.
        /// </exception>
        private String GetPBoxResult(String binaryString, Boolean useInverse = false)
        {
            if (string.IsNullOrEmpty(binaryString) || !Regex.IsMatch(binaryString, "^[01]+$"))
                throw new ArgumentException("Binary string must not be null, empty, or contain characters other than 0 and 1.", nameof(binaryString));

            Dictionary<Int32, Int32> map = useInverse ? this.reversePBox : this.pBox;

            Char[] result = new Char[binaryString.Length];
            foreach (KeyValuePair<Int32, Int32> pair in map)
            {
                int fromIndex = pair.Key;
                int toIndex = pair.Value;
                result[toIndex] = binaryString[fromIndex];
            }
            return new String(result);
        }

        /// <summary>
        /// Applies a P-Box permutation to a list of binary strings.
        /// </summary>
        /// <param name="binaryStrings">The list of binary strings to permute.</param>
        /// <param name="useInverse">
        /// <see langword="true"/> to apply the inverse permutation; otherwise <see langword="false"/>.
        /// </param>
        /// <returns>The concatenated permuted binary string.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the list is null, empty, or contains invalid strings.
        /// </exception>
        private String GetPBoxResult(List<String> binaryStrings, Boolean useInverse = false)
        {
            if (binaryStrings == null || binaryStrings.Count == 0)
                throw new ArgumentException("Binary string list cannot be null or empty", nameof(binaryStrings));

            foreach (String str in binaryStrings)
            {
                if (String.IsNullOrEmpty(str) || !Regex.IsMatch(str, "^[01]+$"))
                    throw new ArgumentException("Each string in the list must not be null, empty, or contain characters other than 0 and 1");
            }

            StringBuilder temp = new StringBuilder();
            foreach (String part in binaryStrings)
                temp.Append(this.GetPBoxResult(part, useInverse));

            return temp.ToString();
        }

        /// <summary>
        /// Applies an S-Box substitution to a binary string.
        /// </summary>
        /// <param name="binaryString">The binary string to substitute.</param>
        /// <param name="useInverse">
        /// <see langword="true"/> to apply the inverse substitution; otherwise <see langword="false"/>.
        /// </param>
        /// <returns>The substituted binary string.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the string is invalid or its length is not a multiple of 3.
        /// </exception>
        private String GetSBoxResult(String binaryString, Boolean useInverse = false)
        {
            if (String.IsNullOrEmpty(binaryString) || !Regex.IsMatch(binaryString, "^[01]+"))
                throw new ArgumentException("Binary string must not be null, empty, or contain characters other than 0 and 1.", nameof(binaryString));

            Dictionary<Int32, Int32> map = useInverse ? this.reverseSBox : this.sBox;

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < binaryString.Length; i += 3)
            {
                if (i + 3 > binaryString.Length)
                    throw new ArgumentException("Binary string length must be a multiple of 3.");

                String triplet = binaryString.Substring(i, 3);
                Int32 decimalValue = Operation.BinaryToDecimal(triplet);

                if (!map.TryGetValue(decimalValue, out Int32 substitutedValue))
                    throw new ArgumentException($"SBox does not contain a mapping for value: {decimalValue}");

                String substitutedBinary = Convert.ToString(substitutedValue, 2).PadLeft(3, '0');
                result.Append(substitutedBinary);
            }

            return result.ToString();
        }

        /// <summary>
        /// Applies an S-Box substitution to a list of binary strings.
        /// </summary>
        /// <param name="binaryStrings">The list of binary strings to substitute.</param>
        /// <param name="useInverse">
        /// <see langword="true"/> to apply the inverse substitution; otherwise <see langword="false"/>.
        /// </param>
        /// <returns>The concatenated substituted binary string.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the list is null, empty, contains invalid characters,
        /// or any string has a length not divisible by 3.
        /// </exception>
        private String GetSBoxResult(List<String> binaryStrings, Boolean useInverse = false)
        {
            if (binaryStrings == null || binaryStrings.Count == 0)
                throw new ArgumentException("Binary string list cannot be null or empty", nameof(binaryStrings));

            foreach (String str in binaryStrings)
            {
                if (String.IsNullOrEmpty(str) || !Regex.IsMatch(str, "^[01]+"))
                    throw new ArgumentException("Each string in the list must not be null, empty, or contain characters other than 0 and 1.");
                if (str.Length % 3 != 0)
                    throw new ArgumentException("Each binary string must have a length divisible by 3.");
            }

            StringBuilder result = new StringBuilder();
            foreach (String part in binaryStrings)
                result.Append(this.GetSBoxResult(part, useInverse));

            return result.ToString();
        }

        /// <summary>
        /// Encrypts plaintext using two keys with two rounds of
        /// XOR, S-Box substitution, and P-Box permutation.
        /// </summary>
        /// <param name="plaintext">The plaintext to encrypt.</param>
        /// <param name="key1">The first key.</param>
        /// <param name="key2">The second key.</param>
        /// <returns>The encrypted ciphertext.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when any input string is null or empty.
        /// </exception>
        public String Encrypt(String plaintext, String key1, String key2)
        {
            if (String.IsNullOrEmpty(plaintext))
                throw new ArgumentException("Plaintext must not be null or empty.", nameof(plaintext));
            if (String.IsNullOrEmpty(key1))
                throw new ArgumentException("Key1 must not be null or empty.", nameof(key1));
            if (String.IsNullOrEmpty(key2))
                throw new ArgumentException("Key2 must not be null or empty.", nameof(key2));

            String binaryPlaintext = Operation.TextToBinary(plaintext);
            String binaryKey1 = Operation.TextToBinary(key1);
            String binaryKey2 = Operation.TextToBinary(key2);

            Operation.AddMissingZeros(ref binaryPlaintext, ref binaryKey1, ref binaryKey2);

            String xor = Operation.Xor(binaryPlaintext, binaryKey1);
            List<String> splitXor = Operation.SplitBinaryString(xor);
            String pBoxResult = this.GetPBoxResult(splitXor);
            List<String> splitPBoxResult = Operation.SplitBinaryString(pBoxResult);
            String sBoxResult = this.GetSBoxResult(splitPBoxResult);

            xor = Operation.Xor(binaryKey1, binaryKey2);
            xor = Operation.Xor(sBoxResult, xor);
            splitXor = Operation.SplitBinaryString(xor);
            pBoxResult = this.GetPBoxResult(splitXor);
            splitPBoxResult = Operation.SplitBinaryString(pBoxResult);
            sBoxResult = this.GetSBoxResult(splitPBoxResult);

            String result = Operation.BinaryToText(sBoxResult);

            return result;
        }

        /// <summary>
        /// Decrypts ciphertext using two keys by reversing
        /// the two rounds of XOR, substitution, and permutation.
        /// </summary>
        /// <param name="ciphertext">The ciphertext to decrypt.</param>
        /// <param name="key1">The first key.</param>
        /// <param name="key2">The second key.</param>
        /// <returns>The decrypted plaintext.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when any input string is null or empty.
        /// </exception>
        public String Decrypt(String ciphertext, String key1, String key2)
        {
            if (String.IsNullOrEmpty(ciphertext))
                throw new ArgumentException("Ciphertext must not be null or empty.", nameof(ciphertext));
            if (String.IsNullOrEmpty(key1))
                throw new ArgumentException("Key1 must not be null or empty.", nameof(key1));
            if (String.IsNullOrEmpty(key2))
                throw new ArgumentException("Key2 must not be null or empty.", nameof(key2));

            String binaryCiphertext = Operation.TextToBinary(ciphertext);
            Operation.RemovingExtraZeros(ref binaryCiphertext);
            String binaryKey1 = Operation.TextToBinary(key1);
            String binaryKey2 = Operation.TextToBinary(key2);
            Operation.AddMissingZeros(ref binaryCiphertext, ref binaryKey1, ref binaryKey2);

            // Reverse of round 2
            List<String> splitBinaryString = Operation.SplitBinaryString(binaryCiphertext);
            String sBoxInverseResult = this.GetSBoxResult(splitBinaryString, true);
            List<String> splitSBoxResult = Operation.SplitBinaryString(sBoxInverseResult);
            String pBoxInverseResult = this.GetPBoxResult(splitSBoxResult, true);

            String xor = Operation.Xor(binaryKey1, binaryKey2);
            xor = Operation.Xor(pBoxInverseResult, xor);

            // Reverse of round 1
            List<String> splitXor = Operation.SplitBinaryString(xor);
            sBoxInverseResult = this.GetSBoxResult(splitXor, true);
            splitSBoxResult = Operation.SplitBinaryString(sBoxInverseResult);
            pBoxInverseResult = this.GetPBoxResult(splitSBoxResult, true);

            xor = Operation.Xor(pBoxInverseResult, binaryKey1);

            xor = Operation.BinaryToText(xor);

            return xor;
        }
    }
}