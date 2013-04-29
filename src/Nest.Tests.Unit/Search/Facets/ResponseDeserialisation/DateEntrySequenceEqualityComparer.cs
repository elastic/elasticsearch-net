using System.Collections.Generic;
using System.Linq;

namespace Nest.Tests.Unit.Search.Facets.ResponseDeserialisation
{
    internal class DateEntrySequenceEqualityComparer : IEqualityComparer<IEnumerable<DateEntry>>, IEqualityComparer<DateEntry>
    {
        public bool Equals(DateEntry x, DateEntry y)
        {
            return x.Count == y.Count &&
                   x.Max == y.Max &&
                   x.Mean == y.Mean &&
                   x.Min == y.Min &&
                   x.Time == y.Time;
        }

        public int GetHashCode(DateEntry obj)
        {
            return obj.GetHashCode();
        }

        public bool Equals(IEnumerable<DateEntry> x, IEnumerable<DateEntry> y)
        {
            var xArray = x as DateEntry[] ?? x.ToArray();
            var yArray = y as DateEntry[] ?? y.ToArray();

            return xArray.Count() == yArray.Count() && xArray.SequenceEqual(yArray, this);
        }

        public int GetHashCode(IEnumerable<DateEntry> sequence)
        {
            return sequence.Aggregate(0, (current, dateEntry) => current*dateEntry.GetHashCode());
        }
    }
}