namespace MFEP.Duality.Editor.Plugins.InputPlugin.Modules
{
	partial class InputKeyBox
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.comboBox = new System.Windows.Forms.ComboBox();
			this.addRemoveBtn = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.KeyTypeBtn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// comboBox
			// 
			this.comboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox.FormattingEnabled = true;
			this.comboBox.Location = new System.Drawing.Point(42, 0);
			this.comboBox.Margin = new System.Windows.Forms.Padding(0);
			this.comboBox.MaximumSize = new System.Drawing.Size(99, 0);
			this.comboBox.Name = "comboBox";
			this.comboBox.Size = new System.Drawing.Size(99, 21);
			this.comboBox.TabIndex = 0;
			this.toolTip1.SetToolTip(this.comboBox, "Physical key on international keyboard");
			// 
			// addRemoveBtn
			// 
			this.addRemoveBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
			this.addRemoveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.addRemoveBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
			this.addRemoveBtn.Image = global::MFEP.Duality.Editor.Plugins.InputPlugin.Properties.Resources.delete;
			this.addRemoveBtn.Location = new System.Drawing.Point(0, 0);
			this.addRemoveBtn.Margin = new System.Windows.Forms.Padding(0);
			this.addRemoveBtn.Name = "addRemoveBtn";
			this.addRemoveBtn.Size = new System.Drawing.Size(21, 21);
			this.addRemoveBtn.TabIndex = 1;
			this.addRemoveBtn.UseVisualStyleBackColor = false;
			// 
			// KeyTypeBtn
			// 
			this.KeyTypeBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
			this.KeyTypeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.KeyTypeBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
			this.KeyTypeBtn.Image = global::MFEP.Duality.Editor.Plugins.InputPlugin.Properties.Resources.keyboard;
			this.KeyTypeBtn.Location = new System.Drawing.Point(21, 0);
			this.KeyTypeBtn.Margin = new System.Windows.Forms.Padding(0);
			this.KeyTypeBtn.Name = "KeyTypeBtn";
			this.KeyTypeBtn.Size = new System.Drawing.Size(21, 21);
			this.KeyTypeBtn.TabIndex = 2;
			this.toolTip1.SetToolTip(this.KeyTypeBtn, "Select input device");
			this.KeyTypeBtn.UseVisualStyleBackColor = false;
			this.KeyTypeBtn.Click += new System.EventHandler(this.KeyTypeBtn_Click);
			// 
			// InputKeyBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
			this.Controls.Add(this.comboBox);
			this.Controls.Add(this.KeyTypeBtn);
			this.Controls.Add(this.addRemoveBtn);
			this.MaximumSize = new System.Drawing.Size(141, 21);
			this.MinimumSize = new System.Drawing.Size(141, 21);
			this.Name = "InputKeyBox";
			this.Size = new System.Drawing.Size(141, 21);
			this.ResumeLayout(false);

		}

		#endregion

		protected System.Windows.Forms.ComboBox comboBox;
		protected System.Windows.Forms.Button addRemoveBtn;
		protected System.Windows.Forms.ToolTip toolTip1;
		protected System.Windows.Forms.Button KeyTypeBtn;
	}
}
