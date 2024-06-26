using System.ComponentModel;

namespace lab03
{
    partial class SortedResultForm
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
            this.sortedGrid = new System.Windows.Forms.DataGridView();
            this.sortedComboBox = new System.Windows.Forms.ComboBox();
            this.sortedButton = new System.Windows.Forms.Button();
            this.errorLabel = new System.Windows.Forms.Label();
            this.allData = new System.Windows.Forms.RadioButton();
            this.searchData = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.sortedGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // sortedGrid
            // 
            this.sortedGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sortedGrid.Location = new System.Drawing.Point(12, 111);
            this.sortedGrid.Name = "sortedGrid";
            this.sortedGrid.RowTemplate.Height = 28;
            this.sortedGrid.Size = new System.Drawing.Size(774, 325);
            this.sortedGrid.TabIndex = 0;
            // 
            // sortedComboBox
            // 
            this.sortedComboBox.FormattingEnabled = true;
            this.sortedComboBox.Items.AddRange(new object[] { "курс", "группа", "год", "специальность", "фамилия", "стаж" });
            this.sortedComboBox.Location = new System.Drawing.Point(244, 37);
            this.sortedComboBox.Name = "sortedComboBox";
            this.sortedComboBox.Size = new System.Drawing.Size(202, 28);
            this.sortedComboBox.TabIndex = 1;
            this.sortedComboBox.Text = "Сортировка по";
            // 
            // sortedButton
            // 
            this.sortedButton.Location = new System.Drawing.Point(452, 37);
            this.sortedButton.Name = "sortedButton";
            this.sortedButton.Size = new System.Drawing.Size(177, 28);
            this.sortedButton.TabIndex = 3;
            this.sortedButton.Text = "Сортировка";
            this.sortedButton.UseVisualStyleBackColor = true;
            this.sortedButton.Click += new System.EventHandler(this.sortButton_Click);
            // 
            // errorLabel
            // 
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(244, 78);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(348, 30);
            this.errorLabel.TabIndex = 5;
            this.errorLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // allData
            // 
            this.allData.Location = new System.Drawing.Point(37, 37);
            this.allData.Name = "allData";
            this.allData.Size = new System.Drawing.Size(173, 24);
            this.allData.TabIndex = 0;
            this.allData.TabStop = true;
            this.allData.Text = "все данные";
            this.allData.UseVisualStyleBackColor = true;
            // 
            // searchData
            // 
            this.searchData.Location = new System.Drawing.Point(37, 70);
            this.searchData.Name = "searchData";
            this.searchData.Size = new System.Drawing.Size(173, 24);
            this.searchData.TabIndex = 1;
            this.searchData.TabStop = true;
            this.searchData.Text = "данные поиска";
            this.searchData.UseVisualStyleBackColor = true;
            // 
            // SortedResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.searchData);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.allData);
            this.Controls.Add(this.sortedButton);
            this.Controls.Add(this.sortedComboBox);
            this.Controls.Add(this.sortedGrid);
            this.Name = "SortedResultForm";
            this.Text = "SortedResultForm";
            ((System.ComponentModel.ISupportInitialize)(this.sortedGrid)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label errorLabel;

        private System.Windows.Forms.ComboBox sortedComboBox;
        private System.Windows.Forms.Button sortedButton;
        private System.Windows.Forms.RadioButton allData;
        private System.Windows.Forms.RadioButton searchData;

        private System.Windows.Forms.DataGridView sortedGrid;

        #endregion
    }
}