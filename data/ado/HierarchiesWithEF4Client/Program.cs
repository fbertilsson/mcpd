using System;
using System.Configuration;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.IO;
using System.Linq;
using HierarchiesWithEF4;

namespace HierarchiesWithEF4Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["HierarchiesContainer"].ConnectionString;
            new Program().Go(connectionString);
        }

        private void Go(string connectionString)
        {
            bool doLoop = true;
            while (doLoop)
            {
                bool ok;
                var chosenInt = Read(out ok);
                if (! ok) continue;
                if (chosenInt == 0) doLoop = false;
                try
                {
                    var result = Eval(connectionString, chosenInt);
                    Console.WriteLine(result.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private static int Read(out bool ok)
        {
            Console.WriteLine("Choose:");
            Console.WriteLine(" 0: Exit");
            Console.WriteLine(" 1. New TPH");
            Console.WriteLine(" 2. New TPT");
            Console.WriteLine(" 3. Detach - modify - attach");
            Console.WriteLine(" 4. Delete extension points TPH");
            var choice = Console.ReadLine();
            int chosenInt;
            ok = int.TryParse(choice, out chosenInt);
            return chosenInt;
        }

        private StringWriter Eval(string connectionString, int chosenInt)
        {
            StringWriter w = null;
            using (var ctx = new HierarchiesContainer(connectionString))
            {
                switch (chosenInt)
                {
                    case 0:
                        w = new StringWriter();
                        break;
                    case 1:
                        w = GoTph(ctx);
                        break;
                    case 2:
                        w = GoTpt(ctx);
                        break;
                    case 3:
                        w = DetachModifyAttach(ctx);
                        break;
                    case 4:
                        w = DeleteExtensionPoints(ctx);
                        break;
                }
                try
                {
                    ctx.SaveChanges();
                }
                catch (OptimisticConcurrencyException e)
                {
                    Console.WriteLine(e);
                    foreach (ObjectStateEntry item in e.StateEntries)
                    {
                        Console.WriteLine("Found an item: {0}", item);
                    }
                }
            }
            return w;
        }

        private StringWriter DetachModifyAttach(HierarchiesContainer ctx)
        {
            var firstInsect = ctx.TphAnimalSet.OfType<TphInsect>().First();
            var w = new StringWriter();
            w.WriteLine("  Antenna count: {1}, State before detaching: {0}", firstInsect.EntityState, firstInsect.AntennaCount);
            var copy = new TphInsect
            {
                Id = firstInsect.Id,
                Name = firstInsect.Name,
                AntennaCount = firstInsect.AntennaCount
            };
            ctx.TphAnimalSet.Detach(firstInsect);
            firstInsect.AntennaCount += 1;
            w.WriteLine("  Detached and incremented antenna count. State now: {0}", firstInsect.EntityState);
            ctx.TphAnimalSet.Attach(firstInsect);
            w.WriteLine("  Attached. State now: {0}", firstInsect.EntityState);
            ctx.TphAnimalSet.ApplyOriginalValues(copy);
            w.WriteLine("  Applied original values. State now: {0}", firstInsect.EntityState);
            ctx.Refresh(RefreshMode.ClientWins, firstInsect);
            w.WriteLine("  Refreshed. State now: {0}", firstInsect.EntityState);
            ctx.SaveChanges();
            w.WriteLine("  Saved with no options. State now: {0}", firstInsect.EntityState);
            return w;
        }

        private void DetachModifyAttachTake1(HierarchiesContainer ctx)
        {
            var firstInsect = ctx.TphAnimalSet.OfType<TphInsect>().First();
            Console.WriteLine("  Antenna count: {1}, State before detaching: {0}", firstInsect.EntityState, firstInsect.AntennaCount);
            ctx.TphAnimalSet.Detach(firstInsect);
            firstInsect.AntennaCount += 1;
            Console.WriteLine("  Detached and incremented antenna count. State now: {0}", firstInsect.EntityState);
            ctx.TphAnimalSet.Attach(firstInsect);
            Console.WriteLine("  Attached. State now: {0}", firstInsect.EntityState);
            ctx.TphAnimalSet.ApplyOriginalValues(firstInsect);
            Console.WriteLine("  Applied original values. State now: {0}", firstInsect.EntityState);
            ctx.Refresh(RefreshMode.ClientWins, firstInsect);
            Console.WriteLine("  Refreshed. State now: {0}", firstInsect.EntityState);
            ctx.SaveChanges();
            Console.WriteLine("  Saved with no options. State now: {0}", firstInsect.EntityState);
        }
        
        private StringWriter GoTph(HierarchiesContainer ctx)
        {
            var w = new StringWriter();
            Console.Write("Add TPH mammal, name? ");
            var name = Console.ReadLine();
            if (string.IsNullOrEmpty(name)) return w;
            var mammal = new TphMammal {Name = name, DaysPregnancy = name.Length * 10};
            ctx.TphAnimalSet.AddObject(mammal);


            Console.Write("Add TPH insect, name? ");
            name = Console.ReadLine();
            if (string.IsNullOrEmpty(name)) return w;
            var insect = new TphInsect { Name = name, AntennaCount = name.Length };
            ctx.TphAnimalSet.AddObject(insect);
            return w;
        }

        private StringWriter GoTpt(HierarchiesContainer ctx)
        {
            var w = new StringWriter();
            Console.Write("Add TPT mammal, name? ");
            var name = Console.ReadLine();
            if (string.IsNullOrEmpty(name)) return w;
            var mammal = new TptMammal { Name = name, DaysPregnancy = name.Length * 10 };
            ctx.TptAnimals.AddObject(mammal);


            Console.Write("Add TPT insect, name? ");
            name = Console.ReadLine();
            if (string.IsNullOrEmpty(name)) return w;
            var insect = new TptInsect { Name = name, AntennaCount = name.Length };
            ctx.TptAnimals.AddObject(insect);
            return w;
        }

        private StringWriter DeleteExtensionPoints(HierarchiesContainer ctx)
        {
            var w = new StringWriter();
            Console.Write("Enter id to delete: ");
            var idString = Console.ReadLine();
            if (idString == null) return w;

            var id = int.Parse(idString);
            var animal = ctx.TphAnimalSet.FirstOrDefault(x => x.Id == id);
            if (animal == null)
            {
                w.WriteLine("Could not find animal with id {0}", idString);
            }
            else
            {
                ctx.DeleteObject(animal);               // either
                ctx.TphAnimalSet.DeleteObject(animal);  // or, but found no extension point in EF. Only in Linq2Sql???
                w.WriteLine("Deleted.");
            }
            return w;
        }


    }
}
