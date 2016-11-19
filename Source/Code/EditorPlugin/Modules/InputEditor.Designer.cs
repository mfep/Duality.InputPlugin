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
            this.newButton = new System.Windows.Forms.Button();
            this.virtualButtonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // virtualButtonsPanel
            // 
            this.virtualButtonsPanel.AutoScroll = true;
            this.virtualButtonsPanel.Controls.Add(this.newButton);
            this.virtualButtonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.virtualButtonsPanel.Location = new System.Drawing.Point(0, 0);
            this.virtualButtonsPanel.Name = "virtualButtonsPanel";
            this.virtualButtonsPanel.Padding = new System.Windows.Forms.Padding(8);
            this.virtualButtonsPanel.Size = new System.Drawing.Size(346, 496);
            this.virtualButtonsPanel.TabIndex = 1;
            // 
            // newButton
            // 
            this.newButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.newButton.Location = new System.Drawing.Point(8, 8);
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(330, 23);
            this.newButton.TabIndex = 0;
            this.newButton.Text = "New VirtualButton";
            this.newButton.UseVisualStyleBackColor = true;
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // InputEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
            this.ClientSize = new System.Drawing.Size(346, 496);
            this.Controls.Add(this.virtualButtonsPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InputEditor";
            this.Text = "Input Mapping";
            this.virtualButtonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel virtualButtonsPanel;
        private System.Windows.Forms.Button newButton;
    }
}