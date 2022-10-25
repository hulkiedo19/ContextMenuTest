using ContextMenuText.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ContextMenuText
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Product> _items { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            _items = new List<Product>();

            UpdateList();
        }

        private void UpdateList()
        {
            _items = new Variant2Context().Products
                .Include(p => p.ProductType)
                .ToList();

            ListView1.ItemsSource = _items;
        }

        private void DeleteProductMaterialsWithId(int Id)
        {
            var DbContext = new Variant2Context();
            var productMaterials = DbContext.ProductMaterials.Where(p => p.ProductId == Id).ToList();

            if (productMaterials.Count > 0)
                return;

            foreach (var pm in productMaterials)
                DbContext.ProductMaterials.Remove(pm);

            DbContext.SaveChanges();
        }

        // button click
        private void Add_Item_Click(object sender, RoutedEventArgs e)
        {
            Window window = new AddItemWindow();
            window.Show();
        }

        private void UpdateList_Click(object sender, RoutedEventArgs e)
        {
            UpdateList();
        }

        // menuitem click
        private void MenuItem_Edit_Click(object sender, RoutedEventArgs e)
        {
            Product? item = ListView1.Items.GetItemAt(ListView1.SelectedIndex) as Product;

            if(item == null)
            {
                MessageBox.Show("invalid item");
                return;
            }

            Window window = new EditItemWindow(item);
            window.Show();
        }

        private void MenuItem_Delete_Click(object sender, RoutedEventArgs e)
        {
            Product? item = ListView1.Items.GetItemAt(ListView1.SelectedIndex) as Product;
            if (item == null)
            {
                MessageBox.Show("error");
                return;
            }

            DeleteProductMaterialsWithId(item.Id);

            var DbContext = new Variant2Context();
            DbContext.Products.Remove(item);
            DbContext.SaveChanges();

            UpdateList();
        }
    }
}
