using System;
using System.Configuration;
using System.Data;
using System.Data.Objects;
using System.Data.SqlClient;
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
            try
            {
                using (var ctx = new HierarchiesContainer(connectionString))
                {
                    GoTph(ctx);
                    GoTpt(ctx);
                    DetachModifyAttach(ctx);
                    ctx.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void DetachModifyAttach(HierarchiesContainer ctx)
        {
            var firstInsect = ctx.TphAnimalSet.OfType<TphInsect>().First();
            Console.WriteLine("  Antenna count: {1}, State before detaching: {0}", firstInsect.EntityState, firstInsect.AntennaCount);
            var copy = new TphInsect
            {
                Id = firstInsect.Id,
                Name = firstInsect.Name,
                AntennaCount = firstInsect.AntennaCount
            };
            ctx.TphAnimalSet.Detach(firstInsect);
            firstInsect.AntennaCount += 1;
            Console.WriteLine("  Detached and incremented antenna count. State now: {0}", firstInsect.EntityState);
            ctx.TphAnimalSet.Attach(firstInsect);
            Console.WriteLine("  Attached. State now: {0}", firstInsect.EntityState);
            ctx.TphAnimalSet.ApplyOriginalValues(copy);
            Console.WriteLine("  Applied original values. State now: {0}", firstInsect.EntityState);
            ctx.Refresh(RefreshMode.ClientWins, firstInsect);
            Console.WriteLine("  Refreshed. State now: {0}", firstInsect.EntityState);
            ctx.SaveChanges();
            Console.WriteLine("  Saved with no options. State now: {0}", firstInsect.EntityState);
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
        
        private void GoTph(HierarchiesContainer ctx)
        {
            Console.Write("Add TPH mammal, name? ");
            var name = Console.ReadLine();
            if (string.IsNullOrEmpty(name)) return;
            var mammal = new TphMammal {Name = name, DaysPregnancy = name.Length * 10};
            ctx.TphAnimalSet.AddObject(mammal);


            Console.Write("Add TPH insect, name? ");
            name = Console.ReadLine();
            if (string.IsNullOrEmpty(name)) return;
            var insect = new TphInsect { Name = name, AntennaCount = name.Length };
            ctx.TphAnimalSet.AddObject(insect);
        }

        private void GoTpt(HierarchiesContainer ctx)
        {
            Console.Write("Add TPT mammal, name? ");
            var name = Console.ReadLine();
            if (string.IsNullOrEmpty(name)) return;
            var mammal = new TptMammal { Name = name, DaysPregnancy = name.Length * 10 };
            ctx.TptAnimals.AddObject(mammal);


            Console.Write("Add TPT insect, name? ");
            name = Console.ReadLine();
            if (string.IsNullOrEmpty(name)) return;
            var insect = new TptInsect { Name = name, AntennaCount = name.Length };
            ctx.TptAnimals.AddObject(insect);
        }

    }
}
