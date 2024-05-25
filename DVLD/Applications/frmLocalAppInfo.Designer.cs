namespace DVLD.Applications
{
    partial class frmLocalAppInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLocalAppInfo));
            this.ctrDrivingLicenceApplicatioInfo1 = new DVLD.Licenses.ctrDrivingLicenceApplicatioInfo();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrDrivingLicenceApplicatioInfo1
            // 
            this.ctrDrivingLicenceApplicatioInfo1.Location = new System.Drawing.Point(2, 12);
            this.ctrDrivingLicenceApplicatioInfo1.Name = "ctrDrivingLicenceApplicatioInfo1";
            this.ctrDrivingLicenceApplicatioInfo1.Size = new System.Drawing.Size(1174, 385);
            this.ctrDrivingLicenceApplicatioInfo1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(920, 403);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(132, 50);
            this.btnClose.TabIndex = 123;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmLocalAppInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 460);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrDrivingLicenceApplicatioInfo1);
            this.Name = "frmLocalAppInfo";
            this.Text = "frmLocalAppInfo";
            this.Load += new System.EventHandler(this.frmLocalAppInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Licenses.ctrDrivingLicenceApplicatioInfo ctrDrivingLicenceApplicatioInfo1;
        private System.Windows.Forms.Button btnClose;
    }
}