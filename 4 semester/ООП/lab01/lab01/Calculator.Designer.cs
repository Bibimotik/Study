namespace lab01
{
  partial class Calculator
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
      this.clear = new System.Windows.Forms.Button();
      this.firstnumber = new System.Windows.Forms.TextBox();
      this.secondnumber = new System.Windows.Forms.TextBox();
      this.result = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.save = new System.Windows.Forms.Button();
      this.extract = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.comboBox1 = new System.Windows.Forms.ComboBox();
      this.label4 = new System.Windows.Forms.Label();
      this.send = new System.Windows.Forms.Button();
      this.error1 = new System.Windows.Forms.Label();
      this.error2 = new System.Windows.Forms.Label();
      this.error3 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // clear
      // 
      this.clear.Location = new System.Drawing.Point(159, 307);
      this.clear.Name = "clear";
      this.clear.Size = new System.Drawing.Size(109, 47);
      this.clear.TabIndex = 5;
      this.clear.Text = "Clear";
      this.clear.UseVisualStyleBackColor = true;
      this.clear.Click += new System.EventHandler(this.clear_Click);
      // 
      // firstnumber
      // 
      this.firstnumber.Location = new System.Drawing.Point(27, 57);
      this.firstnumber.Name = "firstnumber";
      this.firstnumber.Size = new System.Drawing.Size(344, 26);
      this.firstnumber.TabIndex = 8;
      // 
      // secondnumber
      // 
      this.secondnumber.Location = new System.Drawing.Point(27, 197);
      this.secondnumber.Name = "secondnumber";
      this.secondnumber.Size = new System.Drawing.Size(344, 26);
      this.secondnumber.TabIndex = 9;
      // 
      // result
      // 
      this.result.Location = new System.Drawing.Point(27, 264);
      this.result.Name = "result";
      this.result.ReadOnly = true;
      this.result.Size = new System.Drawing.Size(344, 26);
      this.result.TabIndex = 10;
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(27, 168);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(123, 26);
      this.label2.TabIndex = 12;
      this.label2.Text = "Значение 2";
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(27, 235);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(123, 26);
      this.label3.TabIndex = 13;
      this.label3.Text = "Результат";
      // 
      // save
      // 
      this.save.Location = new System.Drawing.Point(392, 264);
      this.save.Name = "save";
      this.save.Size = new System.Drawing.Size(138, 37);
      this.save.TabIndex = 15;
      this.save.Text = "Сохранить";
      this.save.UseVisualStyleBackColor = true;
      this.save.Click += new System.EventHandler(this.save_Click);
      // 
      // extract
      // 
      this.extract.Location = new System.Drawing.Point(392, 307);
      this.extract.Name = "extract";
      this.extract.Size = new System.Drawing.Size(138, 34);
      this.extract.TabIndex = 16;
      this.extract.Text = "Извлечь";
      this.extract.UseVisualStyleBackColor = true;
      this.extract.Click += new System.EventHandler(this.extract_Click);
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(27, 28);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(123, 26);
      this.label1.TabIndex = 17;
      this.label1.Text = "Значение 1";
      // 
      // comboBox1
      // 
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Items.AddRange(new object[] { "+", "-", "*", "/", "//", "%" });
      this.comboBox1.Location = new System.Drawing.Point(27, 120);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new System.Drawing.Size(130, 28);
      this.comboBox1.TabIndex = 18;
      this.comboBox1.Text = "+";
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(27, 95);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(109, 22);
      this.label4.TabIndex = 19;
      this.label4.Text = "Операция";
      // 
      // send
      // 
      this.send.Location = new System.Drawing.Point(27, 307);
      this.send.Name = "send";
      this.send.Size = new System.Drawing.Size(109, 47);
      this.send.TabIndex = 7;
      this.send.Text = "=";
      this.send.UseVisualStyleBackColor = true;
      this.send.Click += new System.EventHandler(this.send_Click);
      // 
      // error1
      // 
      this.error1.Cursor = System.Windows.Forms.Cursors.Default;
      this.error1.ForeColor = System.Drawing.Color.Red;
      this.error1.Location = new System.Drawing.Point(146, 28);
      this.error1.Name = "error1";
      this.error1.Size = new System.Drawing.Size(225, 23);
      this.error1.TabIndex = 20;
      // 
      // error2
      // 
      this.error2.ForeColor = System.Drawing.Color.Red;
      this.error2.Location = new System.Drawing.Point(146, 168);
      this.error2.Name = "error2";
      this.error2.Size = new System.Drawing.Size(225, 23);
      this.error2.TabIndex = 21;
      // 
      // error3
      // 
      this.error3.Cursor = System.Windows.Forms.Cursors.Default;
      this.error3.ForeColor = System.Drawing.Color.Red;
      this.error3.Location = new System.Drawing.Point(172, 123);
      this.error3.Name = "error3";
      this.error3.Size = new System.Drawing.Size(199, 23);
      this.error3.TabIndex = 22;
      // 
      // Calculator
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.Control;
      this.ClientSize = new System.Drawing.Size(552, 398);
      this.Controls.Add(this.error3);
      this.Controls.Add(this.error2);
      this.Controls.Add(this.error1);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.comboBox1);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.extract);
      this.Controls.Add(this.save);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.result);
      this.Controls.Add(this.secondnumber);
      this.Controls.Add(this.firstnumber);
      this.Controls.Add(this.send);
      this.Controls.Add(this.clear);
      this.Location = new System.Drawing.Point(15, 15);
      this.Name = "Calculator";
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private System.Windows.Forms.Label error3;

    private System.Windows.Forms.ComboBox comboBox1;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Button send;
    private System.Windows.Forms.Label error1;
    private System.Windows.Forms.Label error2;

    private System.Windows.Forms.Label label1;

    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button save;
    private System.Windows.Forms.Button extract;

    private System.Windows.Forms.TextBox result;

    private System.Windows.Forms.TextBox firstnumber;
    private System.Windows.Forms.TextBox secondnumber;

    private System.Windows.Forms.Button clear;

    #endregion
  }
}

