using ModelFirstEntities;

namespace ModelFirstClient
{
    class Program
    {
        static void Main(string[] args)
        {
            WorkWithTpt();
            WorkWithTph();
        }

        private static void WorkWithTph()
        {
            using (var entities = new ModelFirstEntContainer())
            {
                
                var a = new TphSub1A
                {
                    Manufacturer = "A manufacturer for A",
                    SpecialA = "A"
                };
                entities.TphBaseSet.Add(a);

                var b = new TphSub1B
                {
                    Manufacturer = "B manufacturer",
                    SpecialB = "B"
                };
                entities.TphBaseSet.Add(b);
                entities.SaveChanges();

                entities.TphBaseSet.Remove(a);
                entities.TphBaseSet.Remove(b);
                entities.SaveChanges();
            }
        }

        private static void WorkWithTpt()
        {
            using (var entities = new ModelFirstEntContainer())
            {
                var sub = entities.BaseEntitySet.Create<Sub1A>();
                sub.Manufacturer = "IKEA";
                sub.SpecialA = "I'm a special Sub1a";
                entities.BaseEntitySet.Add(sub);
                entities.SaveChanges();

                entities.BaseEntitySet.Remove(sub);
                entities.SaveChanges();


                sub = new Sub1A
                {
                    Manufacturer = "Jula",
                    SpecialA = "Another special"
                };
                entities.BaseEntitySet.Add(sub);
                entities.SaveChanges();

                entities.BaseEntitySet.Remove(sub);
                entities.SaveChanges();
            }
        }
    }
}
