namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation
{
    partial class ApplicationHostView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationHostView));
            this.titleLabel = new System.Windows.Forms.Label();
            this.footerLabel = new System.Windows.Forms.Label();
            this.contentControl = new Charites.Windows.Forms.ContentControl();
            this.dataContextSource = new Charites.Windows.Mvc.DataContextSource(this.components);
            this.windowsFormsController = new Charites.Windows.Mvc.WindowsFormsController(this.components);
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            resources.ApplyResources(this.titleLabel, "titleLabel");
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Name = "titleLabel";
            // 
            // footerLabel
            // 
            this.footerLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            resources.ApplyResources(this.footerLabel, "footerLabel");
            this.footerLabel.ForeColor = System.Drawing.Color.White;
            this.footerLabel.Name = "footerLabel";
            // 
            // contentControl
            // 
            resources.ApplyResources(this.contentControl, "contentControl");
            this.contentControl.Name = "contentControl";
            this.contentControl.TabStop = false;
            // 
            // dataContextSource
            // 
            this.dataContextSource.DataContextChanged += new Charites.Windows.Mvc.DataContextChangedEventHandler(this.dataContextSource_DataContextChanged);
            //
            // windowsFormsController
            //
            this.windowsFormsController.View = this;
            // 
            // ApplicationHostView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.Controls.Add(this.contentControl);
            this.Controls.Add(this.footerLabel);
            this.Controls.Add(this.titleLabel);
            this.DoubleBuffered = true;
            this.Name = "ApplicationHostView";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label footerLabel;
        private Forms.ContentControl contentControl;
        private Mvc.DataContextSource dataContextSource;
        private Mvc.WindowsFormsController windowsFormsController;
    }
}
