namespace AutoTrade
{
    partial class ShowInfoForm
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
            this.tb_Info = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // tb_Info
            // 
            this.tb_Info.Dock = System.Windows.Forms.DockStyle.Top;
            this.tb_Info.Location = new System.Drawing.Point(0, 0);
            this.tb_Info.Name = "tb_Info";
            this.tb_Info.Size = new System.Drawing.Size(786, 511);
            this.tb_Info.TabIndex = 0;
            this.tb_Info.Text = "";
            // 
            // ShowInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 573);
            this.Controls.Add(this.tb_Info);
            this.Name = "ShowInfo";
            this.Text = "ShowInfo";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox tb_Info;
    }
}