using System.ComponentModel;

namespace lab03
{
    partial class SearchResultForm
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
            this.searchGrid = new System.Windows.Forms.DataGridView();
            this.searchButton = new System.Windows.Forms.Button();
            this.fio = new System.Windows.Forms.TextBox();
            this.spec = new System.Windows.Forms.TextBox();
            this.cours = new System.Windows.Forms.TextBox();
            this.ball = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.searchGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // searchGrid
            // 
            this.searchGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.searchGrid.Location = new System.Drawing.Point(12, 277);
            this.searchGrid.Name = "searchGrid";
            this.searchGrid.RowTemplate.Height = 28;
            this.searchGrid.Size = new System.Drawing.Size(775, 332);
            this.searchGrid.TabIndex = 0;
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(349, 99);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(170, 74);
            this.searchButton.TabIndex = 2;
            this.searchButton.Text = "Поиск";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // fio
            // 
            this.fio.Location = new System.Drawing.Point(22, 35);
            this.fio.Name = "fio";
            this.fio.Size = new System.Drawing.Size(238, 26);
            this.fio.TabIndex = 3;
            // 
            // spec
            // 
            this.spec.Location = new System.Drawing.Point(22, 93);
            this.spec.Name = "spec";
            this.spec.Size = new System.Drawing.Size(238, 26);
            this.spec.TabIndex = 5;
            // 
            // cours
            // 
            this.cours.Location = new System.Drawing.Point(22, 152);
            this.cours.Name = "cours";
            this.cours.Size = new System.Drawing.Size(238, 26);
            this.cours.TabIndex = 6;
            // 
            // ball
            // 
            this.ball.Location = new System.Drawing.Point(22, 223);
            this.ball.Name = "ball";
            this.ball.Size = new System.Drawing.Size(238, 26);
            this.ball.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(22, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(263, 23);
            this.label1.TabIndex = 8;
            this.label1.Text = "ФИО";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(22, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(263, 23);
            this.label2.TabIndex = 9;
            this.label2.Text = "Специальность";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(22, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(263, 23);
            this.label3.TabIndex = 10;
            this.label3.Text = "Курс";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(22, 197);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(263, 23);
            this.label4.TabIndex = 11;
            this.label4.Text = "Балл";
            // 
            // SearchResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 620);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ball);
            this.Controls.Add(this.cours);
            this.Controls.Add(this.spec);
            this.Controls.Add(this.fio);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.searchGrid);
            this.Name = "SearchResultForm";
            this.Text = "SearchResultForm";
            ((System.ComponentModel.ISupportInitialize)(this.searchGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;

        private System.Windows.Forms.TextBox cours;
        private System.Windows.Forms.TextBox ball;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.TextBox spec;

        private System.Windows.Forms.TextBox fio;

        private System.Windows.Forms.Button searchButton;

        private System.Windows.Forms.DataGridView searchGrid;

        #endregion
    }
}