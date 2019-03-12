using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreboardTrials
{
    public class ProportionValue<T>
    {
        public double Proportion { get; set; } //a proportion is allocated to each possible outcome when navigating through lists
        public T Value { get; set; } //this will store play outcomes
        public T InsertValue { get; set; } //this will store strings to trigger methods
        public ProportionValue<T>[] PropValueList { get; set; }
    }

    public static class ProportionValue
    {
        public static ProportionValue<T> Create<T>(double proportion, T value)
        {
            return new ProportionValue<T> { Proportion = proportion, Value = value };
        }

        public static ProportionValue<T> Create<T>(double proportion, ProportionValue<T>[] propValueList)
        {
            return new ProportionValue<T> { Proportion = proportion, PropValueList = propValueList };
        }

        public static ProportionValue<T> Create<T>(double proportion, ProportionValue<T>[] propValueList, T value)
        {
            return new ProportionValue<T> { Proportion = proportion, Value = value, PropValueList = propValueList };
        }

        public static ProportionValue<T> Create<T>(double proportion, ProportionValue<T>[] propValueList, T value, T insertValue)
        {
            return new ProportionValue<T> { Proportion = proportion, Value = value, PropValueList = propValueList, InsertValue = insertValue };
        }
        
        //the random number object used to choose outcomes for a play by comparing to each option's proportion    
        static Random random = new Random();

        public static ProportionValue<T> ChooseByRandom<T>(
            this IEnumerable<ProportionValue<T>> collection)
        {

            //used to vary the random numbers
            var rnd = random.NextDouble();

            //this loop takes each random number and compares it to each item on the list. If an item's proportion is 
           //greater than the random number generated, the item is returned. An "item" is a proportionvalue object.
            foreach (var propValueObject in collection)
            {
                //If the random number is less than the propValueObject's proportion, return the propValueObject
                if (rnd < propValueObject.Proportion)
                {
                    return propValueObject;
                }
                //Else: subtract the proportion of the propValueObject from the random number and try the next potential propValueObject
                rnd -= propValueObject.Proportion;
            }

            throw new InvalidOperationException(
                "The proportions in the collection do not add up to 1");
        }       
        
    }
}
