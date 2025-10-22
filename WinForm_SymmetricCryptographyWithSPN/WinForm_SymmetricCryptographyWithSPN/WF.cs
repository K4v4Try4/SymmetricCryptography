using Library;
using CustomControls;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace WinForm_SymmetricCryptographyWithSPN
{
    /// <summary>
    /// Main window of the WinForms application for symmetric cryptography
    /// based on SPN (Substitution-Permutation Network).
    /// 
    /// Provides:
    /// - A graphical interface to input plaintext, keys, and ciphertext.
    /// - Buttons for encryption, decryption, clearing fields, and inserting default values.
    /// - A custom title bar with close and minimize buttons.
    /// - Rounded corners and a custom visual style.
    /// </summary>
    public partial class WF : Form
    {
        private Cryptography cryptography;

        /// <summary>
        /// Initializes the main window.
        /// Sets up the UI style, adds the custom title bar, 
        /// and attaches events to the textboxes.
        /// </summary>
        public WF()
        {
            InitializeComponent();

            this.cryptography = new Cryptography();

            this.BackColor = Color.FromArgb(240, 248, 255);

            // Rounded corners
            this.FormBorderStyle = FormBorderStyle.None;
            this.Padding = new Padding(1);
            this.SetRoundedRegion(20);

            // Custom title bar
            this.AddTitleBar();

            // Attach TextChanged events
            cc_tb_pt.TextChanged += TextBoxes_TextChanged;
            cc_tb_k1.TextChanged += TextBoxes_TextChanged;
            cc_tb_k2.TextChanged += TextBoxes_TextChanged;
            cc_tb_c.TextChanged += TextBoxes_TextChanged;
            cc_tb_dt.TextChanged += TextBoxes_TextChanged;

            UpdateButtonStates();
        }

        /// <summary>
        /// Applies rounded corners to the form.
        /// </summary>
        /// <param name="radius">Radius of the rounded corners.</param>
        private void SetRoundedRegion(int radius)
        {
            var bounds = new Rectangle(0, 0, this.Width, this.Height);
            var path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
            path.AddArc(bounds.Right - radius, bounds.Y, radius, radius, 270, 90);
            path.AddArc(bounds.Right - radius, bounds.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            this.Region = new Region(path);
        }

        /// <summary>
        /// Adds a custom title bar with "Close" and "Minimize" buttons,
        /// and enables dragging the form with the mouse.
        /// </summary>
        private void AddTitleBar()
        {
            Panel titleBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 30,
                BackColor = Color.LightSteelBlue
            };
            this.Controls.Add(titleBar);

            // Pulsante Chiudi
            Button btnClose = new Button
            {
                Text = "X",
                Dock = DockStyle.Right,
                FlatStyle = FlatStyle.Flat,
                Width = 40,
                BackColor = Color.IndianRed,
                ForeColor = Color.White
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => this.Close();

            // Pulsante Minimizza
            Button btnMinimize = new Button
            {
                Text = "_",
                Dock = DockStyle.Right,
                FlatStyle = FlatStyle.Flat,
                Width = 40,
                BackColor = Color.LightGray,
                ForeColor = Color.Black
            };
            btnMinimize.FlatAppearance.BorderSize = 0;
            btnMinimize.Click += (s, e) => this.WindowState = FormWindowState.Minimized;

            titleBar.Controls.Add(btnMinimize);
            titleBar.Controls.Add(btnClose);

            // Drag della finestra
            titleBar.MouseDown += TitleBar_MouseDown;
        }

        #region WinAPI constants and methods for window dragging

        /// <summary>
        /// Windows message code for non-client left button down.
        /// </summary>
        public const int WM_NCLBUTTONDOWN = 0xA1;

        /// <summary>
        /// Hit test value for the window title bar.
        /// </summary>
        public const int HT_CAPTION = 0x2;

        /// <summary>
        /// Releases the current mouse capture (WinAPI).
        /// </summary>
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();

        /// <summary>
        /// Sends a Windows message to a window (WinAPI).
        /// </summary>
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        /// <summary>
        /// Handles window dragging when clicking on the custom title bar.
        /// </summary>
        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        #endregion


        /// <summary>
        /// Encrypt button click handler.
        /// Encrypts the plaintext using the two keys and updates the ciphertext textbox.
        /// </summary>
        private void cc_btn_encrypt_Click(object sender, EventArgs e)
        {
            try
            {
                cc_tb_c.Text = cryptography.Encrypt(
                    cc_tb_pt.Text,
                    cc_tb_k1.Text,
                    cc_tb_k2.Text
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
            UpdateButtonStates();
        }

        /// <summary>
        /// Decrypt button click handler.
        /// Decrypts the ciphertext using the two keys and updates the decrypted text textbox.
        /// </summary>
        private void cc_btn_decrypt_Click(object sender, EventArgs e)
        {
            try
            {
                cc_tb_dt.Text = cryptography.Decrypt(
                    cc_tb_c.Text,
                    cc_tb_k1.Text,
                    cc_tb_k2.Text
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
            UpdateButtonStates();
        }

        /// <summary>
        /// Clear button click handler.
        /// Clears all textboxes and updates the button states.
        /// </summary>
        private void cc_btn_clear_Click(object sender, EventArgs e)
        {
            cc_tb_pt.Clear();
            cc_tb_k1.Clear();
            cc_tb_k2.Clear();
            cc_tb_c.Clear();
            cc_tb_dt.Clear();
            UpdateButtonStates();
        }

        /// <summary>
        /// Default button click handler.
        /// Inserts example values into the plaintext and key textboxes.
        /// </summary>
        private void cc_btn_default_Click(object sender, EventArgs e)
        {
            cc_tb_pt.Text = "Cia0";
            cc_tb_k1.Text = "3L";
            cc_tb_k2.Text = "KrT";
            UpdateButtonStates();
        }

        /// <summary>
        /// Generic event handler for text changes in any textbox.
        /// Updates the state of all buttons.
        /// </summary>
        private void TextBoxes_TextChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }

        /// <summary>
        /// Checks whether all specified textboxes contain non-empty text.
        /// </summary>
        /// <param name="tbs">Array of <see cref="CCTextBox"/> to validate.</param>
        /// <returns>True if all textboxes contain text, otherwise false.</returns>
        private bool IsFilled(params CCTextBox[] tbs)
        {
            return tbs.All(tb => !string.IsNullOrWhiteSpace(tb.Text));
        }

        /// <summary>
        /// Updates the enabled/disabled state of all buttons:
        /// - "Encrypt" requires plaintext + key1 + key2
        /// - "Decrypt" requires ciphertext + key1 + key2
        /// - "Clear" is enabled if at least one textbox is not empty
        /// - "Default" is always enabled
        /// </summary>
        private void UpdateButtonStates()
        {
            cc_btn_encrypt.Enabled = IsFilled(cc_tb_pt, cc_tb_k1, cc_tb_k2);

            cc_btn_decrypt.Enabled = IsFilled(cc_tb_c, cc_tb_k1, cc_tb_k2);

            cc_btn_clear.Enabled =
                !string.IsNullOrWhiteSpace(cc_tb_pt.Text) ||
                !string.IsNullOrWhiteSpace(cc_tb_k1.Text) ||
                !string.IsNullOrWhiteSpace(cc_tb_k2.Text) ||
                !string.IsNullOrWhiteSpace(cc_tb_c.Text) ||
                !string.IsNullOrWhiteSpace(cc_tb_dt.Text);

            cc_btn_default.Enabled = true;
        }
    }
}
