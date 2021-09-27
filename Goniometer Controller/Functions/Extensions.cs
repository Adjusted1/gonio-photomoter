using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goniometer_Controller
{
    static internal class StringExtensions
    {
        public static string Multiply(this string source, int multiplier)
        {
            StringBuilder sb = new StringBuilder(multiplier * source.Length);
            for (int i = 0; i < multiplier; i++)
            {
                sb.Append(source);
            }

            return sb.ToString();
        }


        public static string Multiply(this char source, int multiplier)
        {
            StringBuilder sb = new StringBuilder(multiplier);
            for (int i = 0; i < multiplier; i++)
            {
                sb.Append(source);
            }

            return sb.ToString();
        }
    }

    static internal class LinqExtensions
    {
        //
        // Summary:
        //     Invokes a transform function on each element of a sequence and returns the
        //     minimum System.Double value.
        //
        // Parameters:
        //   source:
        //     A sequence of values to determine the minimum value of.
        //
        //   selector:
        //     A transform function to apply to each element.
        //
        // Type parameters:
        //   TSource:
        //     The type of the elements of source.
        //
        // Returns:
        //     The member from the sequence with the minimum selector.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     source or selector is null.
        //
        //   System.InvalidOperationException:
        //     source contains no elements.
        public static TSource MinSelectMember<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (selector == null)
                throw new ArgumentNullException("selector");

            //keep track if the set had any members
            bool empty = true;

            //keep track of min value and member
            double min = Double.MaxValue;
            TSource minSource = default(TSource);

            foreach (TSource t in source)
            {
                //the set yielded a member!
                empty = false;

                if (selector(t) <= min)
                {
                    //record new min and member
                    min = selector(t);
                    minSource = t;
                }
            }

            if (empty)
                throw new InvalidOperationException();

            return minSource;
        }
    }
}
