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
    /// Interaction logic for EditItemWindow.xaml
    /// </summary>
    public partial class EditItemWindow : Window
    {
        private int item_id;
        public EditItemWindow(Product item)
        {
            InitializeComponent();

            item_id = item.Id;
            SetTextBoxes(item);
        }

        private void SetTextBoxes(Product item)
        {
            TB_Title.Text = item.Title;
            TB_ProductTypeId.Text = Convert.ToString(item.ProductTypeId);
            TB_Description.Text = item.Description;
            TB_ArticleNumber.Text = item.ArticleNumber;
            TB_ImagePath.Text = item.Image;
            TB_PersonCount.Text = Convert.ToString(item.ProductionPersonCount);
            TB_WorkshopNumber.Text = Convert.ToString(item.ProductionWorkshopNumber);
            TB_Cost.Text = Convert.ToString(item.MinCostForAgent);
        }

        private Product SetItem()
        {
            Product product = new Product();
            product.Title = TB_Title.Text;
            product.ProductTypeId = Convert.ToInt32(TB_ProductTypeId.Text);
            product.ArticleNumber = TB_ArticleNumber.Text;
            product.MinCostForAgent = Convert.ToDecimal(TB_Cost.Text);

            product.Description = (TB_Description.Text == "") ? null : TB_Description.Text;
            product.ProductionPersonCount = (TB_PersonCount.Text == "") ? null : Convert.ToInt32(TB_PersonCount.Text);
            product.ProductionWorkshopNumber = (TB_WorkshopNumber.Text == "") ? null : Convert.ToInt32(TB_WorkshopNumber.Text);
            product.Image = (TB_ImagePath.Text == "") ? null : TB_ImagePath.Text;

            return product;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TB_Title.Text == "" || TB_ProductTypeId.Text == "" || TB_ArticleNumber.Text == "" || TB_Cost.Text == "")
            {
                MessageBox.Show("Invalid input");
                return;
            }

            Product product = SetItem();

            var DbContext = new Variant2Context();
            Product item = DbContext.Products
                .Where(p => p.Id == item_id)
                .First();

            item.Title = product.Title;
            item.ProductTypeId = product.ProductTypeId;
            item.ArticleNumber = product.ArticleNumber;
            item.MinCostForAgent = product.MinCostForAgent;

            item.Description = product.Description;
            item.ProductionPersonCount = product.ProductionPersonCount;
            item.ProductionWorkshopNumber = product.ProductionWorkshopNumber;
            item.Image = product.Image;

            DbContext.SaveChanges();

            // idk why .Update() method does not work, but i think it's good solution for this
            //DbContext.SaveChanges();
            //DbContext.Products.Update(product);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
