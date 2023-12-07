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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeView));
            commandPanel = new Panel();
            logoutButton = new Button();
            messageLabel = new Label();
            windowsFormsController = new Mvc.WindowsFormsController(components);
            commandPanel.SuspendLayout();
            SuspendLayout();
            // 
            // commandPanel
            // 
            commandPanel.Controls.Add(logoutButton);
            resources.ApplyResources(commandPanel, "commandPanel");
            commandPanel.Name = "commandPanel";
            // 
            // logoutButton
            // 
            resources.ApplyResources(logoutButton, "logoutButton");
            logoutButton.Name = "logoutButton";
            logoutButton.UseVisualStyleBackColor = true;
            // 
            // messageLabel
            // 
            resources.ApplyResources(messageLabel, "messageLabel");
            messageLabel.Name = "messageLabel";
            // 
            // windowsFormsController
            // 
            windowsFormsController.View = this;
            // 
            // HomeView
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Dpi;
            Controls.Add(messageLabel);
            Controls.Add(commandPanel);
            DoubleBuffered = true;
            Name = "HomeView";
            DataContextChanged += HomeView_DataContextChanged;
            commandPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel commandPanel;
        private System.Windows.Forms.Button logoutButton;
        private System.Windows.Forms.Label messageLabel;
        private Mvc.WindowsFormsController windowsFormsController;
    }
}
