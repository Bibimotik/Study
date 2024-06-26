using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace lab03
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
            this.labelCours = new System.Windows.Forms.Label();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripComboBox2 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripTextBox3 = new System.Windows.Forms.ToolStripTextBox();
            this.miniToolStrip = new System.Windows.Forms.MenuStrip();
            this.searchButton = new System.Windows.Forms.ToolStripTextBox();
            this.sortButton = new System.Windows.Forms.ToolStripTextBox();
            this.saveMenu = new System.Windows.Forms.ToolStripTextBox();
            this.programMenu = new System.Windows.Forms.ToolStripTextBox();
            InitializeToolbar();
            LoadStudentData();
            ((System.ComponentModel.ISupportInitialize)(this.age)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.curs)).BeginInit();
            this.sexBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mark)).BeginInit();
            this.miniToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // firstName
            // 
            this.firstName.Location = new System.Drawing.Point(11, 123);
            this.firstName.MaxLength = 30;
            this.firstName.Name = "firstName";
            this.firstName.Size = new System.Drawing.Size(199, 26);
            this.firstName.TabIndex = 0;
            // 
            // labelFirst
            // 
            this.labelFirst.Location = new System.Drawing.Point(11, 97);
            this.labelFirst.Name = "labelFirst";
            this.labelFirst.Size = new System.Drawing.Size(100, 23);
            this.labelFirst.TabIndex = 1;
            this.labelFirst.Text = "Имя";
            // 
            // labelSecond
            // 
            this.labelSecond.Location = new System.Drawing.Point(11, 163);
            this.labelSecond.Name = "labelSecond";
            this.labelSecond.Size = new System.Drawing.Size(100, 23);
            this.labelSecond.TabIndex = 2;
            this.labelSecond.Text = "Фамилия";
            // 
            // secondName
            // 
            this.secondName.Location = new System.Drawing.Point(11, 189);
            this.secondName.MaxLength = 30;
            this.secondName.Name = "secondName";
            this.secondName.Size = new System.Drawing.Size(199, 26);
            this.secondName.TabIndex = 3;
            // 
            // labelThird
            // 
            this.labelThird.Location = new System.Drawing.Point(11, 226);
            this.labelThird.Name = "labelThird";
            this.labelThird.Size = new System.Drawing.Size(100, 23);
            this.labelThird.TabIndex = 4;
            this.labelThird.Text = "Отчество";
            // 
            // patronymic
            // 
            this.patronymic.Location = new System.Drawing.Point(11, 252);
            this.patronymic.MaxLength = 30;
            this.patronymic.Name = "patronymic";
            this.patronymic.Size = new System.Drawing.Size(199, 26);
            this.patronymic.TabIndex = 5;
            // 
            // labelSpec
            // 
            this.labelSpec.Location = new System.Drawing.Point(11, 290);
            this.labelSpec.Name = "labelSpec";
            this.labelSpec.Size = new System.Drawing.Size(136, 23);
            this.labelSpec.TabIndex = 6;
            this.labelSpec.Text = "Специальность";
            // 
            // labelBirthday
            // 
            this.labelBirthday.Location = new System.Drawing.Point(11, 358);
            this.labelBirthday.Name = "labelBirthday";
            this.labelBirthday.Size = new System.Drawing.Size(164, 23);
            this.labelBirthday.TabIndex = 8;
            this.labelBirthday.Text = "Дата рождения";
            // 
            // birthday
            // 
            this.birthday.Location = new System.Drawing.Point(11, 384);
            this.birthday.MaxDate = new System.DateTime(2024, 3, 6, 0, 0, 0, 0);
            this.birthday.Name = "birthday";
            this.birthday.Size = new System.Drawing.Size(312, 26);
            this.birthday.TabIndex = 10;
            this.birthday.Value = new System.DateTime(2024, 3, 6, 0, 0, 0, 0);
            this.birthday.ValueChanged += new System.EventHandler(this.Birthday_ValueChanged);
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(532, 394);
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
            this.spec.Location = new System.Drawing.Point(11, 316);
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
            this.print.Location = new System.Drawing.Point(664, 394);
            this.print.Name = "print";
            this.print.Size = new System.Drawing.Size(123, 37);
            this.print.TabIndex = 17;
            this.print.Text = "Отобразить";
            this.print.UseVisualStyleBackColor = true;
            this.print.Click += new System.EventHandler(this.print_Click_1);
            // 
            // labelMark
            // 
            this.labelMark.Location = new System.Drawing.Point(245, 226);
            this.labelMark.Name = "labelMark";
            this.labelMark.Size = new System.Drawing.Size(140, 23);
            this.labelMark.TabIndex = 18;
            this.labelMark.Text = "Средняя оценка";
            // 
            // age
            // 
            this.age.Location = new System.Drawing.Point(371, 384);
            this.age.Name = "age";
            this.age.ReadOnly = true;
            this.age.Size = new System.Drawing.Size(120, 26);
            this.age.TabIndex = 20;
            // 
            // labelAge
            // 
            this.labelAge.Location = new System.Drawing.Point(371, 358);
            this.labelAge.Name = "labelAge";
            this.labelAge.Size = new System.Drawing.Size(100, 23);
            this.labelAge.TabIndex = 21;
            this.labelAge.Text = "Возраст";
            // 
            // curs
            // 
            this.curs.Location = new System.Drawing.Point(245, 124);
            this.curs.Maximum = new decimal(new int[] { 4, 0, 0, 0 });
            this.curs.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.curs.Name = "curs";
            this.curs.Size = new System.Drawing.Size(120, 26);
            this.curs.TabIndex = 23;
            this.curs.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // labelGroup
            // 
            this.labelGroup.Location = new System.Drawing.Point(245, 163);
            this.labelGroup.Name = "labelGroup";
            this.labelGroup.Size = new System.Drawing.Size(100, 23);
            this.labelGroup.TabIndex = 24;
            this.labelGroup.Text = "Группа";
            // 
            // group
            // 
            this.group.Location = new System.Drawing.Point(245, 189);
            this.group.MaxLength = 2;
            this.group.Name = "group";
            this.group.Size = new System.Drawing.Size(120, 26);
            this.group.TabIndex = 25;
            // 
            // sexBox
            // 
            this.sexBox.Controls.Add(this.man);
            this.sexBox.Controls.Add(this.woman);
            this.sexBox.Location = new System.Drawing.Point(11, 416);
            this.sexBox.Name = "sexBox";
            this.sexBox.Size = new System.Drawing.Size(200, 74);
            this.sexBox.TabIndex = 28;
            this.sexBox.TabStop = false;
            this.sexBox.Text = "Пол";
            // 
            // picture
            // 
            this.picture.Image = ((System.Drawing.Image)(resources.GetObject("picture.Image")));
            this.picture.Location = new System.Drawing.Point(11, 492);
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
            this.studentGrid.Location = new System.Drawing.Point(309, 455);
            this.studentGrid.Name = "studentGrid";
            this.studentGrid.ReadOnly = true;
            this.studentGrid.RowTemplate.Height = 28;
            this.studentGrid.Size = new System.Drawing.Size(753, 200);
            this.studentGrid.TabIndex = 32;
            // 
            // continue
            // 
            this.@continue.Location = new System.Drawing.Point(403, 112);
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
            this.errorMessage.Location = new System.Drawing.Point(9, 616);
            this.errorMessage.Name = "errorMessage";
            this.errorMessage.Size = new System.Drawing.Size(294, 39);
            this.errorMessage.TabIndex = 34;
            this.errorMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // mark
            // 
            this.mark.DecimalPlaces = 2;
            this.mark.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            this.mark.Location = new System.Drawing.Point(245, 253);
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
            this.labelAdres.Location = new System.Drawing.Point(532, 112);
            this.labelAdres.Name = "labelAdres";
            this.labelAdres.Size = new System.Drawing.Size(255, 199);
            this.labelAdres.TabIndex = 36;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(808, 112);
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
            this.labelJob.Location = new System.Drawing.Point(915, 110);
            this.labelJob.Name = "labelJob";
            this.labelJob.Size = new System.Drawing.Size(147, 203);
            this.labelJob.TabIndex = 38;
            // 
            // labelCours
            // 
            this.labelCours.Location = new System.Drawing.Point(245, 97);
            this.labelCours.Name = "labelCours";
            this.labelCours.Size = new System.Drawing.Size(100, 23);
            this.labelCours.TabIndex = 22;
            this.labelCours.Text = "Курс";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(150, 33);
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 33);
            this.toolStripComboBox1.Text = "Поиск по";
            // 
            // toolStripComboBox2
            // 
            this.toolStripComboBox2.Name = "toolStripComboBox2";
            this.toolStripComboBox2.Size = new System.Drawing.Size(170, 33);
            this.toolStripComboBox2.Text = "Сортировка по";
            // 
            // toolStripTextBox2
            // 
            this.toolStripTextBox2.Name = "toolStripTextBox2";
            this.toolStripTextBox2.ReadOnly = true;
            this.toolStripTextBox2.Size = new System.Drawing.Size(100, 33);
            this.toolStripTextBox2.Text = "Сохранить";
            // 
            // toolStripTextBox3
            // 
            this.toolStripTextBox3.Name = "toolStripTextBox3";
            this.toolStripTextBox3.ReadOnly = true;
            this.toolStripTextBox3.Size = new System.Drawing.Size(125, 33);
            this.toolStripTextBox3.Text = "О программе";
            // 
            // miniToolStrip
            // 
            this.miniToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.searchButton, this.sortButton, this.saveMenu, this.programMenu });
            this.miniToolStrip.Location = new System.Drawing.Point(0, 0);
            this.miniToolStrip.Name = "miniToolStrip";
            this.miniToolStrip.Size = new System.Drawing.Size(1083, 35);
            this.miniToolStrip.TabIndex = 41;
            this.miniToolStrip.Text = "menuStrip1";
            // 
            // searchButton
            // 
            this.searchButton.Name = "searchButton";
            this.searchButton.ReadOnly = true;
            this.searchButton.Size = new System.Drawing.Size(100, 31);
            this.searchButton.Text = "Поиск";
            this.searchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // sortButton
            // 
            this.sortButton.Name = "sortButton";
            this.sortButton.ReadOnly = true;
            this.sortButton.Size = new System.Drawing.Size(125, 31);
            this.sortButton.Text = "Сортировать";
            this.sortButton.Click += new System.EventHandler(this.sortButton_Click);
            // 
            // saveMenu
            // 
            this.saveMenu.Name = "saveMenu";
            this.saveMenu.ReadOnly = true;
            this.saveMenu.Size = new System.Drawing.Size(100, 31);
            this.saveMenu.Text = "Сохранить";
            this.saveMenu.Click += new System.EventHandler(this.saveMenu_Click);
            // 
            // programMenu
            // 
            this.programMenu.Name = "programMenu";
            this.programMenu.ReadOnly = true;
            this.programMenu.Size = new System.Drawing.Size(125, 31);
            this.programMenu.Text = "О программе";
            this.programMenu.Click += new System.EventHandler(this.programMenu_Click);
            // 
            // Person
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1083, 707);
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
            this.Controls.Add(this.miniToolStrip);
            this.MainMenuStrip = this.miniToolStrip;
            this.Name = "Person";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.age)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.curs)).EndInit();
            this.sexBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mark)).EndInit();
            this.miniToolStrip.ResumeLayout(false);
            this.miniToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private void InitializeToolbar()
        {
            toolStrip = new ToolStrip();
            toolStrip.Dock = DockStyle.Top;
            toolStrip.Visible = true;

            searchButtonI = new ToolStripButton("Поиск");
            searchButtonI.Click += SearchButton_Click;
            toolStrip.Items.Add(searchButtonI);

            sortButtonI = new ToolStripButton("Сортировка");
            sortButtonI.Click += sortButton_Click;
            toolStrip.Items.Add(sortButtonI);

            clearButtonI = new ToolStripButton("Очистить");
            clearButtonI.Click += ClearButton_Click;
            toolStrip.Items.Add(clearButtonI);

            deleteButtonI = new ToolStripButton("Удалить");
            deleteButtonI.Click += DeleteButton_Click;
            toolStrip.Items.Add(deleteButtonI);

            forwardButtonI = new ToolStripButton("Вперед");
            forwardButtonI.Click += ForwardButton_Click;
            toolStrip.Items.Add(forwardButtonI);

            backButtonI = new ToolStripButton("Назад");
            backButtonI.Click += BackButton_Click;
            toolStrip.Items.Add(backButtonI);

            toggleToolbarButtonI = new ToolStripButton("Скрыть");
            toggleToolbarButtonI.Click += ToggleToolbarButton_Click;
            toolStrip.Items.Add(toggleToolbarButtonI);

            toggleToolbarButtonI2 = new ToolStripButton("Показать");
            toggleToolbarButtonI2.Click += ShowToolbarButton_Click;
            toggleToolbarButtonI2.Visible = false;
            toolStrip.Items.Add(toggleToolbarButtonI2);
            
            toggleToolbarButtonI3 = new ToolStripButton("Скрыть совсем");
            toggleToolbarButtonI3.Click += HideToolbarButton_Click;
            toolStrip.Items.Add(toggleToolbarButtonI3);
            
            Controls.Add(toolStrip);
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            this.firstName.Text = string.Empty;
            this.secondName.Text = string.Empty;
            this.patronymic.Text = string.Empty;
            this.spec.SelectedIndex = -1;
            this.man.Checked = false;
            this.woman.Checked = false;
            this.age.Value = this.age.Minimum;
            this.curs.Value = this.curs.Minimum;
            this.group.Text = string.Empty;
            this.mark.Value = this.mark.Minimum;
            this.birthday.Value = this.birthday.MaxDate;

            this.sexBox.Controls.Clear();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            File.WriteAllText("student.json", string.Empty);
        }
        private List<Student> studentList;
        private int currentStudentIndex = -1;
        private void LoadStudentData()
        {
            try
            {
                string json = File.ReadAllText("student.json");
                studentList = JsonConvert.DeserializeObject<List<Student>>(json);
                currentStudentIndex = 0;
                DisplayCurrentStudent();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных студентов: " + ex.Message);
            }
        }

        private void DisplayCurrentStudent()
        {
            if (currentStudentIndex >= 0 && currentStudentIndex < studentList.Count)
            {
                Student currentStudent = studentList[currentStudentIndex];
                studentGrid.DataSource = new List<Student> { currentStudent };
            }
        }

        private void ForwardButton_Click(object sender, EventArgs e)
        {
            if (currentStudentIndex < studentList.Count - 1)
            {
                currentStudentIndex++;
                DisplayCurrentStudent();
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            if (currentStudentIndex > 0)
            {
                currentStudentIndex--;
                DisplayCurrentStudent();
            }
        }

        private void ToggleToolbarButton_Click(object sender, EventArgs e)
        {
            searchButtonI.Visible = false;
            sortButtonI.Visible = false;
            clearButtonI.Visible = false;
            deleteButtonI.Visible = false;
            forwardButtonI.Visible = false;
            backButtonI.Visible = false;
            toggleToolbarButtonI.Visible = false;
            toggleToolbarButtonI3.Visible = false;
            toggleToolbarButtonI2.Visible = true;
        }
        
        private void ShowToolbarButton_Click(object sender, EventArgs e)
        {
            searchButtonI.Visible = true;
            sortButtonI.Visible = true;
            clearButtonI.Visible = true;
            deleteButtonI.Visible = true;
            forwardButtonI.Visible = true;
            backButtonI.Visible = true;
            toggleToolbarButtonI.Visible = true;
            toggleToolbarButtonI3.Visible = true;
            toggleToolbarButtonI2.Visible = false;
        }
        private void HideToolbarButton_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = false;
        }

        private ToolStrip toolStrip;
        private ToolStripButton searchButtonI;
        private ToolStripButton sortButtonI;
        private ToolStripButton clearButtonI;
        private ToolStripButton deleteButtonI;
        private ToolStripButton forwardButtonI;
        private ToolStripButton backButtonI;
        private ToolStripButton toggleToolbarButtonI;
        private ToolStripButton toggleToolbarButtonI2;
        private ToolStripButton toggleToolbarButtonI3;
        
        private System.Windows.Forms.ToolStripTextBox sortButton;

        private System.Windows.Forms.ToolStripTextBox searchButton;

        private System.Windows.Forms.ToolStripTextBox programMenu;

        private System.Windows.Forms.ToolStripTextBox saveMenu;

        private System.Windows.Forms.MenuStrip miniToolStrip;

        private System.Windows.Forms.ToolStripTextBox toolStripTextBox3;

        private System.Windows.Forms.ToolStripTextBox toolStripTextBox2;

        private System.Windows.Forms.ToolStripComboBox toolStripComboBox2;

        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;

        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;

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