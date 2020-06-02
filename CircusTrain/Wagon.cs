using System.Collections.Generic;
using System.Linq;

namespace CircusTrain
{
    public class Wagon
    {
        // A list of all animals in wagon.
        private readonly List<Animal> _animalList = new List<Animal>();
        public IEnumerable<Animal> Animals => _animalList;

        // Return Sum of all points from each animal in the wagon.
        public int Points
        {
            get { return _animalList.Sum(animal => (int) animal.AnimalSize); }
        }

        // Checks if the animal fits in the wagon.
        // returns if the animal fits or not.
        private bool AnimalFits(Animal animal)
        {
            return Points + (int)animal.AnimalSize <= 10;
        }

        // Adds an animal to the wagon if the animal fits and if the animal cannot be eaten by other animals.
        // returns if the animal was added or not.
        public bool AddAnimalToWagon(Animal animal)
        {
            if (!AnimalFits(animal))
            { return false; }

            foreach (var animalInWagon in _animalList)
            {
                if (animalInWagon.Carnivore && animalInWagon.AnimalSize >= animal.AnimalSize)
                { return false; }

                if (animal.Carnivore && animal.AnimalSize >= animalInWagon.AnimalSize)
                { return false; }
            }

            _animalList.Add(animal);
            return true;
        }
        
    }
}