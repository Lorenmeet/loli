using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam
{
    public class DishModel : INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

        private double _weight;
        public double Weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("Weight"));
            }
        }

        private string _calories;
        public string Calories
        {
            get { return _calories; }
            set
            {
                _calories = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("Calories"));
            }
        }

        private string _proteinfatCarbohydrate;
        public string ProteinfatCarbohydrate
        {
            get { return _proteinfatCarbohydrate; }
            set
            {
                _proteinfatCarbohydrate = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("ProteinfatCarbohydrate"));
            }
        }
        private bool _readyornot;
        public bool Readyornot
        {
            get { return _readyornot; }
            set
            {
                _readyornot = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("Readyornot"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(sender, e);
            }
        }
    }
}
