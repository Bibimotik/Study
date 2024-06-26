namespace lab02
{
    partial class Person
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Person));
            this.firstName = new System.Windows.Forms.TextBox();
            this.labelFirst = new System.Windows.Forms.Label();
            this.labelSecond = new System.Windows.Forms.Label();
            this.secondName = new System.Windows.Forms.TextBox();
            this.labelThird = new System.Windows.Forms.Label();
            this.patronymic = new System.Windows.Forms.TextBox();
            this.labelSpec = new System.Windows.Forms.Label();
            this.labelBirthday = new System.Windows.Forms.Label();
            this.birthday = new System.Windows.Forms.DateTimePicker();
            this.save = new System.Windows.Forms.Button();
            this.spec = new System.Windows.Forms.ComboBox();
            this.man = new System.Windows.Forms.RadioButton();
            this.woman = new System.Windows.Forms.RadioButton();
            this.print = new System.Windows.Forms.Button();
            this.labelMark = new System.Windows.Forms.Label();
            this.age = new System.Windows.Forms.NumericUpDown();
            this.labelAge = new System.Windows.Forms.Label();
            this.labelCours = new System.Windows.Forms.Label();
            this.curs = new System.Windows.Forms.NumericUpDown();
            this.labelGroup = new System.Windows.Forms.Label();
            this.group = new System.Windows.Forms.TextBox();
            this.sexBox = new System.Windows.Forms.GroupBox();
            this.picture = new System.Windows.Forms.PictureBox();
            this.studentGrid = new System.Windows.Forms.DataGridView();
            this.@continue = new System.Windows.Forms.Button();
            this.errorMessage = new System.Windows.Forms.Label();
            this.mark = new System.Windows.Forms.NumericUpDown();
            this.labelAdres = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.labelJob = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.age)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.curs)).BeginInit();
            this.sexBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mark)).BeginInit();
            this.SuspendLayout();
            // 
            // firstName
            // 
            this.firstName.Location = new System.Drawing.Point(26, 48);
            this.firstName.MaxLength = 30;
            this.firstName.Name = "firstName";
            this.firstName.Size = new System.Drawing.Size(199, 26);
            this.firstName.TabIndex = 0;
            // 
            // labelFirst
            // 
            this.labelFirst.Location = new System.Drawing.Point(26, 22);
            this.labelFirst.Name = "labelFirst";
            this.labelFirst.Size = new System.Drawing.Size(100, 23);
            this.labelFirst.TabIndex = 1;
            this.labelFirst.Text = "Имя";
            // 
            // labelSecond
            // 
            this.labelSecond.Location = new System.Drawing.Point(26, 88);
            this.labelSecond.Name = "labelSecond";
            this.labelSecond.Size = new System.Drawing.Size(100, 23);
            this.labelSecond.TabIndex = 2;
            this.labelSecond.Text = "Фамилия";
            // 
            // secondName
            // 
            this.secondName.Location = new System.Drawing.Point(26, 114);
            this.secondName.MaxLength = 30;
            this.secondName.Name = "secondName";
            this.secondName.Size = new System.Drawing.Size(199, 26);
            this.secondName.TabIndex = 3;
            // 
            // labelThird
            // 
            this.labelThird.Location = new System.Drawing.Point(26, 151);
            this.labelThird.Name = "labelThird";
            this.labelThird.Size = new System.Drawing.Size(100, 23);
            this.labelThird.TabIndex = 4;
            this.labelThird.Text = "Отчество";
            // 
            // patronymic
            // 
            this.patronymic.Location = new System.Drawing.Point(26, 177);
            this.patronymic.MaxLength = 30;
            this.patronymic.Name = "patronymic";
            this.patronymic.Size = new System.Drawing.Size(199, 26);
            this.patronymic.TabIndex = 5;
            // 
            // labelSpec
            // 
            this.labelSpec.Location = new System.Drawing.Point(26, 215);
            this.labelSpec.Name = "labelSpec";
            this.labelSpec.Size = new System.Drawing.Size(136, 23);
            this.labelSpec.TabIndex = 6;
            this.labelSpec.Text = "Специальность";
            // 
            // labelBirthday
            // 
            this.labelBirthday.Location = new System.Drawing.Point(26, 283);
            this.labelBirthday.Name = "labelBirthday";
            this.labelBirthday.Size = new System.Drawing.Size(164, 23);
            this.labelBirthday.TabIndex = 8;
            this.labelBirthday.Text = "Дата рождения";
            // 
            // birthday
            // 
            this.birthday.Location = new System.Drawing.Point(26, 309);
            this.birthday.Name = "birthday";
            this.birthday.Size = new System.Drawing.Size(312, 26);
            this.birthday.TabIndex = 10;
            this.birthday.ValueChanged += new System.EventHandler(this.Birthday_ValueChanged);
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(547, 319);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(123, 37);
            this.save.TabIndex = 11;
            this.save.Text = "Сохранить";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // spec
            // 
            this.spec.FormattingEnabled = true;
            this.spec.Items.AddRange(new object[] { "ИСиТ", "ПОИТ", "ПОИБМС", "ДЭВИ" });
            this.spec.Location = new System.Drawing.Point(26, 241);
            this.spec.Name = "spec";
            this.spec.Size = new System.Drawing.Size(199, 28);
            this.spec.TabIndex = 13;
            // 
            // man
            // 
            this.man.Location = new System.Drawing.Point(34, 38);
            this.man.Name = "man";
            this.man.Size = new System.Drawing.Size(54, 24);
            this.man.TabIndex = 14;
            this.man.TabStop = true;
            this.man.Text = "м";
            this.man.UseVisualStyleBackColor = true;
            // 
            // woman
            // 
            this.woman.Location = new System.Drawing.Point(116, 38);
            this.woman.Name = "woman";
            this.woman.Size = new System.Drawing.Size(54, 24);
            this.woman.TabIndex = 15;
            this.woman.TabStop = true;
            this.woman.Text = "w";
            this.woman.UseVisualStyleBackColor = true;
            // 
            // print
            // 
            this.print.Location = new System.Drawing.Point(679, 319);
            this.print.Name = "print";
            this.print.Size = new System.Drawing.Size(123, 37);
            this.print.TabIndex = 17;
            this.print.Text = "Отобразить";
            this.print.UseVisualStyleBackColor = true;
            this.print.Click += new System.EventHandler(this.print_Click_1);
            // 
            // labelMark
            // 
            this.labelMark.Location = new System.Drawing.Point(260, 151);
            this.labelMark.Name = "labelMark";
            this.labelMark.Size = new System.Drawing.Size(140, 23);
            this.labelMark.TabIndex = 18;
            this.labelMark.Text = "Средняя оценка";
            // 
            // age
            // 
            this.age.Location = new System.Drawing.Point(386, 309);
            this.age.Name = "age";
            this.age.ReadOnly = true;
            this.age.Size = new System.Drawing.Size(120, 26);
            this.age.TabIndex = 20;
            // 
            // labelAge
            // 
            this.labelAge.Location = new System.Drawing.Point(386, 283);
            this.labelAge.Name = "labelAge";
            this.labelAge.Size = new System.Drawing.Size(100, 23);
            this.labelAge.TabIndex = 21;
            this.labelAge.Text = "Возраст";
            // 
            // labelCours
            // 
            this.labelCours.Location = new System.Drawing.Point(260, 22);
            this.labelCours.Name = "labelCours";
            this.labelCours.Size = new System.Drawing.Size(100, 23);
            this.labelCours.TabIndex = 22;
            this.labelCours.Text = "Курс";
            // 
            // curs
            // 
            this.curs.Location = new System.Drawing.Point(260, 49);
            this.curs.Maximum = new decimal(new int[] { 4, 0, 0, 0 });
            this.curs.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.curs.Name = "curs";
            this.curs.Size = new System.Drawing.Size(120, 26);
            this.curs.TabIndex = 23;
            this.curs.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // labelGroup
            // 
            this.labelGroup.Location = new System.Drawing.Point(260, 88);
            this.labelGroup.Name = "labelGroup";
            this.labelGroup.Size = new System.Drawing.Size(100, 23);
            this.labelGroup.TabIndex = 24;
            this.labelGroup.Text = "Группа";
            // 
            // group
            // 
            this.group.Location = new System.Drawing.Point(260, 114);
            this.group.MaxLength = 2;
            this.group.Name = "group";
            this.group.Size = new System.Drawing.Size(120, 26);
            this.group.TabIndex = 25;
            // 
            // sexBox
            // 
            this.sexBox.Controls.Add(this.man);
            this.sexBox.Controls.Add(this.woman);
            this.sexBox.Location = new System.Drawing.Point(26, 341);
            this.sexBox.Name = "sexBox";
            this.sexBox.Size = new System.Drawing.Size(200, 74);
            this.sexBox.TabIndex = 28;
            this.sexBox.TabStop = false;
            this.sexBox.Text = "Пол";
            // 
            // picture
            // 
            this.picture.Image = ((System.Drawing.Image)(resources.GetObject("picture.Image")));
            this.picture.Location = new System.Drawing.Point(26, 430);
            this.picture.Name = "picture";
            this.picture.Size = new System.Drawing.Size(292, 106);
            this.picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picture.TabIndex = 31;
            this.picture.TabStop = false;
            // 
            // studentGrid
            // 
            this.studentGrid.AllowUserToDeleteRows = false;
            this.studentGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.studentGrid.Location = new System.Drawing.Point(324, 375);
            this.studentGrid.Name = "studentGrid";
            this.studentGrid.ReadOnly = true;
            this.studentGrid.RowTemplate.Height = 28;
            this.studentGrid.Size = new System.Drawing.Size(753, 200);
            this.studentGrid.TabIndex = 32;
            // 
            // continue
            // 
            this.@continue.Location = new System.Drawing.Point(418, 37);
            this.@continue.Name = "continue";
            this.@continue.Size = new System.Drawing.Size(123, 37);
            this.@continue.TabIndex = 33;
            this.@continue.Text = "Адрес";
            this.@continue.UseVisualStyleBackColor = true;
            this.@continue.Click += new System.EventHandler(this.adres_Click);
            // 
            // errorMessage
            // 
            this.errorMessage.ForeColor = System.Drawing.Color.Red;
            this.errorMessage.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.errorMessage.Location = new System.Drawing.Point(26, 539);
            this.errorMessage.Name = "errorMessage";
            this.errorMessage.Size = new System.Drawing.Size(294, 39);
            this.errorMessage.TabIndex = 34;
            this.errorMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // mark
            // 
            this.mark.DecimalPlaces = 2;
            this.mark.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            this.mark.Location = new System.Drawing.Point(260, 178);
            this.mark.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            this.mark.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            this.mark.Name = "mark";
            this.mark.Size = new System.Drawing.Size(120, 26);
            this.mark.TabIndex = 35;
            this.mark.Value = new decimal(new int[] { 1, 0, 0, 65536 });
            // 
            // labelAdres
            // 
            this.labelAdres.BackColor = System.Drawing.Color.White;
            this.labelAdres.Location = new System.Drawing.Point(547, 37);
            this.labelAdres.Name = "labelAdres";
            this.labelAdres.Size = new System.Drawing.Size(255, 199);
            this.labelAdres.TabIndex = 36;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(823, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 37);
            this.button1.TabIndex = 37;
            this.button1.Text = "Работа";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.job_Click);
            // 
            // labelJob
            // 
            this.labelJob.BackColor = System.Drawing.Color.White;
            this.labelJob.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelJob.Location = new System.Drawing.Point(930, 35);
            this.labelJob.Name = "labelJob";
            this.labelJob.Size = new System.Drawing.Size(147, 203);
            this.labelJob.TabIndex = 38;
            // 
            // Person
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1083, 587);
            this.Controls.Add(this.labelJob);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelAdres);
            this.Controls.Add(this.mark);
            this.Controls.Add(this.errorMessage);
            this.Controls.Add(this.@continue);
            this.Controls.Add(this.studentGrid);
            this.Controls.Add(this.picture);
            this.Controls.Add(this.sexBox);
            this.Controls.Add(this.age);
            this.Controls.Add(this.group);
            this.Controls.Add(this.labelGroup);
            this.Controls.Add(this.curs);
            this.Controls.Add(this.labelCours);
            this.Controls.Add(this.labelAge);
            this.Controls.Add(this.labelMark);
            this.Controls.Add(this.print);
            this.Controls.Add(this.spec);
            this.Controls.Add(this.save);
            this.Controls.Add(this.birthday);
            this.Controls.Add(this.labelBirthday);
            this.Controls.Add(this.labelSpec);
            this.Controls.Add(this.patronymic);
            this.Controls.Add(this.labelThird);
            this.Controls.Add(this.secondName);
            this.Controls.Add(this.labelSecond);
            this.Controls.Add(this.labelFirst);
            this.Controls.Add(this.firstName);
            this.Name = "Person";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.age)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.curs)).EndInit();
            this.sexBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mark)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelJob;

        private System.Windows.Forms.Label labelAdres;

        private System.Windows.Forms.NumericUpDown mark;

        private System.Windows.Forms.Label errorMessage;

        private System.Windows.Forms.Button @continue;

        private System.Windows.Forms.DataGridView studentGrid;

        private System.Windows.Forms.PictureBox picture;

        private System.Windows.Forms.GroupBox sexBox;

        private System.Windows.Forms.Label labelGroup;
        private System.Windows.Forms.TextBox group;

        private System.Windows.Forms.Label labelCours;
        private System.Windows.Forms.NumericUpDown curs;

        private System.Windows.Forms.NumericUpDown age;
        private System.Windows.Forms.Label labelAge;

        private System.Windows.Forms.Label labelMark;

        private System.Windows.Forms.Button print;

        private System.Windows.Forms.RadioButton man;
        private System.Windows.Forms.RadioButton woman;

        private System.Windows.Forms.ComboBox spec;

        private System.Windows.Forms.Button save;

        private System.Windows.Forms.DateTimePicker birthday;

        private System.Windows.Forms.Label labelBirthday;

        private System.Windows.Forms.Label labelSpec;

        private System.Windows.Forms.TextBox firstName;
        private System.Windows.Forms.Label labelFirst;
        private System.Windows.Forms.Label labelSecond;
        private System.Windows.Forms.TextBox secondName;
        private System.Windows.Forms.Label labelThird;
        private System.Windows.Forms.TextBox patronymic;

        #endregion
    }
}