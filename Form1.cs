using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Octokit;

namespace Zadanie_rekrutacyjne
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var client = new GitHubClient(new ProductHeaderValue("Test"));
            var repository = client.Repository.Get("karolornoch", "Zadanie_rekrutacyjne").Result;
            MessageBox.Show($"Nazwa użytkownika: {repository.Owner.Login} \nData utworzenia: {repository.CreatedAt.ToLocalTime().ToString()}\nOcena: {repository.StargazersCount}", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);           
        }
    }
}
