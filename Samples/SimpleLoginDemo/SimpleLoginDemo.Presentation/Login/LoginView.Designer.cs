namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Login
{
    partial class LoginView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginView));
            loginPanel = new Panel();
            loginButton = new Button();
            passwordTextBox = new TextBox();
            passwordHeaderLabel = new Label();
            userIdTextBox = new TextBox();
            userIdHeaderLabel = new Label();
            windowsFormsController = new Mvc.WindowsFormsController(components);
            messageLabel = new Label();
            loginPanel.SuspendLayout();
            SuspendLayout();
            // 
            // loginPanel
            // 
            loginPanel.BackColor = Color.FromArgb(238, 238, 238);
            loginPanel.BorderStyle = BorderStyle.FixedSingle;
            loginPanel.Controls.Add(loginButton);
            loginPanel.Controls.Add(passwordTextBox);
            loginPanel.Controls.Add(passwordHeaderLabel);
            loginPanel.Controls.Add(userIdTextBox);
            loginPanel.Controls.Add(userIdHeaderLabel);
            resources.ApplyResources(loginPanel, "loginPanel");
            loginPanel.Name = "loginPanel";
            // 
            // loginButton
            // 
            resources.ApplyResources(loginButton, "loginButton");
            loginButton.Name = "loginButton";
            loginButton.UseVisualStyleBackColor = true;
            // 
            // passwordTextBox
            // 
            resources.ApplyResources(passwordTextBox, "passwordTextBox");
            passwordTextBox.Name = "passwordTextBox";
            // 
            // passwordHeaderLabel
            // 
            resources.ApplyResources(passwordHeaderLabel, "passwordHeaderLabel");
            passwordHeaderLabel.Name = "passwordHeaderLabel";
            // 
            // userIdTextBox
            // 
            resources.ApplyResources(userIdTextBox, "userIdTextBox");
            userIdTextBox.Name = "userIdTextBox";
            // 
            // userIdHeaderLabel
            // 
            resources.ApplyResources(userIdHeaderLabel, "userIdHeaderLabel");
            userIdHeaderLabel.Name = "userIdHeaderLabel";
            // 
            // windowsFormsController
            // 
            windowsFormsController.View = this;
            // 
            // messageLabel
            // 
            resources.ApplyResources(messageLabel, "messageLabel");
            messageLabel.ForeColor = Color.Red;
            messageLabel.Name = "messageLabel";
            // 
            // LoginView
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Dpi;
            Controls.Add(messageLabel);
            Controls.Add(loginPanel);
            DoubleBuffered = true;
            Name = "LoginView";
            DataContextChanged += LoginView_DataContextChanged;
            loginPanel.ResumeLayout(false);
            loginPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel loginPanel;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label passwordHeaderLabel;
        private System.Windows.Forms.TextBox userIdTextBox;
        private System.Windows.Forms.Label userIdHeaderLabel;
        private System.Windows.Forms.Button loginButton;
        private Mvc.WindowsFormsController windowsFormsController;
        private System.Windows.Forms.Label messageLabel;
    }
}
