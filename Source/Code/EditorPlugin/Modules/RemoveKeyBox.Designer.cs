﻿namespace MFEP.Duality.Editor.Plugins.InputPlugin.Modules
{
	partial class RemoveKeyBox
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
			this.SuspendLayout();
			// 
			// comboBox
			// 
			this.comboBox.Enabled = false;
			this.toolTip1.SetToolTip(this.comboBox, "Physical key on international keyboard");
			// 
			// addRemoveBtn
			// 
			this.toolTip1.SetToolTip(this.addRemoveBtn, "Remove this Key from the VirtualButton.");
			this.addRemoveBtn.Click += new System.EventHandler(this.removeButton_Click);
			// 
			// KeyTypeBtn
			// 
			this.KeyTypeBtn.Enabled = false;
			// 
			// RemoveKeyBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "RemoveKeyBox";
			this.ResumeLayout(false);

		}

		#endregion
	}
}