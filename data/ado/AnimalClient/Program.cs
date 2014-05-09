using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                dog = new Dog {PetName = "Lobster", RaceName = "Spanish Waterdog"};
                ctx.AnimalSet.Add(dog);

                // Finds nothing because nothing persisted yet (unless a previous run was aborted before delete)
                var searchResult = ctx.DogsSearch("Lo%");
                Debug.Assert(! searchResult.Any());
                
                var m = new Monkey {MonkeySpecial = "special string", RaceName = "Chimp"};
                ctx.AnimalSet.Add(m);

                var jungle = new Habitat {Name = "jungle"};
                var med = new Habitat {Name = "mediterranian"};
                dog.Habitat.Add(med);
                dog.Habitat.Add(jungle);

                ctx.SaveChanges();

                searchResult = ctx.DogsSearch("Lo%");
                foreach (var hit in searchResult)
                {
                    Console.WriteLine("Id: {0}, PetName: {1}", hit.Id, hit.PetName);
                }

                // Try a model defined function
                try
                {
                    ExecModelDefinedFunction(ctx);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                RemoveAllAnimals(ctx);
            }


        }

        private static void ExecModelDefinedFunction(AnimalsContainer ctx)
        {
            // This line produces run-time error because it calls directly: 
            // var s = AnimalExtensions.AnimalExtensions.MyModelDefinedFunctionRaceNamePlusPetName(dog);

            var dogsFullnames =
                from aDog in ctx.AnimalSet.OfType<Dog>()
                select new { FullName = AnimalExtensions.MyModelDefinedFunctionRaceNamePlusPetName(aDog) };
            
            foreach (var o in dogsFullnames) {
                Console.WriteLine("Model defined function - race name plus pet name: {0}", o.FullName);
            }

            Console.WriteLine("Dogs' habitats:");
            var dogsHabitats =
                from aDog in ctx.AnimalSet.OfType<Dog>()
                select new {Habitats = AnimalExtensions.MyModelDefinedFunctionAnimalHabitatStrings(aDog)};
            //foreach (var dogHabitats in ctx.AnimalSet.OfType<Dog>()
            //    .(dog => new {Habitats = AnimalExtensions.MyModelDefinedFunctionAnimalHabitatStrings(dog)}))
            foreach (var habitatName in dogsHabitats.SelectMany(habitatList => habitatList.Habitats))
            {
                Console.WriteLine("  " + habitatName);
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
