using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace CustomControls
{
    /// <summary>
    /// A custom text box control with support for rounded borders, animations,
    /// placeholder text, gradient backgrounds, and password masking.
    /// </summary>
    public class CCTextBox : UserControl
    {
        private TextBox _textBox;
        private Color _borderColor = Color.Silver;
        private Color _borderColorFocus = Color.RoyalBlue;
        private int _borderSize = 2;
        private int _borderRadius = 15;
        private Color _backgroundColor = Color.WhiteSmoke;
        private Color _backgroundGradientStart = Color.White;
        private Color _backgroundGradientEnd = Color.LightGray;
        private bool _useGradient = false;
        private Color _placeholderColor = Color.Gray;
        private string _placeholderText = "";
        private bool _isPasswordChar = false;
        private char _passwordChar = '●';
        private bool _multiline = false;
        private bool _underlinedStyle = false;

        private bool _isFocused = false;
        private bool _isHovering = false;

        private Timer _animationTimer;
        private float _animationProgress = 0f;

        // Questo è il punto cruciale: dichiara un nuovo evento 'TextChanged'
        // che nasconde quello del controllo base e permette al form principale di agganciarsi ad esso.
        public new event EventHandler TextChanged;


        /// <summary>
        /// Initializes a new instance of the <see cref="CCTextBox"/> class with default appearance settings.
        /// </summary>
        public CCTextBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.DoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            Size = new Size(220, 40);
            BackColor = Color.Transparent;
            Font = new Font("Segoe UI", 10F);
        }

        /// <inheritdoc/>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (!DesignMode)
            {
                InitializeTextBox();
                InitializeAnimation();
            }
        }

        /// <summary>
        /// Creates and initializes the internal <see cref="TextBox"/> control.
        /// </summary>
        private void InitializeTextBox()
        {
            _textBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                BackColor = _backgroundColor,
                Font = Font,
                ForeColor = ForeColor
            };

            // Aggancia gli eventi del controllo interno e invoca l'evento
            // 'TextChanged' del nostro controllo personalizzato.
            _textBox.Enter += (s, e) => { _isFocused = true; _animationTimer.Start(); Invalidate(); };
            _textBox.Leave += (s, e) => { _isFocused = false; _animationTimer.Start(); Invalidate(); };
            _textBox.TextChanged += (s, e) => { Invalidate(); TextChanged?.Invoke(this, e); };
            _textBox.MouseEnter += (s, e) => { _isHovering = true; _animationTimer.Start(); Invalidate(); };
            _textBox.MouseLeave += (s, e) => { _isHovering = false; _animationTimer.Start(); Invalidate(); };

            Controls.Add(_textBox);
            UpdateTextBoxBounds();
        }

        /// <summary>
        /// Initializes the border animation logic for focus and hover transitions.
        /// </summary>
        private void InitializeAnimation()
        {
            _animationTimer = new Timer();
            _animationTimer.Interval = 15;
            _animationTimer.Tick += (s, e) =>
            {
                float target = (_isFocused || _isHovering) ? 1f : 0f;
                float step = 0.1f;
                if (Math.Abs(_animationProgress - target) > 0.01f)
                {
                    _animationProgress += (target > _animationProgress ? step : -step);
                    _animationProgress = Math.Max(0f, Math.Min(1f, _animationProgress));
                    Invalidate();
                }
                else
                {
                    _animationProgress = target;
                    _animationTimer.Stop();
                }
            };
        }

        #region Custom Properties

        /// <summary>Gets or sets the default border color.</summary>
        [Category("Custom Appearance")]
        public Color BorderColor { get => _borderColor; set { _borderColor = value; Invalidate(); } }

        /// <summary>Gets or sets the border color when the control is focused.</summary>
        [Category("Custom Appearance")]
        public Color BorderColorFocus { get => _borderColorFocus; set { _borderColorFocus = value; Invalidate(); } }

        /// <summary>Gets or sets the border thickness.</summary>
        [Category("Custom Appearance")]
        public int BorderSize { get => _borderSize; set { _borderSize = value; UpdateTextBoxBounds(); Invalidate(); } }

        /// <summary>Gets or sets the border radius for rounded corners.</summary>
        [Category("Custom Appearance")]
        public int BorderRadius { get => _borderRadius; set { _borderRadius = value; UpdateRegion(); Invalidate(); } }

        /// <summary>Gets or sets the solid background color.</summary>
        [Category("Custom Appearance")]
        public Color BackgroundColor { get => _backgroundColor; set { _backgroundColor = value; Invalidate(); } }

        /// <summary>Gets or sets the starting color of the background gradient.</summary>
        [Category("Custom Appearance")]
        public Color BackgroundGradientStart { get => _backgroundGradientStart; set { _backgroundGradientStart = value; Invalidate(); } }

        /// <summary>Gets or sets the ending color of the background gradient.</summary>
        [Category("Custom Appearance")]
        public Color BackgroundGradientEnd { get => _backgroundGradientEnd; set { _backgroundGradientEnd = value; Invalidate(); } }

        /// <summary>Determines whether to use a gradient background instead of solid color.</summary>
        [Category("Custom Appearance")]
        public bool UseGradient { get => _useGradient; set { _useGradient = value; Invalidate(); } }

        /// <summary>Determines whether to use an underlined border style instead of a full rectangle.</summary>
        [Category("Custom Appearance")]
        public bool UnderlinedStyle { get => _underlinedStyle; set { _underlinedStyle = value; UpdateRegion(); Invalidate(); } }

        /// <summary>Gets or sets the placeholder text displayed when the box is empty.</summary>
        [Category("Custom Text")]
        public string PlaceholderText { get => _placeholderText; set { _placeholderText = value; Invalidate(); } }

        /// <summary>Gets or sets the color of the placeholder text.</summary>
        [Category("Custom Text")]
        public Color PlaceholderColor { get => _placeholderColor; set { _placeholderColor = value; Invalidate(); } }

        /// <summary>Determines whether the text is masked as a password.</summary>
        [Category("Custom Text")]
        public bool PasswordChar { get => _isPasswordChar; set { _isPasswordChar = value; UpdatePasswordChar(); } }

        /// <summary>Gets or sets the character used for password masking.</summary>
        [Category("Custom Text")]
        public char PasswordCharacter { get => _passwordChar; set { _passwordChar = value; UpdatePasswordChar(); } }

        /// <summary>Gets or sets whether the text box is multiline.</summary>
        [Category("Custom Text")]
        public bool Multiline
        {
            get => _multiline;
            set
            {
                _multiline = value;
                if (_textBox != null)
                {
                    _textBox.Multiline = value;
                    _textBox.ScrollBars = value ? ScrollBars.Vertical : ScrollBars.None;
                }
                if (value) Height = Math.Max(Height, 60);
                UpdateTextBoxBounds();
            }
        }

        // Ho unificato le due proprietà 'Text' per evitare confusione
        // e garantire che il form principale possa accedere al testo correttamente.
        /// <inheritdoc/>
        [Browsable(true)]
        public override string Text
        {
            get => _textBox?.Text ?? "";
            set { if (_textBox != null) _textBox.Text = value; }
        }

        /// <inheritdoc/>
        [Browsable(false)]
        public override Font Font
        {
            get => base.Font;
            set { base.Font = value; if (_textBox != null) _textBox.Font = value; UpdateTextBoxBounds(); }
        }

        /// <inheritdoc/>
        [Browsable(false)]
        public override Color ForeColor
        {
            get => base.ForeColor;
            set { base.ForeColor = value; if (_textBox != null) _textBox.ForeColor = value; }
        }

        #endregion

        /// <summary>
        /// Updates the password character display based on the current settings.
        /// </summary>
        private void UpdatePasswordChar()
        {
            if (_textBox != null) _textBox.PasswordChar = _isPasswordChar ? _passwordChar : '\0';
        }

        /// <summary>
        /// Adjusts the internal text box position and size based on border style and padding.
        /// </summary>
        private void UpdateTextBoxBounds()
        {
            if (_textBox == null) return;
            int padding = _borderSize + 6;
            if (_underlinedStyle)
            {
                _textBox.Location = new Point(5, (Height - _textBox.Height) / 2);
                _textBox.Size = new Size(Width - 10, _textBox.Height);
            }
            else
            {
                _textBox.Location = new Point(padding, (Height - _textBox.PreferredHeight) / 2);
                _textBox.Size = new Size(Width - padding * 2, _textBox.PreferredHeight);
            }
        }

        /// <inheritdoc/>
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            int radius = Math.Min(_borderRadius, Math.Min(Width, Height) / 2);

            // Draw background
            using (GraphicsPath path = GetRoundedRectangle(rect, radius))
            {
                if (_useGradient)
                {
                    using (LinearGradientBrush brush = new LinearGradientBrush(rect, _backgroundGradientStart, _backgroundGradientEnd, LinearGradientMode.Vertical))
                        e.Graphics.FillPath(brush, path);
                }
                else
                {
                    Color bg = (_textBox?.Focused ?? false) || !string.IsNullOrEmpty(_textBox?.Text)
                                ? ControlPaint.Light(_backgroundColor, 0.1f)
                                : _backgroundColor;
                    using (SolidBrush brush = new SolidBrush(bg))
                        e.Graphics.FillPath(brush, path);
                }
            }

            // Draw animated border
            Color border = InterpolateColor(_borderColor, _borderColorFocus, _animationProgress);
            if (_underlinedStyle)
            {
                using (Pen pen = new Pen(border, _borderSize))
                    e.Graphics.DrawLine(pen, 0, Height - _borderSize, Width, Height - _borderSize);
            }
            else
            {
                Rectangle borderRect = new Rectangle(1, 1, Width - 2, Height - 2);
                using (GraphicsPath borderPath = GetRoundedRectangle(borderRect, radius))
                using (Pen pen = new Pen(border, _borderSize))
                    e.Graphics.DrawPath(pen, borderPath);
            }

            // Draw placeholder text
            if (string.IsNullOrWhiteSpace(_textBox?.Text) && !string.IsNullOrEmpty(_placeholderText) && !(_textBox?.Focused ?? false))
            {
                Rectangle textRect = _textBox?.Bounds ?? rect;
                using (SolidBrush brush = new SolidBrush(_placeholderColor))
                    e.Graphics.DrawString(_placeholderText, Font, brush, textRect, new StringFormat { LineAlignment = StringAlignment.Center });
            }

            base.OnPaint(e);
        }

        /// <summary>
        /// Creates a rounded rectangle graphics path.
        /// </summary>
        private GraphicsPath GetRoundedRectangle(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;
            if (radius <= 0) { path.AddRectangle(rect); return path; }
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseAllFigures();
            return path;
        }

        /// <summary>
        /// Updates the region of the control to reflect rounded corners or underline style.
        /// </summary>
        private void UpdateRegion()
        {
            if (!_underlinedStyle)
            {
                using (GraphicsPath path = GetRoundedRectangle(new Rectangle(0, 0, Width, Height), _borderRadius))
                    this.Region = new Region(path);
            }
            else
            {
                this.Region = new Region(new Rectangle(0, 0, Width, Height));
            }
        }

        /// <inheritdoc/>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateTextBoxBounds();
            UpdateRegion();
        }

        /// <summary>
        /// Interpolates between two colors based on a given factor.
        /// </summary>
        private Color InterpolateColor(Color start, Color end, float t)
        {
            int r = (int)(start.R + (end.R - start.R) * t);
            int g = (int)(start.G + (end.G - start.G) * t);
            int b = (int)(start.B + (end.B - start.B) * t);
            return Color.FromArgb(r, g, b);
        }

        #region Utility Methods

        /// <summary>Sets input focus to the text box.</summary>
        public new void Focus() => _textBox?.Focus();

        /// <summary>Selects all text in the text box.</summary>
        public void SelectAll() => _textBox?.SelectAll();

        /// <summary>Clears the text box content.</summary>
        public void Clear() => _textBox?.Clear();

        /// <summary>Pastes the clipboard content into the text box.</summary>
        public void Paste() => _textBox?.Paste();

        /// <summary>Copies the selected text to the clipboard.</summary>
        public void Copy() => _textBox?.Copy();

        /// <summary>Cuts the selected text and copies it to the clipboard.</summary>
        public void Cut() => _textBox?.Cut();

        /// <summary>Undoes the last text operation.</summary>
        public void Undo() => _textBox?.Undo();

        #endregion

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _textBox?.Dispose();
                _animationTimer?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
