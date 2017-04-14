namespace MFEP.Duality.Editor.Plugins.InputPlugin.Modules
{
    partial class ButtonControl
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
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.panel1 = new System.Windows.Forms.Panel();
			this.riseTimeTextBox = new System.Windows.Forms.TextBox();
			this.riseTimeLabel = new System.Windows.Forms.Label();
			this.removeButton = new System.Windows.Forms.Button();
			this.nameLabel = new System.Windows.Forms.Label();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.negativePanel = new System.Windows.Forms.Panel();
			this.label3 = new System.Windows.Forms.Label();
			this.negativeKeysPanel = new System.Windows.Forms.Panel();
			this.positivePanel = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.positiveKeysPanel = new System.Windows.Forms.Panel();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.negativePanel.SuspendLayout();
			this.positivePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.AutoSize = true;
			this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
			this.panel1.Controls.Add(this.riseTimeTextBox);
			this.panel1.Controls.Add(this.riseTimeLabel);
			this.panel1.Controls.Add(this.removeButton);
			this.panel1.Controls.Add(this.nameLabel);
			this.panel1.Controls.Add(this.nameTextBox);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(6, 6);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(4);
			this.panel1.Size = new System.Drawing.Size(114, 128);
			this.panel1.TabIndex = 0;
			// 
			// riseTimeTextBox
			// 
			this.riseTimeTextBox.Location = new System.Drawing.Point(44, 44);
			this.riseTimeTextBox.Name = "riseTimeTextBox";
			this.riseTimeTextBox.Size = new System.Drawing.Size(63, 20);
			this.riseTimeTextBox.TabIndex = 4;
			this.riseTimeTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.riseTimeTextBox_Validating);
			// 
			// riseTimeLabel
			// 
			this.riseTimeLabel.AutoSize = true;
			this.riseTimeLabel.Location = new System.Drawing.Point(7, 47);
			this.riseTimeLabel.Name = "riseTimeLabel";
			this.riseTimeLabel.Size = new System.Drawing.Size(31, 13);
			this.riseTimeLabel.TabIndex = 3;
			this.riseTimeLabel.Text = "Rise:";
			// 
			// removeButton
			// 
			this.removeButton.Location = new System.Drawing.Point(10, 98);
			this.removeButton.Name = "removeButton";
			this.removeButton.Size = new System.Drawing.Size(75, 23);
			this.removeButton.TabIndex = 2;
			this.removeButton.Text = "Remove";
			this.toolTip1.SetToolTip(this.removeButton, "Delete this VirtualButton");
			this.removeButton.UseVisualStyleBackColor = true;
			this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
			// 
			// nameLabel
			// 
			this.nameLabel.AutoSize = true;
			this.nameLabel.Location = new System.Drawing.Point(7, 4);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(35, 13);
			this.nameLabel.TabIndex = 1;
			this.nameLabel.Text = "Name";
			this.toolTip1.SetToolTip(this.nameLabel, "Enter the string you can use from code for this VirtualButton");
			// 
			// nameTextBox
			// 
			this.nameTextBox.Location = new System.Drawing.Point(7, 20);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Size = new System.Drawing.Size(100, 20);
			this.nameTextBox.TabIndex = 0;
			this.toolTip1.SetToolTip(this.nameTextBox, "Enter the string you can use from code for this VirtualButton");
			this.nameTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
			this.nameTextBox.Leave += new System.EventHandler(this.textBox1_Leave);
			// 
			// panel2
			// 
			this.panel2.AutoSize = true;
			this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
			this.panel2.Controls.Add(this.negativePanel);
			this.panel2.Controls.Add(this.positivePanel);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(120, 6);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(0, 128);
			this.panel2.TabIndex = 1;
			// 
			// negativePanel
			// 
			this.negativePanel.AutoSize = true;
			this.negativePanel.Controls.Add(this.label3);
			this.negativePanel.Controls.Add(this.negativeKeysPanel);
			this.negativePanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.negativePanel.Location = new System.Drawing.Point(0, 20);
			this.negativePanel.Margin = new System.Windows.Forms.Padding(0);
			this.negativePanel.Name = "negativePanel";
			this.negativePanel.Size = new System.Drawing.Size(0, 23);
			this.negativePanel.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(-76, 6);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(76, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Negative Keys";
			// 
			// negativeKeysPanel
			// 
			this.negativeKeysPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.negativeKeysPanel.AutoSize = true;
			this.negativeKeysPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.negativeKeysPanel.Location = new System.Drawing.Point(-145, 20);
			this.negativeKeysPanel.MinimumSize = new System.Drawing.Size(141, 0);
			this.negativeKeysPanel.Name = "negativeKeysPanel";
			this.negativeKeysPanel.Size = new System.Drawing.Size(141, 0);
			this.negativeKeysPanel.TabIndex = 3;
			// 
			// positivePanel
			// 
			this.positivePanel.AutoSize = true;
			this.positivePanel.Controls.Add(this.label2);
			this.positivePanel.Controls.Add(this.positiveKeysPanel);
			this.positivePanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.positivePanel.Location = new System.Drawing.Point(0, 0);
			this.positivePanel.Margin = new System.Windows.Forms.Padding(0);
			this.positivePanel.Name = "positivePanel";
			this.positivePanel.Size = new System.Drawing.Size(0, 20);
			this.positivePanel.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(-76, 4);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(70, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Positive Keys";
			// 
			// positiveKeysPanel
			// 
			this.positiveKeysPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.positiveKeysPanel.AutoSize = true;
			this.positiveKeysPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.positiveKeysPanel.Location = new System.Drawing.Point(-145, 17);
			this.positiveKeysPanel.MinimumSize = new System.Drawing.Size(141, 0);
			this.positiveKeysPanel.Name = "positiveKeysPanel";
			this.positiveKeysPanel.Size = new System.Drawing.Size(141, 0);
			this.positiveKeysPanel.TabIndex = 3;
			// 
			// ButtonControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(196)))), ((int)(((byte)(196)))));
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.MinimumSize = new System.Drawing.Size(0, 140);
			this.Name = "ButtonControl";
			this.Padding = new System.Windows.Forms.Padding(6);
			this.Size = new System.Drawing.Size(126, 140);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.negativePanel.ResumeLayout(false);
			this.negativePanel.PerformLayout();
			this.positivePanel.ResumeLayout(false);
			this.positivePanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel positiveKeysPanel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button removeButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel positivePanel;
		private System.Windows.Forms.Panel negativePanel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Panel negativeKeysPanel;
		private System.Windows.Forms.Label riseTimeLabel;
		private System.Windows.Forms.TextBox riseTimeTextBox;
	}
}
