using System;
using System.Configuration;
using System.Data.SqlClient;
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
            using (var ctx = new HierarchiesContainer(connectionString))
            {
                GoTph(ctx);
                GoTpt(ctx);
                ctx.SaveChanges();
            }
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
