using System;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace CustomControls
{
    /// <summary>
    /// A custom Windows Forms button with rounded corners,
    /// gradient backgrounds, hover/press effects, and customizable appearance.
    /// </summary>
    /// <remarks>
    /// Supports custom borders, gradient backgrounds, hover darkening,
    /// pressed overlays, and disabled states with transparency.
    /// </remarks>
    public class CCButton : Button
    {
        private Color _borderColor = Color.Blue;
        private int _borderSize = 2;
        private int _borderRadius = 15;
        private Color _backgroundGradientStart = Color.LightBlue;
        private Color _backgroundGradientEnd = Color.DarkBlue;
        private bool _useGradient = true;
        private Color _hoverDarken = Color.FromArgb(30, Color.Black);
        private Color _pressedOverlay = Color.FromArgb(80, Color.Black);

        private bool _isHovering = false;
        private bool _isPressed = false;

        private Size _originalSize;
        private bool _enabled = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="CCButton"/> class
        /// with default appearance and behavior settings.
        /// </summary>
        public CCButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.DoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            BackColor = Color.LightBlue;
            Size = new Size(120, 40);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            _originalSize = Size;
        }

        /// <summary>
        /// Gets or sets the border color of the button.
        /// </summary>
        [Category("Custom Appearance")]
        public Color BorderColor { get => _borderColor; set { _borderColor = value; Invalidate(); } }

        /// <summary>
        /// Gets or sets the border thickness of the button.
        /// </summary>
        [Category("Custom Appearance")]
        public int BorderSize { get => _borderSize; set { _borderSize = value; Invalidate(); } }

        /// <summary>
        /// Gets or sets the corner radius for rounded button edges.
        /// </summary>
        [Category("Custom Appearance")]
        public int BorderRadius { get => _borderRadius; set { _borderRadius = value; UpdateRegion(); Invalidate(); } }

        /// <summary>
        /// Gets or sets the starting color of the gradient background.
        /// </summary>
        [Category("Custom Appearance")]
        public Color BackgroundGradientStart { get => _backgroundGradientStart; set { _backgroundGradientStart = value; Invalidate(); } }

        /// <summary>
        /// Gets or sets the ending color of the gradient background.
        /// </summary>
        [Category("Custom Appearance")]
        public Color BackgroundGradientEnd { get => _backgroundGradientEnd; set { _backgroundGradientEnd = value; Invalidate(); } }

        /// <summary>
        /// Gets or sets a value indicating whether a gradient is used for the background.
        /// </summary>
        [Category("Custom Appearance")]
        public bool UseGradient { get => _useGradient; set { _useGradient = value; Invalidate(); } }

        /// <summary>
        /// Gets or sets the overlay color used when the button is hovered.
        /// </summary>
        [Category("Custom Appearance")]
        public Color HoverDarken { get => _hoverDarken; set { _hoverDarken = value; Invalidate(); } }

        /// <summary>
        /// Gets or sets the overlay color used when the button is pressed.
        /// </summary>
        [Category("Custom Appearance")]
        public Color PressedOverlay { get => _pressedOverlay; set { _pressedOverlay = value; Invalidate(); } }

        /// <summary>
        /// Gets or sets a value indicating whether the button is enabled.
        /// When disabled, the button appears grayed out.
        /// </summary>
        [Category("Behavior")]
        public new bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                base.Enabled = value;
                Invalidate(); // Redraw to apply disabled overlay
            }
        }
        /// <inheritdoc />
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle buttonRect = new Rectangle(0, 0, Width, Height);
            Rectangle borderRect = new Rectangle(1, 1, Width - 2, Height - 2);

            int radius = Math.Min(_borderRadius, Math.Min(Width, Height) / 2);

            using (GraphicsPath buttonPath = GetRoundedRectangle(buttonRect, radius))
            using (GraphicsPath borderPath = GetRoundedRectangle(borderRect, radius))
            {
                // Background
                if (_useGradient)
                {
                    using (LinearGradientBrush brush = new LinearGradientBrush(buttonRect, _backgroundGradientStart, _backgroundGradientEnd, LinearGradientMode.Vertical))
                        e.Graphics.FillPath(brush, buttonPath);
                }
                else
                {
                    using (SolidBrush brush = new SolidBrush(BackColor))
                        e.Graphics.FillPath(brush, buttonPath);
                }

                // Hover overlay
                if (_isHovering && _enabled)
                {
                    using (SolidBrush overlay = new SolidBrush(_hoverDarken))
                        e.Graphics.FillPath(overlay, buttonPath);
                }

                // Pressed overlay
                if (_isPressed && _enabled)
                {
                    using (SolidBrush overlay = new SolidBrush(_pressedOverlay))
                        e.Graphics.FillPath(overlay, buttonPath);
                }

                // Disabled overlay
                if (!_enabled)
                {
                    using (SolidBrush disabledOverlay = new SolidBrush(Color.FromArgb(120, Color.Gray)))
                        e.Graphics.FillPath(disabledOverlay, buttonPath);
                }

                // Border
                if (_borderSize > 0)
                {
                    using (Pen pen = new Pen(_borderColor, _borderSize))
                        e.Graphics.DrawPath(pen, borderPath);
                }

                // Text
                Color textColor = _enabled ? ForeColor : Color.FromArgb(160, ForeColor);
                TextRenderer.DrawText(e.Graphics, Text, Font, buttonRect, textColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

        /// <summary>
        /// Creates a rounded rectangle <see cref="GraphicsPath"/> for rendering.
        /// </summary>
        /// <param name="rect">The rectangle dimensions.</param>
        /// <param name="radius">The corner radius.</param>
        /// <returns>A <see cref="GraphicsPath"/> representing the rounded rectangle.</returns>
        private GraphicsPath GetRoundedRectangle(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            if (radius <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseAllFigures();

            return path;
        }

        /// <summary>
        /// Updates the button's region to match the rounded rectangle shape.
        /// </summary>
        private void UpdateRegion()
        {
            int radius = Math.Min(_borderRadius, Math.Min(Width, Height) / 2);
            using (GraphicsPath path = GetRoundedRectangle(new Rectangle(0, 0, Width, Height), radius))
                this.Region = new Region(path);
        }

        /// <inheritdoc />
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (!_isPressed)
            {
                _originalSize = Size;
            }

            UpdateRegion();
        }

        /// <inheritdoc />
        protected override void OnMouseEnter(EventArgs e)
        {
            if (!_enabled) return;
            _isHovering = true;
            Invalidate();
            base.OnMouseEnter(e);
        }

        /// <inheritdoc />
        protected override void OnMouseLeave(EventArgs e)
        {
            if (!_enabled) return;
            _isHovering = false;
            if (_isPressed)
                ResetButtonState();
            _isPressed = false;
            Invalidate();
            base.OnMouseLeave(e);
        }

        /// <inheritdoc />
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!_enabled) return;
            if (e.Button == MouseButtons.Left)
            {
                _isPressed = true;
                ShrinkButton();
                Invalidate();
            }
            base.OnMouseDown(e);
        }

        /// <inheritdoc />
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!_enabled) return;
            if (_isPressed)
            {
                _isPressed = false;
                ResetButtonState();
                Invalidate();
            }
            base.OnMouseUp(e);
        }

        /// <summary>
        /// Shrinks the button size slightly when pressed.
        /// </summary>
        private void ShrinkButton()
        {
            int shrinkAmount = 2;

            if (_originalSize.Width > shrinkAmount && _originalSize.Height > shrinkAmount)
            {
                Size = new Size(_originalSize.Width - shrinkAmount, _originalSize.Height - shrinkAmount);
            }
        }

        /// <summary>
        /// Resets the button size to its original state after being released.
        /// </summary>
        private void ResetButtonState()
        {
            Size = _originalSize;
        }
    }
}
