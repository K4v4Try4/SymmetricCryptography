namespace WinForm_SymmetricCryptographyWithSPN
{
    partial class WF
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ccPanel1 = new CustomControls.CCPanel();
            cc_tb_dt = new CustomControls.CCTextBox();
            cc_tb_c = new CustomControls.CCTextBox();
            cc_tb_k2 = new CustomControls.CCTextBox();
            cc_tb_k1 = new CustomControls.CCTextBox();
            cc_tb_pt = new CustomControls.CCTextBox();
            ccLabel5 = new CustomControls.CCLabel();
            ccLabel4 = new CustomControls.CCLabel();
            ccLabel3 = new CustomControls.CCLabel();
            ccLabel2 = new CustomControls.CCLabel();
            ccLabel1 = new CustomControls.CCLabel();
            cc_btn_encrypt = new CustomControls.CCButton();
            ccPanel2 = new CustomControls.CCPanel();
            cc_btn_default = new CustomControls.CCButton();
            cc_btn_clear = new CustomControls.CCButton();
            cc_btn_decrypt = new CustomControls.CCButton();
            ccPanel1.SuspendLayout();
            ccPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // ccPanel1
            // 
            ccPanel1.BackColor = Color.LightSkyBlue;
            ccPanel1.BackgroundGradientEnd = Color.DodgerBlue;
            ccPanel1.BackgroundGradientStart = Color.LightSkyBlue;
            ccPanel1.BorderColor = Color.WhiteSmoke;
            ccPanel1.BorderRadius = 15;
            ccPanel1.BorderSize = 2;
            ccPanel1.Controls.Add(cc_tb_dt);
            ccPanel1.Controls.Add(cc_tb_c);
            ccPanel1.Controls.Add(cc_tb_k2);
            ccPanel1.Controls.Add(cc_tb_k1);
            ccPanel1.Controls.Add(cc_tb_pt);
            ccPanel1.Controls.Add(ccLabel5);
            ccPanel1.Controls.Add(ccLabel4);
            ccPanel1.Controls.Add(ccLabel3);
            ccPanel1.Controls.Add(ccLabel2);
            ccPanel1.Controls.Add(ccLabel1);
            ccPanel1.GradientDirection = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            ccPanel1.Location = new Point(12, 43);
            ccPanel1.Name = "ccPanel1";
            ccPanel1.ShadowColor = Color.FromArgb(50, 0, 0, 0);
            ccPanel1.ShadowOffset = 4;
            ccPanel1.Size = new Size(377, 261);
            ccPanel1.TabIndex = 0;
            ccPanel1.UseGradient = true;
            ccPanel1.UseShadow = true;
            // 
            // cc_tb_dt
            // 
            cc_tb_dt.BackColor = Color.Transparent;
            cc_tb_dt.BackgroundColor = Color.WhiteSmoke;
            cc_tb_dt.BackgroundGradientEnd = Color.LightGray;
            cc_tb_dt.BackgroundGradientStart = Color.White;
            cc_tb_dt.BorderColor = Color.Silver;
            cc_tb_dt.BorderColorFocus = Color.RoyalBlue;
            cc_tb_dt.BorderRadius = 15;
            cc_tb_dt.BorderSize = 2;
            cc_tb_dt.Font = new Font("Segoe UI", 10F);
            cc_tb_dt.Location = new Point(141, 213);
            cc_tb_dt.Multiline = false;
            cc_tb_dt.Name = "cc_tb_dt";
            cc_tb_dt.PasswordChar = false;
            cc_tb_dt.PasswordCharacter = '●';
            cc_tb_dt.PlaceholderColor = Color.Gray;
            cc_tb_dt.PlaceholderText = "";
            cc_tb_dt.Size = new Size(220, 30);
            cc_tb_dt.TabIndex = 9;
            cc_tb_dt.UnderlinedStyle = false;
            cc_tb_dt.UseGradient = false;
            // 
            // cc_tb_c
            // 
            cc_tb_c.BackColor = Color.Transparent;
            cc_tb_c.BackgroundColor = Color.WhiteSmoke;
            cc_tb_c.BackgroundGradientEnd = Color.LightGray;
            cc_tb_c.BackgroundGradientStart = Color.White;
            cc_tb_c.BorderColor = Color.Silver;
            cc_tb_c.BorderColorFocus = Color.RoyalBlue;
            cc_tb_c.BorderRadius = 15;
            cc_tb_c.BorderSize = 2;
            cc_tb_c.Font = new Font("Segoe UI", 10F);
            cc_tb_c.Location = new Point(141, 164);
            cc_tb_c.Multiline = false;
            cc_tb_c.Name = "cc_tb_c";
            cc_tb_c.PasswordChar = false;
            cc_tb_c.PasswordCharacter = '●';
            cc_tb_c.PlaceholderColor = Color.Gray;
            cc_tb_c.PlaceholderText = "";
            cc_tb_c.Size = new Size(220, 30);
            cc_tb_c.TabIndex = 8;
            cc_tb_c.UnderlinedStyle = false;
            cc_tb_c.UseGradient = false;
            // 
            // cc_tb_k2
            // 
            cc_tb_k2.BackColor = Color.Transparent;
            cc_tb_k2.BackgroundColor = Color.WhiteSmoke;
            cc_tb_k2.BackgroundGradientEnd = Color.LightGray;
            cc_tb_k2.BackgroundGradientStart = Color.White;
            cc_tb_k2.BorderColor = Color.Silver;
            cc_tb_k2.BorderColorFocus = Color.RoyalBlue;
            cc_tb_k2.BorderRadius = 15;
            cc_tb_k2.BorderSize = 2;
            cc_tb_k2.Font = new Font("Segoe UI", 10F);
            cc_tb_k2.Location = new Point(141, 114);
            cc_tb_k2.Multiline = false;
            cc_tb_k2.Name = "cc_tb_k2";
            cc_tb_k2.PasswordChar = false;
            cc_tb_k2.PasswordCharacter = '●';
            cc_tb_k2.PlaceholderColor = Color.Gray;
            cc_tb_k2.PlaceholderText = "";
            cc_tb_k2.Size = new Size(220, 30);
            cc_tb_k2.TabIndex = 7;
            cc_tb_k2.UnderlinedStyle = false;
            cc_tb_k2.UseGradient = false;
            // 
            // cc_tb_k1
            // 
            cc_tb_k1.BackColor = Color.Transparent;
            cc_tb_k1.BackgroundColor = Color.WhiteSmoke;
            cc_tb_k1.BackgroundGradientEnd = Color.LightGray;
            cc_tb_k1.BackgroundGradientStart = Color.White;
            cc_tb_k1.BorderColor = Color.Silver;
            cc_tb_k1.BorderColorFocus = Color.RoyalBlue;
            cc_tb_k1.BorderRadius = 15;
            cc_tb_k1.BorderSize = 2;
            cc_tb_k1.Font = new Font("Segoe UI", 10F);
            cc_tb_k1.Location = new Point(141, 65);
            cc_tb_k1.Multiline = false;
            cc_tb_k1.Name = "cc_tb_k1";
            cc_tb_k1.PasswordChar = false;
            cc_tb_k1.PasswordCharacter = '●';
            cc_tb_k1.PlaceholderColor = Color.Gray;
            cc_tb_k1.PlaceholderText = "";
            cc_tb_k1.Size = new Size(220, 30);
            cc_tb_k1.TabIndex = 6;
            cc_tb_k1.UnderlinedStyle = false;
            cc_tb_k1.UseGradient = false;
            // 
            // cc_tb_pt
            // 
            cc_tb_pt.BackColor = Color.Transparent;
            cc_tb_pt.BackgroundColor = Color.WhiteSmoke;
            cc_tb_pt.BackgroundGradientEnd = Color.LightGray;
            cc_tb_pt.BackgroundGradientStart = Color.White;
            cc_tb_pt.BorderColor = Color.Silver;
            cc_tb_pt.BorderColorFocus = Color.RoyalBlue;
            cc_tb_pt.BorderRadius = 15;
            cc_tb_pt.BorderSize = 2;
            cc_tb_pt.Font = new Font("Segoe UI", 10F);
            cc_tb_pt.Location = new Point(141, 14);
            cc_tb_pt.Multiline = false;
            cc_tb_pt.Name = "cc_tb_pt";
            cc_tb_pt.PasswordChar = false;
            cc_tb_pt.PasswordCharacter = '●';
            cc_tb_pt.PlaceholderColor = Color.Gray;
            cc_tb_pt.PlaceholderText = "";
            cc_tb_pt.Size = new Size(220, 30);
            cc_tb_pt.TabIndex = 5;
            cc_tb_pt.UnderlinedStyle = false;
            cc_tb_pt.UseGradient = false;
            // 
            // ccLabel5
            // 
            ccLabel5.AutoSizeToText = false;
            ccLabel5.BackColor = Color.FromArgb(230, 240, 255);
            ccLabel5.BackgroundGradientEnd = Color.DodgerBlue;
            ccLabel5.BackgroundGradientStart = Color.LightBlue;
            ccLabel5.BorderColor = Color.Gray;
            ccLabel5.BorderRadius = 10;
            ccLabel5.BorderSize = 1;
            ccLabel5.DrawBackground = true;
            ccLabel5.DrawShadow = false;
            ccLabel5.Font = new Font("Segoe UI", 9F);
            ccLabel5.ForeColor = Color.Black;
            ccLabel5.Location = new Point(15, 213);
            ccLabel5.Name = "ccLabel5";
            ccLabel5.ShadowColor = Color.FromArgb(50, 0, 0, 0);
            ccLabel5.ShadowOffset = new Point(2, 2);
            ccLabel5.Size = new Size(120, 30);
            ccLabel5.TabIndex = 4;
            ccLabel5.Text = "Deciphered Text";
            ccLabel5.TextAlignment = ContentAlignment.MiddleLeft;
            ccLabel5.TextPadding = new Padding(5);
            ccLabel5.UseGradient = false;
            // 
            // ccLabel4
            // 
            ccLabel4.AutoSizeToText = false;
            ccLabel4.BackColor = Color.FromArgb(230, 240, 255);
            ccLabel4.BackgroundGradientEnd = Color.DodgerBlue;
            ccLabel4.BackgroundGradientStart = Color.LightBlue;
            ccLabel4.BorderColor = Color.Gray;
            ccLabel4.BorderRadius = 10;
            ccLabel4.BorderSize = 1;
            ccLabel4.DrawBackground = true;
            ccLabel4.DrawShadow = false;
            ccLabel4.Font = new Font("Segoe UI", 9F);
            ccLabel4.ForeColor = Color.Black;
            ccLabel4.Location = new Point(15, 164);
            ccLabel4.Name = "ccLabel4";
            ccLabel4.ShadowColor = Color.FromArgb(50, 0, 0, 0);
            ccLabel4.ShadowOffset = new Point(2, 2);
            ccLabel4.Size = new Size(120, 30);
            ccLabel4.TabIndex = 3;
            ccLabel4.Text = "Ciphertext";
            ccLabel4.TextAlignment = ContentAlignment.MiddleLeft;
            ccLabel4.TextPadding = new Padding(5);
            ccLabel4.UseGradient = false;
            // 
            // ccLabel3
            // 
            ccLabel3.AutoSizeToText = false;
            ccLabel3.BackColor = Color.FromArgb(230, 240, 255);
            ccLabel3.BackgroundGradientEnd = Color.DodgerBlue;
            ccLabel3.BackgroundGradientStart = Color.LightBlue;
            ccLabel3.BorderColor = Color.Gray;
            ccLabel3.BorderRadius = 10;
            ccLabel3.BorderSize = 1;
            ccLabel3.DrawBackground = true;
            ccLabel3.DrawShadow = false;
            ccLabel3.Font = new Font("Segoe UI", 9F);
            ccLabel3.ForeColor = Color.Black;
            ccLabel3.Location = new Point(15, 114);
            ccLabel3.Name = "ccLabel3";
            ccLabel3.ShadowColor = Color.FromArgb(50, 0, 0, 0);
            ccLabel3.ShadowOffset = new Point(2, 2);
            ccLabel3.Size = new Size(120, 30);
            ccLabel3.TabIndex = 2;
            ccLabel3.Text = "Key 2";
            ccLabel3.TextAlignment = ContentAlignment.MiddleLeft;
            ccLabel3.TextPadding = new Padding(5);
            ccLabel3.UseGradient = false;
            // 
            // ccLabel2
            // 
            ccLabel2.AutoSizeToText = false;
            ccLabel2.BackColor = Color.FromArgb(230, 240, 255);
            ccLabel2.BackgroundGradientEnd = Color.DodgerBlue;
            ccLabel2.BackgroundGradientStart = Color.LightBlue;
            ccLabel2.BorderColor = Color.Gray;
            ccLabel2.BorderRadius = 10;
            ccLabel2.BorderSize = 1;
            ccLabel2.DrawBackground = true;
            ccLabel2.DrawShadow = false;
            ccLabel2.Font = new Font("Segoe UI", 9F);
            ccLabel2.ForeColor = Color.Black;
            ccLabel2.Location = new Point(15, 65);
            ccLabel2.Name = "ccLabel2";
            ccLabel2.ShadowColor = Color.FromArgb(50, 0, 0, 0);
            ccLabel2.ShadowOffset = new Point(2, 2);
            ccLabel2.Size = new Size(120, 30);
            ccLabel2.TabIndex = 1;
            ccLabel2.Text = "Key 1";
            ccLabel2.TextAlignment = ContentAlignment.MiddleLeft;
            ccLabel2.TextPadding = new Padding(5);
            ccLabel2.UseGradient = false;
            // 
            // ccLabel1
            // 
            ccLabel1.AutoSizeToText = false;
            ccLabel1.BackColor = Color.FromArgb(230, 240, 255);
            ccLabel1.BackgroundGradientEnd = Color.DodgerBlue;
            ccLabel1.BackgroundGradientStart = Color.LightBlue;
            ccLabel1.BorderColor = Color.Gray;
            ccLabel1.BorderRadius = 10;
            ccLabel1.BorderSize = 1;
            ccLabel1.DrawBackground = true;
            ccLabel1.DrawShadow = false;
            ccLabel1.Font = new Font("Segoe UI", 9F);
            ccLabel1.ForeColor = Color.Black;
            ccLabel1.Location = new Point(15, 14);
            ccLabel1.Name = "ccLabel1";
            ccLabel1.ShadowColor = Color.FromArgb(50, 0, 0, 0);
            ccLabel1.ShadowOffset = new Point(2, 2);
            ccLabel1.Size = new Size(120, 30);
            ccLabel1.TabIndex = 0;
            ccLabel1.Text = "Plain Text";
            ccLabel1.TextAlignment = ContentAlignment.MiddleLeft;
            ccLabel1.TextPadding = new Padding(5);
            ccLabel1.UseGradient = false;
            // 
            // cc_btn_encrypt
            // 
            cc_btn_encrypt.BackColor = Color.LightBlue;
            cc_btn_encrypt.BackgroundGradientEnd = Color.DarkBlue;
            cc_btn_encrypt.BackgroundGradientStart = Color.LightBlue;
            cc_btn_encrypt.BorderColor = Color.Blue;
            cc_btn_encrypt.BorderRadius = 15;
            cc_btn_encrypt.BorderSize = 2;
            cc_btn_encrypt.FlatAppearance.BorderSize = 0;
            cc_btn_encrypt.FlatStyle = FlatStyle.Flat;
            cc_btn_encrypt.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            cc_btn_encrypt.ForeColor = Color.White;
            cc_btn_encrypt.HoverDarken = Color.FromArgb(30, 0, 0, 0);
            cc_btn_encrypt.Location = new Point(15, 15);
            cc_btn_encrypt.Name = "cc_btn_encrypt";
            cc_btn_encrypt.PressedOverlay = Color.FromArgb(80, 0, 0, 0);
            cc_btn_encrypt.Size = new Size(120, 40);
            cc_btn_encrypt.TabIndex = 7;
            cc_btn_encrypt.Text = "Encrypt";
            cc_btn_encrypt.UseGradient = true;
            cc_btn_encrypt.UseVisualStyleBackColor = false;
            cc_btn_encrypt.Click += cc_btn_encrypt_Click;
            // 
            // ccPanel2
            // 
            ccPanel2.BackColor = Color.LightSkyBlue;
            ccPanel2.BackgroundGradientEnd = Color.DodgerBlue;
            ccPanel2.BackgroundGradientStart = Color.LightSkyBlue;
            ccPanel2.BorderColor = Color.WhiteSmoke;
            ccPanel2.BorderRadius = 15;
            ccPanel2.BorderSize = 2;
            ccPanel2.Controls.Add(cc_btn_default);
            ccPanel2.Controls.Add(cc_btn_clear);
            ccPanel2.Controls.Add(cc_btn_decrypt);
            ccPanel2.Controls.Add(cc_btn_encrypt);
            ccPanel2.GradientDirection = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            ccPanel2.Location = new Point(55, 310);
            ccPanel2.Name = "ccPanel2";
            ccPanel2.ShadowColor = Color.FromArgb(50, 0, 0, 0);
            ccPanel2.ShadowOffset = 4;
            ccPanel2.Size = new Size(293, 132);
            ccPanel2.TabIndex = 8;
            ccPanel2.UseGradient = true;
            ccPanel2.UseShadow = true;
            // 
            // cc_btn_default
            // 
            cc_btn_default.BackColor = Color.LightBlue;
            cc_btn_default.BackgroundGradientEnd = Color.DarkBlue;
            cc_btn_default.BackgroundGradientStart = Color.LightBlue;
            cc_btn_default.BorderColor = Color.Blue;
            cc_btn_default.BorderRadius = 15;
            cc_btn_default.BorderSize = 2;
            cc_btn_default.FlatAppearance.BorderSize = 0;
            cc_btn_default.FlatStyle = FlatStyle.Flat;
            cc_btn_default.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            cc_btn_default.ForeColor = Color.White;
            cc_btn_default.HoverDarken = Color.FromArgb(30, 0, 0, 0);
            cc_btn_default.Location = new Point(155, 74);
            cc_btn_default.Name = "cc_btn_default";
            cc_btn_default.PressedOverlay = Color.FromArgb(80, 0, 0, 0);
            cc_btn_default.Size = new Size(120, 40);
            cc_btn_default.TabIndex = 10;
            cc_btn_default.Text = "Default";
            cc_btn_default.UseGradient = true;
            cc_btn_default.UseVisualStyleBackColor = false;
            cc_btn_default.Click += cc_btn_default_Click;
            // 
            // cc_btn_clear
            // 
            cc_btn_clear.BackColor = Color.LightBlue;
            cc_btn_clear.BackgroundGradientEnd = Color.DarkBlue;
            cc_btn_clear.BackgroundGradientStart = Color.LightBlue;
            cc_btn_clear.BorderColor = Color.Blue;
            cc_btn_clear.BorderRadius = 15;
            cc_btn_clear.BorderSize = 2;
            cc_btn_clear.FlatAppearance.BorderSize = 0;
            cc_btn_clear.FlatStyle = FlatStyle.Flat;
            cc_btn_clear.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            cc_btn_clear.ForeColor = Color.White;
            cc_btn_clear.HoverDarken = Color.FromArgb(30, 0, 0, 0);
            cc_btn_clear.Location = new Point(15, 74);
            cc_btn_clear.Name = "cc_btn_clear";
            cc_btn_clear.PressedOverlay = Color.FromArgb(80, 0, 0, 0);
            cc_btn_clear.Size = new Size(120, 40);
            cc_btn_clear.TabIndex = 9;
            cc_btn_clear.Text = "Clear";
            cc_btn_clear.UseGradient = true;
            cc_btn_clear.UseVisualStyleBackColor = false;
            cc_btn_clear.Click += cc_btn_clear_Click;
            // 
            // cc_btn_decrypt
            // 
            cc_btn_decrypt.BackColor = Color.LightBlue;
            cc_btn_decrypt.BackgroundGradientEnd = Color.DarkBlue;
            cc_btn_decrypt.BackgroundGradientStart = Color.LightBlue;
            cc_btn_decrypt.BorderColor = Color.Blue;
            cc_btn_decrypt.BorderRadius = 15;
            cc_btn_decrypt.BorderSize = 2;
            cc_btn_decrypt.FlatAppearance.BorderSize = 0;
            cc_btn_decrypt.FlatStyle = FlatStyle.Flat;
            cc_btn_decrypt.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            cc_btn_decrypt.ForeColor = Color.White;
            cc_btn_decrypt.HoverDarken = Color.FromArgb(30, 0, 0, 0);
            cc_btn_decrypt.Location = new Point(155, 15);
            cc_btn_decrypt.Name = "cc_btn_decrypt";
            cc_btn_decrypt.PressedOverlay = Color.FromArgb(80, 0, 0, 0);
            cc_btn_decrypt.Size = new Size(120, 40);
            cc_btn_decrypt.TabIndex = 8;
            cc_btn_decrypt.Text = "Decrypt";
            cc_btn_decrypt.UseGradient = true;
            cc_btn_decrypt.UseVisualStyleBackColor = false;
            cc_btn_decrypt.Click += cc_btn_decrypt_Click;
            // 
            // WF
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(401, 452);
            Controls.Add(ccPanel2);
            Controls.Add(ccPanel1);
            Name = "WF";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WFSC";
            ccPanel1.ResumeLayout(false);
            ccPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private CustomControls.CCPanel ccPanel1;
        private CustomControls.CCTextBox cc_tb_dt;
        private CustomControls.CCTextBox cc_tb_c;
        private CustomControls.CCTextBox cc_tb_k2;
        private CustomControls.CCTextBox cc_tb_k1;
        private CustomControls.CCTextBox cc_tb_pt;
        private CustomControls.CCLabel ccLabel5;
        private CustomControls.CCLabel ccLabel4;
        private CustomControls.CCLabel ccLabel3;
        private CustomControls.CCLabel ccLabel2;
        private CustomControls.CCLabel ccLabel1;
        private CustomControls.CCButton cc_btn_encrypt;
        private CustomControls.CCPanel ccPanel2;
        private CustomControls.CCButton cc_btn_default;
        private CustomControls.CCButton cc_btn_clear;
        private CustomControls.CCButton cc_btn_decrypt;
    }
}
