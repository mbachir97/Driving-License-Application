namespace DVLD.Licenses
{
    partial class frmshowPersonLicenseHistory
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
            this.ctrDriverLicense1 = new DVLD.Drivers.ctrDriverLicense();
            this.ctrPersonCardWithFilter1 = new DVLD.Controles.ctrPersonCardWithFilter();
            this.SuspendLayout();
            // 
            // ctrDriverLicense1
            // 
            this.ctrDriverLicense1.Location = new System.Drawing.Point(26, 513);
            this.ctrDriverLicense1.Name = "ctrDriverLicense1";
            this.ctrDriverLicense1.Size = new System.Drawing.Size(1090, 392);
            this.ctrDriverLicense1.TabIndex = 0;
            // 
            // ctrPersonCardWithFilter1
            // 
            this.ctrPersonCardWithFilter1.Location = new System.Drawing.Point(49, 12);
            this.ctrPersonCardWithFilter1.Name = "ctrPersonCardWithFilter1";
            this.ctrPersonCardWithFilter1.Size = new System.Drawing.Size(1067, 480);
            this.ctrPersonCardWithFilter1.TabIndex = 1;
            // 
            // frmshowPersonLicenseHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1139, 929);
            this.Controls.Add(this.ctrPersonCardWithFilter1);
            this.Controls.Add(this.ctrDriverLicense1);
            this.Name = "frmshowPersonLicenseHistory";
            this.Text = "showPersonLicenseHistory";
            this.Load += new System.EventHandler(this.showPersonLicenseHistory_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Drivers.ctrDriverLicense ctrDriverLicense1;
        private Controles.ctrPersonCardWithFilter ctrPersonCardWithFilter1;
    }
}