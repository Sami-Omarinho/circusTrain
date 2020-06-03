namespace CircusTrain
{
    public class Animal
    {
        // Check if the animal is carnivore.
        public bool Carnivore { get; }
        // The size of the animal as an enumerator.
        public AnimalSize AnimalSize { get; }

        public Animal(bool carnivore, AnimalSize animalSize)
        {
            Carnivore = carnivore;
            AnimalSize = animalSize;
        }
        
        // Gets all the animals information as a string.
        // returns a string with all the animals info for displaying.
        public override string ToString()
        {
            return "Carnivorous: " + Carnivore.ToString() + " // Size: " + AnimalSize.ToString() + " // Points: " + (int) AnimalSize;
        }
    }
}