using exam.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace exam.View
{
    /// <summary>
    /// Логика взаимодействия для Ingred.xaml
    /// </summary>
    public partial class Ingred : Window
    {
        public ObservableCollection<Ingredient> ingredients { get; set; }
        public dishesEntities1 dishesEntities { get; set; }
        public Ingred(dishesEntities1 entities )
        {

            InitializeComponent();
            this.dishesEntities = entities;
            DataContext = this;
            ingredients = new ObservableCollection<Ingredient>(entities.Ingredients);
        }
        private void AddIngred(object sender, RoutedEventArgs e)
        {
            Ingredient ingredient = new Ingredient();
            ingredient.nameI = NameIngred.Text.Trim();
            ingredient.protein = int.Parse(ProtIngred.Text.Trim());
            ingredient.fat = int.Parse(FatIngred.Text.Trim());
            ingredient.carbonydrates = int.Parse(CarbonyIngred.Text.Trim());
            ingredient.weightI = int.Parse(WeightIngred.Text.Trim());  

            dishesEntities.Ingredients.Add(ingredient);
            dishesEntities.SaveChanges();
            NameIngred.Clear();
            ProtIngred.Clear();
            FatIngred.Clear();
            CarbonyIngred.Clear();
            WeightIngred.Clear();
         

        }

        private void DeleteIngred(object sender, RoutedEventArgs e)
        {
            var delItem = listView.SelectedItem as Ingredient;
            ingredients.Remove(delItem);
            dishesEntities.Ingredients.Remove(delItem);
            dishesEntities.SaveChanges();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Owner = this;
            main.Show();
            Hide();
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
