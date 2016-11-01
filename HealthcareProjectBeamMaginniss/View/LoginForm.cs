﻿using System;
using System.Windows.Forms;
using HealthcareProjectBeamMaginniss.DAL.Controller;
using HealthcareProjectBeamMaginniss.DAL.Repository;

namespace HealthcareProjectBeamMaginniss.View
{
    public partial class LoginForm : Form
    {
        #region Data members

        private readonly LoginController login;

        #endregion

        #region Constructors

        public LoginForm()
        {
            this.InitializeComponent();
            this.login = new LoginController();
        }

        #endregion

        private void attemptLogin()
        {
            var username = this.usernameTextBox.Text;
            var password = this.passwordTextBox.Text;
            this.passwordTextBox.Text = "";

            if (this.login.CheckLogin(username, password))
            {
                this.successfulLogin();
            }
            else
            {
                this.wrongLoginLabel.Visible = true;
            }
        }

        private void successfulLogin()
        {
            var mainform = new MainForm();
            mainform.Show();
            this.Hide();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            this.attemptLogin();
        }

        private void passwordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.attemptLogin();
            }
        }

        private void usernameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.attemptLogin();
            }
        }
    }
}