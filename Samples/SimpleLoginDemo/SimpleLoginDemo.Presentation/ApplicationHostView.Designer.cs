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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationHostView));
            titleLabel = new Label();
            footerLabel = new Label();
            contentControl = new Charites.Windows.Forms.ContentControl();
            windowsFormsController = new Charites.Windows.Mvc.WindowsFormsController(components);
            SuspendLayout();
            // 
            // titleLabel
            // 
            titleLabel.BackColor = Color.FromArgb(51, 51, 51);
            resources.ApplyResources(titleLabel, "titleLabel");
            titleLabel.ForeColor = Color.White;
            titleLabel.Name = "titleLabel";
            // 
            // footerLabel
            // 
            footerLabel.BackColor = Color.FromArgb(51, 51, 51);
            resources.ApplyResources(footerLabel, "footerLabel");
            footerLabel.ForeColor = Color.White;
            footerLabel.Name = "footerLabel";
            // 
            // contentControl
            // 
            resources.ApplyResources(contentControl, "contentControl");
            contentControl.Name = "contentControl";
            contentControl.TabStop = true;
            // 
            // windowsFormsController
            // 
            windowsFormsController.View = this;
            // 
            // ApplicationHostView
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(204, 204, 204);
            Controls.Add(contentControl);
            Controls.Add(footerLabel);
            Controls.Add(titleLabel);
            DoubleBuffered = true;
            Name = "ApplicationHostView";
            DataContextChanged += ApplicationHostView_DataContextChanged;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label footerLabel;
        private Forms.ContentControl contentControl;
        private Mvc.WindowsFormsController windowsFormsController;
    }
}
