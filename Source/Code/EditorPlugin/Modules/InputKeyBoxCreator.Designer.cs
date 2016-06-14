namespace MFEP.Duality.Editor.Plugins.InputPlugin
{
    partial class InputKeyBoxCreator
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
            this.SuspendLayout();
            // 
            // deleteButton
            // 
            this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteButton.Image = global::MFEP.Duality.Editor.Plugins.InputPlugin.Properties.Resources.add;
            this.toolTip1.SetToolTip(this.deleteButton, "Add new key to VirtualButton");
            // 
            // InputKeyBoxCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BtnTooltip = "Add new key to VirtualButton";
            this.Name = "InputKeyBoxCreator";
            this.ResumeLayout(false);

        }

        #endregion
    }
}
