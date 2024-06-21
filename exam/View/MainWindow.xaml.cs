using exam.Database;
using exam.View;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace exam
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public dishesEntities1 dishesEntities;

        public ObservableCollection<Dish> dishes {  get; set; }
        public ObservableCollection<DishAndIngridient> receipes { get; set; }
        public ObservableCollection<Ingredient> ingredients1 { get; set; }
        public ObservableCollection<DishModel> dishNotReady { get; set; }
        public ObservableCollection<DishModel> dishReady { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            dishesEntities = new dishesEntities1();
            
            DataContext = this;
            dishes = new ObservableCollection<Dish>(dishesEntities.Dishes);
            ingredients1 = new ObservableCollection<Ingredient>(dishesEntities.Ingredients);
            dishReady = new ObservableCollection<DishModel>();
            dishNotReady = new ObservableCollection<DishModel>();
            receipes = new ObservableCollection<DishAndIngridient>(dishesEntities.DishAndIngridients);
            FillListViews();
        }


        public void FillListViews()
        {

            for (int i = 0; i < dishes.Count; i++)
            {
                double protein = 0;
                double fat = 0;
                double Carbohydrate = 0;
                double calories = 0;
                for (int j = 0; j < receipes.Count; j++)
                {
                    if (receipes[j].nameDish == dishes[i].id)
                    {
                        var ingredient = ingredients1.Where(u => u.id == receipes[j].nameI).FirstOrDefault();
                        protein += ingredient.protein;
                        fat += ingredient.fat;
                        Carbohydrate += ingredient.carbonydrates;

                    }
                    calories = (4 * protein) + (4 * fat) + (9 * Carbohydrate);
                }
                if (dishes[i].ready == 1)
                {
                    dishReady.Add(new DishModel
                    {
                        Name = dishes[i].nameD,
                        Calories = $"Калории: {calories}",
                        ProteinfatCarbohydrate = $"БЖУ: {protein}, {fat}, {Carbohydrate}"
                    });
                }
                else
                {
                    dishNotReady.Add(new DishModel
                    {
                        Name = dishes[i].nameD,
                        Calories = $"Калории: {calories}",
                        ProteinfatCarbohydrate = $"БЖУ: {protein}, {fat}, {Carbohydrate}"
                    });
                }

            }
        }

        private void Button_Click_Ingredients(object sender, RoutedEventArgs e)
        {
            Ingred ingredients = new Ingred(dishesEntities);
            ingredients.Owner = this;
            ingredients.Show();
            Hide();

        }

        private void Button_Click_DishRedact(object sender, RoutedEventArgs e)
        {
            AddDish addDish = new AddDish(dishesEntities);
            var Item = listView.SelectedItem as Dish;
            addDish.setItem(Item);
            addDish.isEdit = true;
            addDish.Show();
            Hide();
        }

        private void Button_Click_DishCreate(object sender, RoutedEventArgs e)
        {
            AddDish addDish = new AddDish(dishesEntities);
            addDish.Owner = this;
            addDish.Show();
            Hide();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            var delItem = listView.SelectedItem as Dish;
            dishes.Remove(delItem);
            dishesEntities.Dishes.Remove(delItem);
            dishesEntities.SaveChanges();
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DelNotReady(object sender, RoutedEventArgs e)
        {
            var Items = listView.SelectedItem as DishModel;
            dishReady.Remove(Items);
            dishNotReady.Add(Items);
            var name = Items.Name;
            var dish = dishes.Where(a => a.nameD == name).FirstOrDefault();
            dish.ready = 0;
            dishesEntities.SaveChanges();
        }

        private void DelReady(object sender, RoutedEventArgs e)
        {
            var Items = listViewDontReady.SelectedItem as DishModel;
            dishNotReady.Remove(Items);
            dishReady.Add(Items);
            var name = Items.Name;
            var dish = dishes.Where(a => a.nameD == name).FirstOrDefault();
            dish.ready = 1;
            dishesEntities.SaveChanges();
        }
    }
}
