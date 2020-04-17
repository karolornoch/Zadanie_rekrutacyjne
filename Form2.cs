using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zadanie_rekrutacyjne.DB;
using Zadanie_rekrutacyjne.DB.Entities;
using System.Data.Entity;

namespace Zadanie_rekrutacyjne
{
    public partial class Form2 : Form
    {
        private List<Client> Clients;
        private List<Product> Products;
        private Dictionary<Guid, OrderProducts> OrdersProducts = new Dictionary<Guid, OrderProducts>();
        private int? OrderId { get; set; }
        public Form1 MainForm;
        public Form2(int? orderId = null)
        {
            InitializeComponent();
            OrderId = orderId;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            using (var context = new WarehouseContext())
            {
                Clients = context.Clients.ToList();
                comboBox1.DisplayMember = "Value";
                comboBox1.ValueMember = "Key";
                foreach (var client in Clients)
                {
                    comboBox1.Items.Add(new KeyValuePair<int, int>(client.Id, client.Id));
                }
                Products = context.Products.ToList();
                comboBox2.DisplayMember = "Value";
                comboBox2.ValueMember = "Key";
                foreach (var product in Products)
                {
                    comboBox2.Items.Add(new KeyValuePair<int, string>(product.Id, product.Name));
                }

                if (OrderId.HasValue)
                {
                    var order = context.Orders
                        .Include(_ => _.Client)
                        .Include(_ => _.OrderProducts.Select(__ => __.Product))
                        .Where(_ => _.Id == OrderId)
                        .Single();
                    comboBox1.SelectedIndex = comboBox1.Items.IndexOf(new KeyValuePair<int, int>(order.ClientId, order.ClientId));
                    foreach (var orderProduct in order.OrderProducts)
                    {
                        var tag = Guid.NewGuid();
                        var item = new ListViewItem(new string[] { orderProduct.Product.Name, orderProduct.Count.ToString(), orderProduct.Product.NetValue.ToString(), orderProduct.Product.GrossValue.ToString() })
                        {
                            Tag = tag
                        };
                        OrdersProducts.Add(tag, orderProduct);
                        listView1.Items.Add(item);
                    }
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var product = Products.Where(_ => _.Id == ((KeyValuePair<int, String>)comboBox2.SelectedItem).Key).Single();
            var count = Convert.ToInt32(textBox1.Text);
            var tag = Guid.NewGuid();
            var item = new ListViewItem(new string[] { product.Name, textBox1.Text, product.NetValue.ToString(), product.GrossValue.ToString() })
            {
                Tag = tag
            };
            OrdersProducts.Add(tag, new OrderProducts { Count = count, ProductId = product.Id });
            listView1.Items.Add(item);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var context = new WarehouseContext())
            {
                //Update current
                if (OrderId.HasValue)
                {
                    var currentOrder = context.Orders
                        .Include(_ => _.Client)
                        .Include(_ => _.OrderProducts.Select(__ => __.Product))
                        .Where(_ => _.Id == OrderId)
                        .Single();
                    foreach (var orderProduct in OrdersProducts)
                    {
                        if (orderProduct.Value.Id == 0)
                        {
                            orderProduct.Value.OrderId = currentOrder.Id;
                            context.OrdersProducts.Add(orderProduct.Value);
                        }
                        else
                        {
                            var currentProductOrder = currentOrder.OrderProducts.Single(_ => _.Id == orderProduct.Value.Id);
                            currentProductOrder.Count = orderProduct.Value.Count;
                            context.Entry<OrderProducts>(currentProductOrder).State = EntityState.Modified;
                        }
                    }
                }
                else
                {
                    var order = new Order { ClientId = ((KeyValuePair<int, int>)comboBox1.SelectedItem).Key, Date = DateTime.UtcNow };
                    context.Orders.Add(order);
                    foreach (var orderProduct in OrdersProducts)
                    {
                        orderProduct.Value.OrderId = order.Id;
                        context.OrdersProducts.Add(orderProduct.Value);
                    }
                }
                context.SaveChanges();
            }
            this.Close();
            MainForm.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                var selected = listView1.SelectedItems[0];
                if (OrderId.HasValue)
                {
                    var result = MessageBox.Show("Czy chcesz usunąć ten rekord?", "Usuwanie rekordu", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        using (var context = new WarehouseContext())
                        {
                            var id = OrdersProducts.Single(__ => __.Key == (Guid)selected.Tag).Value.Id;
                            var entity = context.OrdersProducts.Where(_ => _.Id == id).Single();
                            context.OrdersProducts.Remove(entity);
                            context.SaveChanges();
                        }
                    }
                    else
                    {
                        return;
                    }
                        
                }
                OrdersProducts.Remove((Guid)selected.Tag);
                listView1.Items.Remove(selected);
            }
        }
    }
}
