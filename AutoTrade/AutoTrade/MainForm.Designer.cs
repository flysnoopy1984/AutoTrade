namespace AutoTrade
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bn_Login = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bn_Login
            // 
            this.bn_Login.Location = new System.Drawing.Point(13, 23);
            this.bn_Login.Name = "bn_Login";
            this.bn_Login.Size = new System.Drawing.Size(75, 23);
            this.bn_Login.TabIndex = 0;
            this.bn_Login.Text = "登录";
            this.bn_Login.UseVisualStyleBackColor = true;
            this.bn_Login.Click += new System.EventHandler(this.bn_Login_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(142, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 491);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.bn_Login);
            this.Name = "MainForm";
            this.Text = "抢购神器--QQ:395940187";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bn_Login;
        private System.Windows.Forms.Button button1;
    }
}

