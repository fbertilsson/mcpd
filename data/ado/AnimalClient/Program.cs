using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalEntities;

namespace AnimalClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new AnimalsContainer())
            {
                var nAnimals = ctx.AnimalSet.Count();
                var nPersons = ctx.PersonSet.Count();

                var dog = new Dog {PetName = "Lobi", RaceName = "Keeshond"};
                ctx.AnimalSet.Add(dog);

                var m = new Monkey {MonkeySpecial = "special string", RaceName = "Chimp"};
                ctx.AnimalSet.Add(m);

                ctx.SaveChanges();


                RemoveAllAnimals(ctx);
            }


        }

        private static void RemoveAllAnimals(AnimalsContainer ctx)
        {
            foreach (var animal in ctx.AnimalSet)
            {
                ctx.AnimalSet.Remove(animal);
            }
            ctx.SaveChanges();
        }
    }
}
