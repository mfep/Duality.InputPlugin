namespace MFEP.Duality.Editor.Plugins.InputPlugin.Modules
{
	partial class AddKeyBox
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
			// button
			// 
			this.button.Image = global::MFEP.Duality.Editor.Plugins.InputPlugin.Properties.Resources.add;
			this.toolTip1.SetToolTip(this.button, "Remove this key from the VirtualButton");
			this.button.Click += new System.EventHandler(this.button_Click);
			// 
			// AddKeyBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "AddKeyBox";
			this.ResumeLayout(false);

		}

		#endregion
	}
}
