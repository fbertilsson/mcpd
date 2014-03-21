using AnimalEntities;
using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkTest1
{
    public class AnimalExtensions
    {
        [EdmFunction("Animals", "MyModelDefinedFunctionRaceNamePlusPetName")]
        public static string MyModelDefinedFunctionRaceNamePlusPetName(Dog aDog)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }
    }
}
