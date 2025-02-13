﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controladora;
using Modelo;


namespace Vista
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '•';
        }

        private void chckPW_CheckedChanged(object sender, EventArgs e)
        {
            if (chckPW.Checked == true)
            {
                txtPassword.PasswordChar = default;
            }
            else
            {
                txtPassword.PasswordChar = '•';
                
            }
        }

        private void picboxShowPW_Click(object sender, EventArgs e)
        {
            picboxShowPW.ImageLocation = "Img/ShowPW.png";
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            var listUsers = ControladoraUsuarios.get_instance().list_users();
            Usuario user = listUsers.Find(u => (u.Name == txtUser.Text || u.Email == txtUser.Text) && u.Password == txtPassword.Text);

            if(user != null)
            {
                Users users = new Users();
                users.Show();
            }
            else 
            { 
                MessageBox.Show("Credenciales invalidas"); 
            }          
            
        }

        private void SignUp_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
