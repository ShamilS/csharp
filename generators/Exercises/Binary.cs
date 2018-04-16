using Generators.Input;

namespace Generators.Exercises
{
    public class Binary : GeneratorExercise
    {
        protected override void UpdateCanonicalData(CanonicalData canonicalData)
        {
            foreach (var canonicalDataCase in canonicalData.Cases)
            {
                if (canonicalDataCase.Property == "decimal") canonicalDataCase.Property = "ToDecimal";
                if (canonicalDataCase.Expected == null) canonicalDataCase.Expected = 0;
            }
        }
    }
}