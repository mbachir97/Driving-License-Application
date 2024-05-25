namespace DVLD.Tests
{
    partial class frmSchudueldTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSchudueldTest));
            this.scheduelTest1 = new DVLD.Tests.scheduelTest();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // scheduelTest1
            // 
            this.scheduelTest1.Location = new System.Drawing.Point(12, 2);
            this.scheduelTest1.Name = "scheduelTest1";
            this.scheduelTest1.Size = new System.Drawing.Size(726, 682);
            this.scheduelTest1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(365, 692);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(132, 50);
            this.btnClose.TabIndex = 29;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // frmSchudueldTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 747);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.scheduelTest1);
            this.Name = "frmSchudueldTest";
            this.Text = "frmScheduled";
            this.Load += new System.EventHandler(this.frmSchudueldTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private scheduelTest scheduelTest1;
        private System.Windows.Forms.Button btnClose;
    }
}