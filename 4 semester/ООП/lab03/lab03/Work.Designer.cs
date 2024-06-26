using System.ComponentModel;

namespace lab03
{
    partial class Work
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
            this.saveJob = new System.Windows.Forms.Button();
            this.jobBox = new System.Windows.Forms.GroupBox();
            this.experience = new System.Windows.Forms.NumericUpDown();
            this.labelExperiense = new System.Windows.Forms.Label();
            this.post = new System.Windows.Forms.TextBox();
            this.labelPost = new System.Windows.Forms.Label();
            this.company = new System.Windows.Forms.TextBox();
            this.labelCompany = new System.Windows.Forms.Label();
            this.errorMessage = new System.Windows.Forms.Label();
            this.jobBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.experience)).BeginInit();
            this.SuspendLayout();
            // 
            // saveJob
            // 
            this.saveJob.Location = new System.Drawing.Point(12, 260);
            this.saveJob.Name = "saveJob";
            this.saveJob.Size = new System.Drawing.Size(123, 37);
            this.saveJob.TabIndex = 36;
            this.saveJob.Text = "Продолжить";
            this.saveJob.UseVisualStyleBackColor = true;
            this.saveJob.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // jobBox
            // 
            this.jobBox.Controls.Add(this.experience);
            this.jobBox.Controls.Add(this.labelExperiense);
            this.jobBox.Controls.Add(this.post);
            this.jobBox.Controls.Add(this.labelPost);
            this.jobBox.Controls.Add(this.company);
            this.jobBox.Controls.Add(this.labelCompany);
            this.jobBox.Location = new System.Drawing.Point(12, 12);
            this.jobBox.Name = "jobBox";
            this.jobBox.Size = new System.Drawing.Size(268, 228);
            this.jobBox.TabIndex = 39;
            this.jobBox.TabStop = false;
            this.jobBox.Text = "Текущая работа";
            // 
            // experience
            // 
            this.experience.Location = new System.Drawing.Point(16, 179);
            this.experience.Name = "experience";
            this.experience.Size = new System.Drawing.Size(235, 26);
            this.experience.TabIndex = 5;
            // 
            // labelExperiense
            // 
            this.labelExperiense.Location = new System.Drawing.Point(16, 153);
            this.labelExperiense.Name = "labelExperiense";
            this.labelExperiense.Size = new System.Drawing.Size(100, 23);
            this.labelExperiense.TabIndex = 4;
            this.labelExperiense.Text = "Стаж";
            // 
            // post
            // 
            this.post.Location = new System.Drawing.Point(16, 114);
            this.post.MaxLength = 1000;
            this.post.Name = "post";
            this.post.Size = new System.Drawing.Size(235, 26);
            this.post.TabIndex = 3;
            // 
            // labelPost
            // 
            this.labelPost.Location = new System.Drawing.Point(16, 88);
            this.labelPost.Name = "labelPost";
            this.labelPost.Size = new System.Drawing.Size(100, 23);
            this.labelPost.TabIndex = 2;
            this.labelPost.Text = "Должность";
            // 
            // company
            // 
            this.company.Location = new System.Drawing.Point(16, 49);
            this.company.MaxLength = 1000;
            this.company.Name = "company";
            this.company.Size = new System.Drawing.Size(235, 26);
            this.company.TabIndex = 1;
            // 
            // labelCompany
            // 
            this.labelCompany.Location = new System.Drawing.Point(16, 23);
            this.labelCompany.Name = "labelCompany";
            this.labelCompany.Size = new System.Drawing.Size(100, 23);
            this.labelCompany.TabIndex = 0;
            this.labelCompany.Text = "Компания";
            // 
            // errorMessage
            // 
            this.errorMessage.ForeColor = System.Drawing.Color.Red;
            this.errorMessage.Location = new System.Drawing.Point(141, 260);
            this.errorMessage.Name = "errorMessage";
            this.errorMessage.Size = new System.Drawing.Size(249, 78);
            this.errorMessage.TabIndex = 40;
            this.errorMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Work
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 334);
            this.Controls.Add(this.errorMessage);
            this.Controls.Add(this.jobBox);
            this.Controls.Add(this.saveJob);
            this.Name = "Work";
            this.Text = "Form3";
            this.jobBox.ResumeLayout(false);
            this.jobBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.experience)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.NumericUpDown experience;

        private System.Windows.Forms.Label errorMessage;

        private System.Windows.Forms.GroupBox jobBox;
        private System.Windows.Forms.Label labelCompany;
        private System.Windows.Forms.TextBox company;
        private System.Windows.Forms.Label labelPost;
        private System.Windows.Forms.TextBox post;
        private System.Windows.Forms.Label labelExperiense;

        private System.Windows.Forms.Button saveJob;

        #endregion
    }
}