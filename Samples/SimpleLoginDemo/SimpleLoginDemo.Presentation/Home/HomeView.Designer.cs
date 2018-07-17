namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Home
{
    partial class HomeView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeView));
            this.commandPanel = new System.Windows.Forms.Panel();
            this.messageLabel = new System.Windows.Forms.Label();
            this.logoutButton = new System.Windows.Forms.Button();
            this.dataContextSource = new Charites.Windows.Mvc.DataContextSource(this.components);
            this.windowsFormsController = new Charites.Windows.Mvc.WindowsFormsController(this.components);
            this.commandPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // commandPanel
            // 
            this.commandPanel.Controls.Add(this.logoutButton);
            resources.ApplyResources(this.commandPanel, "commandPanel");
            this.commandPanel.Name = "commandPanel";
            // 
            // messageLabel
            // 
            resources.ApplyResources(this.messageLabel, "messageLabel");
            this.messageLabel.Name = "messageLabel";
            // 
            // logoutButton
            // 
            resources.ApplyResources(this.logoutButton, "logoutButton");
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.UseVisualStyleBackColor = true;
            // 
            // dataContextSource
            // 
            this.dataContextSource.DataContextChanged += new Charites.Windows.Mvc.DataContextChangedEventHandler(this.dataContextSource_DataContextChanged);
            // 
            // windowsFormsController
            // 
            this.windowsFormsController.View = this;
            // 
            // HomeView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.commandPanel);
            this.DoubleBuffered = true;
            this.Name = "HomeView";
            this.commandPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel commandPanel;
        private System.Windows.Forms.Button logoutButton;
        private System.Windows.Forms.Label messageLabel;
        private Mvc.DataContextSource dataContextSource;
        private Mvc.WindowsFormsController windowsFormsController;
    }
}
