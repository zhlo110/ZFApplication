namespace ZFApplication.UI
{
    partial class UpdateListForm
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
            this.components = new System.ComponentModel.Container();
            this.mListBox = new DevExpress.XtraEditors.ListBoxControl();
            this.mMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.mListBox)).BeginInit();
            this.mMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mListBox
            // 
            this.mListBox.ContextMenuStrip = this.mMenuStrip;
            this.mListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mListBox.Location = new System.Drawing.Point(0, 0);
            this.mListBox.Name = "mListBox";
            this.mListBox.Size = new System.Drawing.Size(269, 298);
            this.mListBox.TabIndex = 0;
            // 
            // mMenuStrip
            // 
            this.mMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteMenuItem});
            this.mMenuStrip.Name = "mMenuStrip";
            this.mMenuStrip.Size = new System.Drawing.Size(101, 26);
            // 
            // DeleteMenuItem
            // 
            this.DeleteMenuItem.Name = "DeleteMenuItem";
            this.DeleteMenuItem.Size = new System.Drawing.Size(180, 22);
            this.DeleteMenuItem.Text = "刪除";
            this.DeleteMenuItem.Click += new System.EventHandler(this.DeleteMenuItem_Click);
            // 
            // UpdateListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 298);
            this.Controls.Add(this.mListBox);
            this.Name = "UpdateListForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "更新列表";
            this.Load += new System.EventHandler(this.onLoad);
            ((System.ComponentModel.ISupportInitialize)(this.mListBox)).EndInit();
            this.mMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ListBoxControl mListBox;
        private System.Windows.Forms.ContextMenuStrip mMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem DeleteMenuItem;
    }
}