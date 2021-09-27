using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Goniometer
{
    internal static class ControlExtensions
    {
        public static void Invoke(this Control control, Action action)
        {
            control.Invoke((Delegate)action);
        }
    }

    internal static class TimeSpanExtensions
    {
        public static TimeSpan? Sum(this IEnumerable<TimeSpan> spans)
        {
            TimeSpan? total = null;
            foreach (var span in spans)
            {
                if (total == null)
                    total = new TimeSpan();

                total += span;
            }

            return total;
        }
    }

    internal static class LinqExtensions
    {
        public static IEnumerable<TSource> WhereIn<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, TValue> selector, TValue[] values)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (selector == null)
                throw new ArgumentNullException("selector");

            if (values == null)
                throw new ArgumentNullException("values");

            foreach (TSource t in source)
            {
                if (values.Contains(selector(t)))
                    yield return t;
            }
        }
    }
}
