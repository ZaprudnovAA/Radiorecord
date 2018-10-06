using System.ComponentModel;
using System.Windows.Forms;

namespace Radio
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
            notifyIcon1 = new NotifyIcon(components);
            SuspendLayout();
            // 
            // notifyIcon1
            // 
            notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            notifyIcon1.Text = "Radiorecord";
            notifyIcon1.MouseClick += new MouseEventHandler(notifyIcon1_MouseClick);
            notifyIcon1.MouseDoubleClick += new MouseEventHandler(notifyIcon1_MouseClick);
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            Height = SystemInformation.PrimaryMonitorSize.Height - 80;
            BackgroundImage = Properties.Resources.record;
            BackgroundImageLayout = ImageLayout.Stretch;
            Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            KeyPreview = true;
            Location = new System.Drawing.Point(10, 10);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.Manual;
            Text = "Radiorecord";
            Load += new System.EventHandler(Form1_Load);
            KeyDown += new KeyEventHandler(Form1_KeyDown);
            Resize += new System.EventHandler(Form1_Resize);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        public NotifyIcon notifyIcon1;
    }
}

