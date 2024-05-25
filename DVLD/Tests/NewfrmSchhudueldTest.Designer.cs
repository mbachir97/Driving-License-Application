namespace DVLD.Tests
{
    partial class NewfrmSchhudueldTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewfrmSchhudueldTest));
            this.btnClose = new System.Windows.Forms.Button();
            this.newSchudeldTest1 = new DVLD.Tests.NewSchudeldTest();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(282, 727);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(171, 46);
            this.btnClose.TabIndex = 126;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // newSchudeldTest1
            // 
            this.newSchudeldTest1.Location = new System.Drawing.Point(20, -1);
            this.newSchudeldTest1.Name = "newSchudeldTest1";
            this.newSchudeldTest1.Size = new System.Drawing.Size(668, 721);
            this.newSchudeldTest1.TabIndex = 127;
            this.newSchudeldTest1.TestTypeID = DVLD_Bisness.clsTestType.enTestType.VissionTest;
            // 
            // NewfrmSchhudueldTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(700, 781);
            this.Controls.Add(this.newSchudeldTest1);
            this.Controls.Add(this.btnClose);
            this.Name = "NewfrmSchhudueldTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSchhudueldTest";
            this.Load += new System.EventHandler(this.NewfrmSchhudueldTest_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private NewSchudeldTest newSchudeldTest1;
    }
}