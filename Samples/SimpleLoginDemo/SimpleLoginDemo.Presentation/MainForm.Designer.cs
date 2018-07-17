namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainContentControl = new Charites.Windows.Forms.ContentControl();
            this.SuspendLayout();
            // 
            // mainContentControl
            // 
            resources.ApplyResources(this.mainContentControl, "mainContentControl");
            this.mainContentControl.Name = "mainContentControl";
            this.mainContentControl.TabStop = false;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.mainContentControl);
            this.DoubleBuffered = true;
            this.Name = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Forms.ContentControl mainContentControl;
    }
}