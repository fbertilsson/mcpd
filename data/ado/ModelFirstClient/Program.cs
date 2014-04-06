using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Objects;
using System.Linq;
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
                var b2 = new TphSub1B
                {
                    Manufacturer = "B manufacturer",
                    SpecialB = "B2"
                };
                entities.TphBaseSet.Add(b2);
                entities.SaveChanges();

                //
                // ESQL experiments
                //
                try
                {
                    using (var ctx = new ObjectContext("name=ModelFirstEntContainer"))
                    {
                        //var esqlString = "select ROW(x.id as id, x.manufacturer as manuf, x.specialb as SpecB) from ModelFirstEntities.TphSub1B as x";
                        //var esqlString = "select ROW(x.id as id, x.manufacturer as manuf, x.specialb as SpecB) from ModelFirstEntities.TphBaseSet as x";
                        //var esqlString = "select row(x.id as id, x.manufacturer as manuf, x.specialb as SpecB) from TphBaseSet as x";
                        //var esqlString = "select ROW(x.Id as id, x.Manufacturer as manuf, x.SpecialB as SpecB) from ModelFirstEntContainer.TphBaseSet as x"; // [System.Data.EntitySqlException] = {"'Id' is not a member of type 'ModelFirstEnt.TphBase' in the currently loaded schemas. Near simple identifier, line 1, column 14."}
                        var esqlString = "select ROW(x.Manufacturer as manuf, x.SpecialB as SpecB) from ModelFirstEntContainer.TphBaseSet as x"; 
                        var myObjectQuery = new ObjectQuery<DbDataRecord>(esqlString, ctx);
                        var list = myObjectQuery.ToList();
                        foreach (var row in list)
                        {
                            Console.WriteLine("> {0}", row);
                        }
                    
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                //
                // Remove
                //
                entities.TphBaseSet.Remove(a);
                entities.TphBaseSet.Remove(b);
                entities.TphBaseSet.Remove(b2);
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
