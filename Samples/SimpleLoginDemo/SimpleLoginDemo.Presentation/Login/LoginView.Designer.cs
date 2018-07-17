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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginView));
            this.loginPanel = new System.Windows.Forms.Panel();
            this.loginButton = new System.Windows.Forms.Button();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.passwordHeaderLabel = new System.Windows.Forms.Label();
            this.userIdTextBox = new System.Windows.Forms.TextBox();
            this.userIdHeaderLabel = new System.Windows.Forms.Label();
            this.dataContextSource = new Charites.Windows.Mvc.DataContextSource(this.components);
            this.windowsFormsController = new Charites.Windows.Mvc.WindowsFormsController(this.components);
            this.messageLabel = new System.Windows.Forms.Label();
            this.loginPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // loginPanel
            // 
            this.loginPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.loginPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.loginPanel.Controls.Add(this.loginButton);
            this.loginPanel.Controls.Add(this.passwordTextBox);
            this.loginPanel.Controls.Add(this.passwordHeaderLabel);
            this.loginPanel.Controls.Add(this.userIdTextBox);
            this.loginPanel.Controls.Add(this.userIdHeaderLabel);
            resources.ApplyResources(this.loginPanel, "loginPanel");
            this.loginPanel.Name = "loginPanel";
            // 
            // loginButton
            // 
            resources.ApplyResources(this.loginButton, "loginButton");
            this.loginButton.Name = "loginButton";
            this.loginButton.UseVisualStyleBackColor = true;
            // 
            // passwordTextBox
            // 
            resources.ApplyResources(this.passwordTextBox, "passwordTextBox");
            this.passwordTextBox.Name = "passwordTextBox";
            // 
            // passwordHeaderLabel
            // 
            resources.ApplyResources(this.passwordHeaderLabel, "passwordHeaderLabel");
            this.passwordHeaderLabel.Name = "passwordHeaderLabel";
            // 
            // userIdTextBox
            // 
            resources.ApplyResources(this.userIdTextBox, "userIdTextBox");
            this.userIdTextBox.Name = "userIdTextBox";
            // 
            // userIdHeaderLabel
            // 
            resources.ApplyResources(this.userIdHeaderLabel, "userIdHeaderLabel");
            this.userIdHeaderLabel.Name = "userIdHeaderLabel";
            // 
            // dataContextSource
            // 
            this.dataContextSource.DataContextChanged += new Charites.Windows.Mvc.DataContextChangedEventHandler(this.dataContextSource_DataContextChanged);
            // 
            // windowsFormsController
            // 
            this.windowsFormsController.View = this;
            // 
            // messageLabel
            // 
            resources.ApplyResources(this.messageLabel, "messageLabel");
            this.messageLabel.ForeColor = System.Drawing.Color.Red;
            this.messageLabel.Name = "messageLabel";
            // 
            // LoginView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.loginPanel);
            this.DoubleBuffered = true;
            this.Name = "LoginView";
            this.loginPanel.ResumeLayout(false);
            this.loginPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel loginPanel;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label passwordHeaderLabel;
        private System.Windows.Forms.TextBox userIdTextBox;
        private System.Windows.Forms.Label userIdHeaderLabel;
        private System.Windows.Forms.Button loginButton;
        private Mvc.DataContextSource dataContextSource;
        private Mvc.WindowsFormsController windowsFormsController;
        private System.Windows.Forms.Label messageLabel;
    }
}
