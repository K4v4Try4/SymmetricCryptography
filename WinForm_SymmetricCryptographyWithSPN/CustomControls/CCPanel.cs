using System;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace CustomControls
{
    /// <summary>
    /// A custom panel control that supports rounded corners, gradients, shadows, and customizable borders.
    /// </summary>
    public class CCPanel : Panel
    {
        private Color _borderColor = Color.WhiteSmoke;
        private int _borderSize = 2;
        private int _borderRadius = 15;

        private Color _backgroundGradientStart = Color.LightSkyBlue;
        private Color _backgroundGradientEnd = Color.DodgerBlue;
        private bool _useGradient = true;

        private bool _useShadow = true;
        private Color _shadowColor = Color.FromArgb(50, Color.Black);
        private int _shadowOffset = 4;

        private LinearGradientMode _gradientDirection = LinearGradientMode.Vertical;

        /// <summary>
        /// Initializes a new instance of the <see cref="CCPanel"/> class with default appearance settings.
        /// </summary>
        public CCPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.DoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            BackColor = Color.LightSkyBlue;
            Size = new Size(200, 150);
        }
        /// <summary>Gets or sets the border color of the panel.</summary>
        [Category("Custom Appearance")]
        public Color BorderColor { get => _borderColor; set { _borderColor = value; Invalidate(); } }

        /// <summary>Gets or sets the thickness of the panel border.</summary>
        [Category("Custom Appearance")]
        public int BorderSize { get => _borderSize; set { _borderSize = Math.Max(0, value); Invalidate(); } }

        /// <summary>Gets or sets the radius of the panel’s rounded corners.</summary>
        [Category("Custom Appearance")]
        public int BorderRadius { get => _borderRadius; set { _borderRadius = Math.Max(0, value); UpdateRegion(); Invalidate(); } }

        /// <summary>Gets or sets the starting color of the gradient background.</summary>
        [Category("Custom Appearance")]
        public Color BackgroundGradientStart { get => _backgroundGradientStart; set { _backgroundGradientStart = value; Invalidate(); } }

        /// <summary>Gets or sets the ending color of the gradient background.</summary>
        [Category("Custom Appearance")]
        public Color BackgroundGradientEnd { get => _backgroundGradientEnd; set { _backgroundGradientEnd = value; Invalidate(); } }

        /// <summary>Determines whether the panel background uses a gradient or solid color.</summary>
        [Category("Custom Appearance")]
        public bool UseGradient { get => _useGradient; set { _useGradient = value; Invalidate(); } }

        /// <summary>Gets or sets the direction of the gradient fill.</summary>
        [Category("Custom Appearance")]
        public LinearGradientMode GradientDirection { get => _gradientDirection; set { _gradientDirection = value; Invalidate(); } }
        
        /// <summary>Determines whether a shadow effect is drawn for the panel.</summary>
        [Category("Custom Appearance")]
        public bool UseShadow { get => _useShadow; set { _useShadow = value; Invalidate(); } }

        /// <summary>Gets or sets the color of the shadow.</summary>
        [Category("Custom Appearance")]
        public Color ShadowColor { get => _shadowColor; set { _shadowColor = value; Invalidate(); } }

        /// <summary>Gets or sets the offset distance of the shadow.</summary>
        [Category("Custom Appearance")]
        public int ShadowOffset { get => _shadowOffset; set { _shadowOffset = Math.Max(0, value); Invalidate(); } }

        /// <summary>
        /// Paints the panel, including background, border, and optional shadow.
        /// </summary>
        /// <param name="e">Paint event arguments.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle panelRect = new Rectangle(0, 0, Width, Height);
            Rectangle shadowRect = new Rectangle(_shadowOffset, _shadowOffset, Width - _shadowOffset, Height - _shadowOffset);
            Rectangle borderRect = new Rectangle(0, 0, Width - 1, Height - 1);
            int radius = Math.Min(_borderRadius, Math.Min(Width, Height) / 2);

            // Draw shadow
            if (_useShadow && _shadowOffset > 0)
            {
                using (GraphicsPath shadowPath = GetRoundedRectangle(shadowRect, radius))
                using (SolidBrush shadowBrush = new SolidBrush(_shadowColor))
                {
                    e.Graphics.FillPath(shadowBrush, shadowPath);
                }
            }

            using (GraphicsPath panelPath = GetRoundedRectangle(panelRect, radius))
            using (GraphicsPath borderPath = GetRoundedRectangle(borderRect, radius))
            {
                // Draw background
                if (_useGradient)
                {
                    using (LinearGradientBrush gradientBrush = new LinearGradientBrush(
                        panelRect, _backgroundGradientStart, _backgroundGradientEnd, _gradientDirection))
                    {
                        e.Graphics.FillPath(gradientBrush, panelPath);
                    }
                }
                else
                {
                    using (SolidBrush backgroundBrush = new SolidBrush(BackColor))
                    {
                        e.Graphics.FillPath(backgroundBrush, panelPath);
                    }
                }

                // Draw border
                if (_borderSize > 0)
                {
                    using (Pen borderPen = new Pen(_borderColor, _borderSize))
                    {
                        e.Graphics.DrawPath(borderPen, borderPath);
                    }
                }
            }
        }

        /// <summary>
        /// Creates a rounded rectangle path for drawing.
        /// </summary>
        /// <param name="rect">The rectangle to round.</param>
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

            diameter = Math.Min(diameter, Math.Min(rect.Width, rect.Height));
            radius = diameter / 2;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseAllFigures();

            return path;
        }

        /// <summary>
        /// Updates the panel’s region to match the rounded corner radius.
        /// </summary>
        private void UpdateRegion()
        {
            int radius = Math.Min(_borderRadius, Math.Min(Width, Height) / 2);
            using (GraphicsPath path = GetRoundedRectangle(new Rectangle(0, 0, Width, Height), radius))
            {
                this.Region = new Region(path);
            }
        }

        /// <inheritdoc/>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateRegion();
        }

        /// <inheritdoc/>
        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            Invalidate();
        }

        /// <summary>
        /// Applies a clean "card" style with flat white background and subtle shadow.
        /// </summary>
        public void SetCardStyle()
        {
            UseGradient = false;
            BackColor = Color.White;
            BorderColor = Color.FromArgb(220, 220, 220);
            BorderSize = 1;
            BorderRadius = 8;
            UseShadow = true;
            ShadowColor = Color.FromArgb(30, Color.Black);
            ShadowOffset = 2;
        }

        /// <summary>
        /// Applies a gradient style with the given start and end colors.
        /// </summary>
        /// <param name="startColor">The gradient start color.</param>
        /// <param name="endColor">The gradient end color.</param>
        public void SetGradientStyle(Color startColor, Color endColor)
        {
            UseGradient = true;
            BackgroundGradientStart = startColor;
            BackgroundGradientEnd = endColor;
            BorderSize = 2;
            BorderColor = Color.WhiteSmoke;
            BorderRadius = 15;
            UseShadow = true;
            ShadowColor = Color.FromArgb(50, Color.Black);
            ShadowOffset = 4;
        }

        /// <summary>
        /// Applies a flat style with solid background and border colors, without shadow.
        /// </summary>
        /// <param name="backgroundColor">The background color.</param>
        /// <param name="borderColor">The border color.</param>
        public void SetFlatStyle(Color backgroundColor, Color borderColor)
        {
            UseGradient = false;
            BackColor = backgroundColor;
            BorderColor = borderColor;
            BorderSize = 2;
            BorderRadius = 0;
            UseShadow = false;
        }
    }
}
