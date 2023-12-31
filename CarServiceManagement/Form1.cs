﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Firestore;

namespace CarServiceManagement
{
    public partial class Login : Form
    {

        private FirestoreDb db;
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"service-account.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            db = FirestoreDb.Create("fyp-car-service-management");
        }

        private async void login_btn_Click(object sender, EventArgs e)
        {
            try
            {
                this.loading_lb.Visible = true;

                string email = this.email_tb.Text;
                string password = this.password_tb.Text;

                if(email.Length == 0)
                {
                    throw new Exception("Va rugam introduceti email-ul");
                }

                if (password.Length == 0)
                {
                    throw new Exception("Va rugam introduceti parola");
                }

                Query userQuery = db.Collection("users").WhereEqualTo("email", email);
                QuerySnapshot userQuerySnapshot = await userQuery.GetSnapshotAsync();
                int documentCount = userQuerySnapshot.Documents.Count;

                if(documentCount == 0)
                {
                    throw new Exception("Utilizatorul nu exista");
                }

                DocumentSnapshot doc = userQuerySnapshot.Documents[0];
                Dictionary<string, object> user = doc.ToDictionary();

                Console.WriteLine(user["password"].ToString());
                Console.WriteLine(password);
                Console.WriteLine(user["password"].ToString() != password);

                if (user["password"].ToString() != password) {
                    throw new Exception("Parola nu este corecta");
                }

                Acasa home = new Acasa();
                home.Show();
                this.Hide();
                
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }

            this.loading_lb.Visible = false;
        }

        private void email_tb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
