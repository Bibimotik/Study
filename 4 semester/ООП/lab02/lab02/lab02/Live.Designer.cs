using System.ComponentModel;

namespace lab02
{
    partial class Live
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
            this.adresBox = new System.Windows.Forms.GroupBox();
            this.flat = new System.Windows.Forms.TextBox();
            this.house = new System.Windows.Forms.TextBox();
            this.street = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.index = new System.Windows.Forms.MaskedTextBox();
            this.city = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.errorMessage = new System.Windows.Forms.Label();
            this.@continue = new System.Windows.Forms.Button();
            this.adresBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // adresBox
            // 
            this.adresBox.Controls.Add(this.flat);
            this.adresBox.Controls.Add(this.house);
            this.adresBox.Controls.Add(this.street);
            this.adresBox.Controls.Add(this.label5);
            this.adresBox.Controls.Add(this.index);
            this.adresBox.Controls.Add(this.city);
            this.adresBox.Controls.Add(this.label4);
            this.adresBox.Controls.Add(this.label3);
            this.adresBox.Controls.Add(this.label2);
            this.adresBox.Controls.Add(this.label1);
            this.adresBox.Location = new System.Drawing.Point(21, 12);
            this.adresBox.Name = "adresBox";
            this.adresBox.Size = new System.Drawing.Size(207, 350);
            this.adresBox.TabIndex = 28;
            this.adresBox.TabStop = false;
            this.adresBox.Text = "Адрес";
            // 
            // flat
            // 
            this.flat.Location = new System.Drawing.Point(12, 293);
            this.flat.MaxLength = 4;
            this.flat.Name = "flat";
            this.flat.Size = new System.Drawing.Size(168, 26);
            this.flat.TabIndex = 9;
            // 
            // house
            // 
            this.house.Location = new System.Drawing.Point(10, 239);
            this.house.MaxLength = 10;
            this.house.Name = "house";
            this.house.Size = new System.Drawing.Size(170, 26);
            this.house.TabIndex = 8;
            // 
            // street
            // 
            this.street.Location = new System.Drawing.Point(10, 178);
            this.street.MaxLength = 40;
            this.street.Name = "street";
            this.street.Size = new System.Drawing.Size(170, 26);
            this.street.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 268);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(157, 22);
            this.label5.TabIndex = 6;
            this.label5.Text = "Квартира";
            // 
            // index
            // 
            this.index.Location = new System.Drawing.Point(10, 125);
            this.index.Mask = "000-000";
            this.index.Name = "index";
            this.index.Size = new System.Drawing.Size(170, 26);
            this.index.TabIndex = 5;
            // 
            // city
            // 
            this.city.Location = new System.Drawing.Point(10, 56);
            this.city.MaxLength = 20;
            this.city.Name = "city";
            this.city.Size = new System.Drawing.Size(170, 26);
            this.city.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 216);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(165, 28);
            this.label4.TabIndex = 3;
            this.label4.Text = "Дом";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(173, 28);
            this.label3.TabIndex = 2;
            this.label3.Text = "Улица";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 27);
            this.label2.TabIndex = 1;
            this.label2.Text = "Индекс";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Город";
            // 
            // errorMessage
            // 
            this.errorMessage.ForeColor = System.Drawing.Color.Red;
            this.errorMessage.Location = new System.Drawing.Point(31, 446);
            this.errorMessage.Name = "errorMessage";
            this.errorMessage.Size = new System.Drawing.Size(201, 37);
            this.errorMessage.TabIndex = 37;
            this.errorMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // continue
            // 
            this.@continue.Location = new System.Drawing.Point(69, 384);
            this.@continue.Name = "continue";
            this.@continue.Size = new System.Drawing.Size(123, 37);
            this.@continue.TabIndex = 36;
            this.@continue.Text = "Продолжить";
            this.@continue.UseVisualStyleBackColor = true;
            this.@continue.Click += new System.EventHandler(this.saveAdres_Click);
            // 
            // Live
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(253, 504);
            this.Controls.Add(this.errorMessage);
            this.Controls.Add(this.@continue);
            this.Controls.Add(this.adresBox);
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "Live";
            this.adresBox.ResumeLayout(false);
            this.adresBox.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TextBox house;
        private System.Windows.Forms.TextBox flat;

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox street;

        private System.Windows.Forms.MaskedTextBox index;

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox city;

        private System.Windows.Forms.Button @continue;

        private System.Windows.Forms.Label errorMessage;

        private System.Windows.Forms.GroupBox adresBox;

        #endregion
    }
}