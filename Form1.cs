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
using Zadanie_rekrutacyjne.DB;
using System.Data.Entity;

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
            MessageBox.Show($"Nazwa użytkownika: {repository.Owner.Login} " +
                $"\nData utworzenia: {repository.CreatedAt.ToLocalTime().ToString("dd/mm/yyyy hh/mm/ss")}" +
                $"\nOcena: {repository.StargazersCount}",
                "Informacje o repozytorium",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void LoadData()
        {
            using (WarehouseContext context = new WarehouseContext())
            {
                var orders = context.Orders.AsNoTracking()
                    .Include(_ => _.Client)
                    .Include(_ => _.OrderProducts.Select(__ => __.Product))
                    .ToList();

                foreach (var order in orders)
                {
                    listView1.Items.Add(new ListViewItem(new string[] { order.ClientId.ToString(), order.Date.ToString(), order.Id.ToString() }));
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
