using MaskAndValidationKurbatovRKIS.DBcontext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaskAndValidationKurbatovRKIS
{
    public partial class FormAddUsers : Form
    {
        public FormAddUsers()
        {
            InitializeComponent();
        }
        Model1 model = new Model1();

        private void AddButton_Click(object sender, EventArgs e)
        {
            Regex reg = new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}$", RegexOptions.IgnoreCase);
            if (reg.IsMatch(emailTextBox.Text))
            {
                MessageBox.Show("Почта не соответствует требованиям!");
                return;
            }

            if (passwordTextBox.Text.Equals(passwordTextBox2.Text))
            {
                MessageBox.Show("Пароли не равны! ");
                return;
            }

            if (string.IsNullOrWhiteSpace(loginTextBox.Text) || 
                string.IsNullOrWhiteSpace(passwordTextBox.Text) ||
                string.IsNullOrWhiteSpace(firstNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(secondNameTextBox.Text) ||
                !maskedTextBox1.MaskCompleted)
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            User user = new User();

            user.ID = 0;
            user.Login = loginTextBox.Text;
            user.Password = passwordTextBox.Text;
            user.Email = emailTextBox.Text;
            user.Phone = maskedTextBox1.Text;
            user.First_Name = firstNameTextBox.Text;
            user.Second_Name = secondNameTextBox.Text;
            user.RoleID = (int)roleIDComboBox.SelectedValue;
            user.Gender = radioButtonMan.Checked ? "Мужской" : "Женский";

            try
            {
                model.Users.Add(user);
                model.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Данные добавлены!");
            Close();
        }

        private void FormAddUsers_Load(object sender, EventArgs e)
        {
            bindingSource1.DataSource = model.Roles.ToList();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
