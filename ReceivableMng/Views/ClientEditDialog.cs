using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ReceivableMng.Views
{
    public partial class ClientEditDialog : Form
    {
        public string ClientName => txtName.Text.Trim();
        public string ClientEmail => txtEmail.Text.Trim();

        public ClientEditDialog(string name, string email)
        {
            InitializeComponent();
            
            txtName.Text = name;
            txtEmail.Text = email;

            btnOk.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("업체명을 입력해주세요");
                    return;
                }
                DialogResult = DialogResult.OK;
            };

            AcceptButton = btnOk;
        }
    }
}
