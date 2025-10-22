namespace Library
{
    /// <summary>
    /// Represents a collection of special control and whitespace characters
    /// with their corresponding decimal values.
    /// </summary>
    /// <remarks>
    /// The class is <see langword="sealed"/> and cannot be inherited.
    /// It uses an internal <see cref="Dictionary{Char, Int32}"/> 
    /// to map special characters to their decimal codes.
    /// </remarks>
    public sealed class SpecialCharacters
    {
        private readonly Dictionary<Char, Int32> sc;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecialCharacters"/> class.
        /// </summary>
        /// <remarks>
        /// The constructor pre-populates the mapping with standard ASCII control 
        /// characters (0–32), including whitespace such as <c>Tab</c>, 
        /// <c>Line Feed</c>, <c>Carriage Return</c>, and <c>Space</c>.
        /// </remarks>
        public SpecialCharacters()
        {
            this.sc = new Dictionary<Char, Int32>()
            {
                { '␀', 0 },  // Null
                { '␁', 1 },  // Start of Heading
                { '␂', 2 },  // Start of Text
                { '␃', 3 },  // End of Text
                { '␄', 4 },  // End of Transmission
                { '␅', 5 },  // Enquiry
                { '␆', 6 },  // Acknowledge
                { '␇', 7 },  // Bell
                { '␈', 8 },  // Backspace
                { '␉', 9 },  // Horizontal Tab
                { '␊', 10 }, // Line Feed
                { '␋', 11 }, // Vertical Tab
                { '␌', 12 }, // Form Feed
                { '␍', 13 }, // Carriage Return
                { '␎', 14 }, // Shift Out
                { '␏', 15 }, // Shift In
                { '␐', 16 }, // Data Link Escape
                { '␑', 17 }, // Device Control 1
                { '␒', 18 }, // Device Control 2
                { '␓', 19 }, // Device Control 3
                { '␔', 20 }, // Device Control 4
                { '␕', 21 }, // Negative Acknowledge
                { '␖', 22 }, // Synchronous Idle
                { '␗', 23 }, // End of Transmission Block
                { '␘', 24 }, // Cancel
                { '␙', 25 }, // End of Medium
                { '␚', 26 }, // Substitute
                { '␛', 27 }, // Escape
                { '␜', 28 }, // File Separator
                { '␝', 29 }, // Group Separator
                { '␞', 30 }, // Record Separator
                { '␟', 31 }, // Unit Separator
                { '␠', 32 }, // Space
            };
        }

        /// <summary>
        /// Gets the decimal value associated with a given special character.
        /// </summary>
        /// <param name="value">The character to look up.</param>
        /// <returns>The decimal value corresponding to the character.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the specified <paramref name="value"/> 
        /// is not found in the special character mapping.
        /// </exception>
        public Int32 GetDecimalValue(Char value)
        {
            if (!this.sc.TryGetValue(value, out Int32 dec))
                throw new ArgumentException($"SpecialCharacters does not contain a mapping for value: {value}");
            return dec;
        }

        /// <summary>
        /// Determines whether a given character is present 
        /// in the collection of special characters.
        /// </summary>
        /// <param name="value">The character to check.</param>
        /// <returns>
        /// <see langword="true"/> if the character is contained in the mapping; 
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public Boolean IsIn(Char value)
        {
            return this.sc.ContainsKey(value);  
        }
    }
}
