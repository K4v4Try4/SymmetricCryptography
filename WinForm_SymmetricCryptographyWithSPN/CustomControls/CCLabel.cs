using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CustomControls
{
    /// <summary>
    /// A custom label control with support for rounded corners,
    /// optional gradient backgrounds, borders, padding, shadow effects,
    /// and automatic resizing based on text content.
    /// </summary>
    public class CCLabel : Label
    {
        private Color _borderColor = Color.Gray;
        private int _borderSize = 1;
        private int _borderRadius = 10;

        private Color _backgroundGradientStart = Color.LightBlue;
        private Color _backgroundGradientEnd = Color.DodgerBlue;
        private bool _useGradient = false;
        private bool _drawBackground = true;

        private ContentAlignment _textAlignment = ContentAlignment.MiddleLeft;
        private Padding _textPadding = new Padding(5);
        private bool _autoSizeToText = false;

        private Color _shadowColor = Color.FromArgb(50, Color.Black);
        private bool _drawShadow = false;
        private Point _shadowOffset = new Point(2, 2);

        /// <summary>
        /// Initializes a new instance of the <see cref="CCLabel"/> class
        /// with default appearance and behavior settings.
        /// </summary>
        public CCLabel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.DoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            BackColor = Color.FromArgb(230, 240, 255); // background leggero
            Size = new Size(120, 30);
            ForeColor = Color.Black;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            AutoSize = false;
            _drawBackground = true;
            _useGradient = false;
        }

        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        [Category("Custom Appearance")] public Color BorderColor { get => _borderColor; set { _borderColor = value; Invalidate(); } }

        /// <summary>
        /// Gets or sets the thickness of the border.
        /// </summary>
        [Category("Custom Appearance")] public int BorderSize { get => _borderSize; set { _borderSize = Math.Max(0, value); Invalidate(); } }

        /// <summary>
        /// Gets or sets the corner radius for rounded label edges.
        /// </summary>
        [Category("Custom Appearance")] public int BorderRadius { get => _borderRadius; set { _borderRadius = Math.Max(0, value); UpdateRegion(); Invalidate(); } }

        /// <summary>
        /// Gets or sets the starting color of the gradient background.
        /// </summary>
        [Category("Custom Appearance")] public Color BackgroundGradientStart { get => _backgroundGradientStart; set { _backgroundGradientStart = value; Invalidate(); } }

        /// <summary>
        /// Gets or sets the ending color of the gradient background.
        /// </summary>
        [Category("Custom Appearance")] public Color BackgroundGradientEnd { get => _backgroundGradientEnd; set { _backgroundGradientEnd = value; Invalidate(); } }

        /// <summary>
        /// Gets or sets a value indicating whether a gradient is used for the background.
        /// </summary>
        [Category("Custom Appearance")] public bool UseGradient { get => _useGradient; set { _useGradient = value; Invalidate(); } }

        /// <summary>
        /// Gets or sets a value indicating whether the background should be drawn.
        /// </summary>
        [Category("Custom Appearance")] public bool DrawBackground { get => _drawBackground; set { _drawBackground = value; Invalidate(); } }

        /// <summary>
        /// Gets or sets the alignment of the text inside the label.
        /// </summary>
        [Category("Custom Layout")] public ContentAlignment TextAlignment { get => _textAlignment; set { _textAlignment = value; Invalidate(); } }

        /// <summary>
        /// Gets or sets the padding applied to the text.
        /// </summary>
        [Category("Custom Layout")] public Padding TextPadding { get => _textPadding; set { _textPadding = value; Invalidate(); } }

        /// <summary>
        /// Gets or sets a value indicating whether the label should automatically
        /// resize itself to fit its text content.
        /// </summary>
        [Category("Custom Layout")] public bool AutoSizeToText { get => _autoSizeToText; set { _autoSizeToText = value; if (value) UpdateAutoSize(); } }

        /// <summary>
        /// Gets or sets the color of the text shadow.
        /// </summary>
        [Category("Custom Effects")] public Color ShadowColor { get => _shadowColor; set { _shadowColor = value; Invalidate(); } }

        /// <summary>
        /// Gets or sets a value indicating whether a shadow is drawn behind the text.
        /// </summary>
        [Category("Custom Effects")] public bool DrawShadow { get => _drawShadow; set { _drawShadow = value; Invalidate(); } }

        /// <summary>
        /// Gets or sets the offset of the text shadow relative to the text.
        /// </summary>
        [Category("Custom Effects")] public Point ShadowOffset { get => _shadowOffset; set { _shadowOffset = value; Invalidate(); } }

        /// <inheritdoc />
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle labelRect = new Rectangle(0, 0, Width, Height);
            Rectangle borderRect = new Rectangle(_borderSize / 2, _borderSize / 2, Width - _borderSize, Height - _borderSize);
            int radius = Math.Min(_borderRadius, Math.Min(Width, Height) / 2);

            using (GraphicsPath labelPath = GetRoundedRectangle(labelRect, radius))
            using (GraphicsPath borderPath = GetRoundedRectangle(borderRect, radius))
            {
                // Background
                if (_drawBackground)
                {
                    if (_useGradient)
                    {
                        using (LinearGradientBrush brush = new LinearGradientBrush(labelRect, _backgroundGradientStart, _backgroundGradientEnd, LinearGradientMode.Vertical))
                            e.Graphics.FillPath(brush, labelPath);
                    }
                    else
                    {
                        using (SolidBrush brush = new SolidBrush(BackColor))
                            e.Graphics.FillPath(brush, labelPath);
                    }
                }

                // Border
                if (_borderSize > 0)
                {
                    using (Pen pen = new Pen(_borderColor, _borderSize))
                        e.Graphics.DrawPath(pen, borderPath);
                }

                // Text area
                Rectangle textRect = new Rectangle(
                    _textPadding.Left + _borderSize,
                    _textPadding.Top + _borderSize,
                    Width - _textPadding.Horizontal - (_borderSize * 2),
                    Height - _textPadding.Vertical - (_borderSize * 2)
                );

                // Shadow text
                if (_drawShadow && !string.IsNullOrEmpty(Text))
                {
                    Rectangle shadowRect = new Rectangle(textRect.X + _shadowOffset.X, textRect.Y + _shadowOffset.Y, textRect.Width, textRect.Height);
                    TextRenderer.DrawText(e.Graphics, Text, Font, shadowRect, _shadowColor, GetTextFormatFlags());
                }

                // Main text
                if (!string.IsNullOrEmpty(Text))
                {
                    TextRenderer.DrawText(e.Graphics, Text, Font, textRect, ForeColor, GetTextFormatFlags());
                }
            }
        }

        /// <summary>
        /// Returns text rendering flags based on the current alignment.
        /// </summary>
        private TextFormatFlags GetTextFormatFlags()
        {
            TextFormatFlags flags = TextFormatFlags.WordBreak;

            switch (_textAlignment)
            {
                case ContentAlignment.TopLeft: flags |= TextFormatFlags.Top | TextFormatFlags.Left; break;
                case ContentAlignment.TopCenter: flags |= TextFormatFlags.Top | TextFormatFlags.HorizontalCenter; break;
                case ContentAlignment.TopRight: flags |= TextFormatFlags.Top | TextFormatFlags.Right; break;
                case ContentAlignment.MiddleLeft: flags |= TextFormatFlags.VerticalCenter | TextFormatFlags.Left; break;
                case ContentAlignment.MiddleCenter: flags |= TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter; break;
                case ContentAlignment.MiddleRight: flags |= TextFormatFlags.VerticalCenter | TextFormatFlags.Right; break;
                case ContentAlignment.BottomLeft: flags |= TextFormatFlags.Bottom | TextFormatFlags.Left; break;
                case ContentAlignment.BottomCenter: flags |= TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter; break;
                case ContentAlignment.BottomRight: flags |= TextFormatFlags.Bottom | TextFormatFlags.Right; break;
            }

            return flags;
        }

        /// <summary>
        /// Creates a rounded rectangle <see cref="GraphicsPath"/> for rendering.
        /// </summary>
        private GraphicsPath GetRoundedRectangle(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            if (radius <= 0) { path.AddRectangle(rect); return path; }

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseAllFigures();

            return path;
        }

        /// <summary>
        /// Updates the control region to match the rounded border radius.
        /// </summary>
        private void UpdateRegion()
        {
            if (_borderRadius > 0)
            {
                int radius = Math.Min(_borderRadius, Math.Min(Width, Height) / 2);
                using (GraphicsPath path = GetRoundedRectangle(new Rectangle(0, 0, Width, Height), radius))
                    this.Region = new Region(path);
            }
            else
            {
                this.Region = null;
            }
        }

        /// <summary>
        /// Updates the label size to fit the text content if AutoSizeToText is enabled.
        /// </summary>
        private void UpdateAutoSize()
        {
            if (_autoSizeToText && !string.IsNullOrEmpty(Text))
            {
                using (Graphics g = CreateGraphics())
                {
                    Size textSize = TextRenderer.MeasureText(g, Text, Font);

                    Size newSize = new Size(
                        textSize.Width + _textPadding.Horizontal + (_borderSize * 2) + 4,
                        textSize.Height + _textPadding.Vertical + (_borderSize * 2) + 4
                    );

                    if (Size != newSize)
                    {
                        Size = newSize;
                        UpdateRegion();
                    }
                }
            }
        }

        /// <inheritdoc />
        protected override void OnResize(EventArgs e) { base.OnResize(e); UpdateRegion(); }

        /// <inheritdoc />
        protected override void OnTextChanged(EventArgs e) { if (_autoSizeToText) UpdateAutoSize(); Invalidate(); base.OnTextChanged(e); }

        /// <inheritdoc />
        protected override void OnFontChanged(EventArgs e) { if (_autoSizeToText) UpdateAutoSize(); Invalidate(); base.OnFontChanged(e); }

        // Hide unused inherited properties
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool AutoSize { get => base.AutoSize; set => base.AutoSize = false; }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override ContentAlignment TextAlign { get => base.TextAlign; set => base.TextAlign = value; }
    }
}
