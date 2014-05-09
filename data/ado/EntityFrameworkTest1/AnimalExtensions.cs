using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;

namespace AnimalEntities
{
    public class AnimalExtensions
    {
        [EdmFunction("Animals", "MyModelDefinedFunctionRaceNamePlusPetName")]
        public static string MyModelDefinedFunctionRaceNamePlusPetName(Dog aDog)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }

        [EdmFunction("Animals", "MyModelDefinedFunctionAnimalHabitatStrings")]
        public static IEnumerable<string> MyModelDefinedFunctionAnimalHabitatStrings(Dog aDog)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }
    }
}
