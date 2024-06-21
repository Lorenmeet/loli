using exam.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для AddDish.xaml
    /// </summary>
    public partial class AddDish : Window
    {
        public dishesEntities1 dishesEntities;

        public ObservableCollection<Ingredient> ingredients { get; set; }
        public ObservableCollection<Dish> dishes { get; set; }
        public bool isEdit { get; set; }
        public dishesEntities1 entities;

        public AddDish(dishesEntities1 entities)
        {
            Binding bindingCb = new Binding();
            InitializeComponent();
            this.entities = entities;
            DataContext = this;
            dishes = new ObservableCollection<Dish>(entities.Dishes);
            ingredients = new ObservableCollection<Ingredient>(entities.Ingredients);
            bindingCb.Source = ingredients;
            cbIngredients.SetBinding(ComboBox.ItemsSourceProperty, bindingCb);

        }
        public void setItem(Dish dish)
        {

            DataContext = dish;
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            if (isEdit == true)
            {


                entities.SaveChanges();

            }
        }

        private void AddDishes(object sender, RoutedEventArgs e)
        {

            int TrueFalse = 0;

            if (CheckItem.IsChecked == true)
            {
                TrueFalse = 1;
            }
            else if (CheckItem.IsChecked == false)
            {
                TrueFalse = 0;
            }
            Dish dish = new Dish();
            dish.nameD = nameDish.Text.Trim();
            dish.ready = (byte?)TrueFalse;
            entities.Dishes.Add(dish);
            entities.SaveChanges();
            dishes.Add(dish);
            var lasDish = dishes.LastOrDefault();
            lasDish = entities.Dishes.Where(a => a.nameD == lasDish.nameD).FirstOrDefault();
            for(int i = 0; i < Items.Items.Count; i++)
            {
             
               entities.DishAndIngridients.Add(new DishAndIngridient {nameDish = lasDish.id,nameI = (Items.Items[i] as Ingredient).id });
                entities.SaveChanges();
            }
            

        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Owner = this;
            main.Show();
            Hide();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           var ItemIngrid =  cbIngredients.SelectedItem as Ingredient;
           
            Items.Items.Add(ItemIngrid);
        }
    }
}
