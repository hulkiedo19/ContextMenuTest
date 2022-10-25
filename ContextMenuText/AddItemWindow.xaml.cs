using ContextMenuText.Models;
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
using System.Windows.Shapes;

namespace ContextMenuText
{
    /// <summary>
    /// Interaction logic for AddItemWindow.xaml
    /// </summary>
    public partial class AddItemWindow : Window
    {
        public AddItemWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TB_Title.Text == "" || TB_ProductTypeId.Text == "" || TB_ArticleNumber.Text == "" || TB_Cost.Text == "")
            {
                MessageBox.Show("Invalid input");
                return;
            }

            Product product = new Product();
            product.Title = TB_Title.Text;
            product.ProductTypeId = Convert.ToInt32(TB_ProductTypeId.Text);
            product.ArticleNumber = TB_ArticleNumber.Text;
            product.MinCostForAgent = Convert.ToDecimal(TB_Cost.Text);

            product.Description = (TB_Description.Text == "") ? null : TB_Description.Text;
            product.ProductionPersonCount = (TB_PersonCount.Text == "") ? null : Convert.ToInt32(TB_PersonCount.Text);
            product.ProductionWorkshopNumber = (TB_WorkshopNumber.Text == "") ? null : Convert.ToInt32(TB_WorkshopNumber.Text);
            product.Image = (TB_ImagePath.Text == "") ? null : TB_ImagePath.Text;

            var DbContext = new Variant2Context();
            DbContext.Products.Add(product);
            DbContext.SaveChanges();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
