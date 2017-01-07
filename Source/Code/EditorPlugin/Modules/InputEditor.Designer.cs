namespace MFEP.Duality.Editor.Plugins.InputPlugin.Modules
{
    partial class InputEditor
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputEditor));
			this.virtualButtonsPanel = new System.Windows.Forms.Panel();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.addButton = new System.Windows.Forms.ToolStripButton();
			this.issueButton = new System.Windows.Forms.ToolStripButton();
			this.helpButton = new System.Windows.Forms.ToolStripButton();
			this.toolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// virtualButtonsPanel
			// 
			this.virtualButtonsPanel.AutoScroll = true;
			this.virtualButtonsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(196)))), ((int)(((byte)(196)))));
			this.virtualButtonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.virtualButtonsPanel.Location = new System.Drawing.Point(0, 25);
			this.virtualButtonsPanel.Name = "virtualButtonsPanel";
			this.virtualButtonsPanel.Size = new System.Drawing.Size(346, 471);
			this.virtualButtonsPanel.TabIndex = 1;
			// 
			// toolStrip
			// 
			this.toolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
			this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addButton,
            this.issueButton,
            this.helpButton});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(346, 25);
			this.toolStrip.TabIndex = 0;
			this.toolStrip.Text = "toolStrip";
			// 
			// addButton
			// 
			this.addButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.addButton.Image = global::MFEP.Duality.Editor.Plugins.InputPlugin.Properties.Resources.add;
			this.addButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(23, 22);
			this.addButton.Text = "Add New Virtual Button";
			this.addButton.Click += new System.EventHandler(this.addButton_Click);
			// 
			// issueButton
			// 
			this.issueButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.issueButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.issueButton.Image = global::MFEP.Duality.Editor.Plugins.InputPlugin.Properties.Resources.mail_red;
			this.issueButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.issueButton.Name = "issueButton";
			this.issueButton.Size = new System.Drawing.Size(23, 22);
			this.issueButton.Text = "Report Issue";
			this.issueButton.Click += new System.EventHandler(this.issueButton_Click);
			// 
			// helpButton
			// 
			this.helpButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.helpButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.helpButton.Image = global::MFEP.Duality.Editor.Plugins.InputPlugin.Properties.Resources.emotion_question;
			this.helpButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.helpButton.Name = "helpButton";
			this.helpButton.Size = new System.Drawing.Size(23, 22);
			this.helpButton.Text = "User Guide";
			this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
			// 
			// InputEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
			this.ClientSize = new System.Drawing.Size(346, 496);
			this.Controls.Add(this.virtualButtonsPanel);
			this.Controls.Add(this.toolStrip);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "InputEditor";
			this.Text = "Input Mapping";
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel virtualButtonsPanel;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton addButton;
		private System.Windows.Forms.ToolStripButton issueButton;
		private System.Windows.Forms.ToolStripButton helpButton;
	}
}