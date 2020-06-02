using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CircusTrain;
using System.Text.RegularExpressions;

namespace CircusTrainWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isCarnivore = false;
        private AnimalSize _size = AnimalSize.Small;
        private int _amount = 1;

        private readonly List<Animal> _animals = new List<Animal>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            for (var i = 0; i < _amount; i++)
            {
                var animal = new Animal(_isCarnivore, _size);
                _animals.Add(animal);
                ListBoxAnimals.Items.Add(animal.ToString());
            }
        }

        private void CarnivoreCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            _isCarnivore = true;
        }

        private void CarnivoreCheckbox_OnUnchecked(object sender, RoutedEventArgs e)
        {
            _isCarnivore = false;
        }

        private void AnimalSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var value = AnimalSizeComboBox.SelectedValue.ToString();

            if (value.ToLower().Contains("small"))
                _size = AnimalSize.Small;

            if (value.ToLower().Contains("medium"))
                _size = AnimalSize.Medium;

            if (value.ToLower().Contains("large"))
                _size = AnimalSize.Large;
        }

        private void DistributeButton_Click(object sender, RoutedEventArgs e)
        {
            DistributeInfo.Items.Clear();

            if (!_animals.Any()) return;

            var distributor = new WagonDistributor(_animals);
            distributor.Distribute();
            var items = View(distributor);

            foreach (var item in items)
                DistributeInfo.Items.Add(item);
        }

        private void AmountOfAnimals_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(AmountOfAnimals.Text))
                _amount = Int32.Parse(AmountOfAnimals.Text);
        }

        private List<String> View(WagonDistributor distributor)
        {
            var sb = new List<String>();

            sb.Add("\n--- Wagon Distribution ---\n");
            sb.Add("Total Amount of wagons: " + distributor.Wagons.Count);
            sb.Add("----------------------------------------------------\n");
            for (var i = 0; i < distributor.Wagons.Count; i++)
            {
                var wagon = distributor.Wagons[i];
                var animals = wagon.Animals.ToList();
                sb.Add("Wagon " + (i + 1) + ":\n\n" + animals.Count + " animals\n");
                for (var j = 0; j < animals.Count; j++)
                {
                    var animal = animals[j];
                    sb.Add("Animal " + (j + 1) + " - " + animal);
                }

                sb.Add("---------------------------------------------------- +");
                sb.Add("Total points: " + wagon.Points + "\n\n");
            }

            return sb;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            _animals.Clear();
            ListBoxAnimals.Items.Clear();
        }
    }
}