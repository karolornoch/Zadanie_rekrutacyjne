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
            listView1.Items.Clear();
            using (var context = new WarehouseContext())
            {
                var orders = context.Orders.AsNoTracking()
                    .Include(_ => _.Client)
                    .Include(_ => _.OrderProducts.Select(__ => __.Product))
                    .Select(_ =>
                    new
                    {
                        _.ClientId,
                        _.Date,
                        _.Id,
                        NetValue = _.OrderProducts.Sum(__ => (__.Product.NetValue * __.Count)),
                        GrossValue = _.OrderProducts.Sum(__ => (__.Product.GrossValue * __.Count))
                    })
                    .ToList();

                foreach (var order in orders)
                {
                    var item = new ListViewItem(new string[] { order.ClientId.ToString(), order.Date.ToString(), order.Id.ToString(), order.NetValue.ToString(), order.GrossValue.ToString() })
                    {
                        Tag = order.Id
                    };
                    listView1.Items.Add(item);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void RefreshList()
        {
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
            form.MainForm = this;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                var id = (int)listView1.SelectedItems[0].Tag;
                Form2 form = new Form2(id);
                form.Show();
                form.MainForm = this;
            }else
                MessageBox.Show("Wybierz rekord do edycji!!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                var result = MessageBox.Show("Czy chcesz usunąć ten rekord?", "Usuwanie rekordu", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    var id = (int)listView1.SelectedItems[0].Tag;
                    using (var context = new WarehouseContext())
                    {
                        var order = context.Orders.Where(_ => _.Id == id).Single();
                        context.Orders.Remove(order);
                        context.SaveChanges();
                    }
                }
                MessageBox.Show("Rekord został usuniety", "Komunikat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            else
            {
                MessageBox.Show("Wybierz rekord do usuniecia!!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                var id = (int)listView1.SelectedItems[0].Tag;
                using (var context = new WarehouseContext())
                {
                    var order = context.Orders.Include(_ => _.OrderProducts.Select(__ => __.Product)).Where(_ => _.Id == id).Single();
                    listView2.Items.Clear();

                    foreach (var orderProduct in order.OrderProducts)
                    {
                        var item = new ListViewItem(new string[] { orderProduct.Product.Name.ToString(), orderProduct.Count.ToString(), orderProduct.Product.NetValue.ToString(), orderProduct.Product.GrossValue.ToString() })
                        {
                            Tag = orderProduct.Id
                        };
                        listView2.Items.Add(item);
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshList();
        }
    }
}
