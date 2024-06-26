using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;

namespace lab03
{
    public partial class Live : Form
    {
        static Adres adres = new Adres();

        public Live()
        {
            InitializeComponent();
        }
        
        IndexValidationAttribute attribute = new IndexValidationAttribute();

        private void saveAdres_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(city.Text) || string.IsNullOrWhiteSpace(index.Text) || string.IsNullOrWhiteSpace(street.Text) || string.IsNullOrWhiteSpace(house.Text) || string.IsNullOrWhiteSpace(flat.Text))
            {
                errorMessage.Text = "Заполните все поля.";
            }
            else if (!attribute.IsValid(index.Text))
            {
                errorMessage.Text = "Некорректный индекс. Индекс должен состоять из 6 цифр.";
            }
            else
            {
                errorMessage.Text = "";
                adres.City = city.Text;
                adres.Index = index.Text;
                adres.Flat = flat.Text;
                adres.Street = street.Text;
                adres.House = house.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        public static Adres GetAdres()
        {
            return adres;
        }
    }

    public class IndexValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string index = value as string;
            if (index != null && Regex.IsMatch(index, @"^\d{6}$"))
                return true;
            return false;
        }
    }
}