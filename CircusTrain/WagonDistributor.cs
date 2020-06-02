using System.Collections.Generic;
using System.Linq;

namespace CircusTrain
{
    public class WagonDistributor
    {
        // A list of all the wagons created.
        public readonly List<Wagon> WagonList = new List<Wagon>();

        // List of animals that get put in wagons.
        private readonly List<Animal> _animalList;

        // list to fill with animal object
        public WagonDistributor(List<Animal> animals)
        {
            _animalList = animals;
        }

        // Distributes all animals in the List in wagons
        // Filled wagons get put in list
        public void Distribute()
        {
            foreach (var animal in _animalList)
            {
                if (WagonList.Any(wagon => wagon.AddAnimal(animal))) continue;

                AddWagonWithAnimal(animal);
            }
        }
        
        /// <summary>
        /// Creates a new wagon and adds the animal to the newly created wagon.
        /// </summary>
        /// <param name="animal">The animal that needs a new wagon.</param>
        private void AddWagonWithAnimal(Animal animal)
        {
            var newWagon = new Wagon();
            newWagon.AddAnimal(animal);
            WagonList.Add(newWagon);
        }
    }
}