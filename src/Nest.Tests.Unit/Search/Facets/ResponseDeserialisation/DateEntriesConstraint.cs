using System.Collections.Generic;
using NUnit.Framework.Constraints;

namespace Nest.Tests.Unit.Search.Facets.ResponseDeserialisation
{
    internal class DateEntriesConstraint : EqualConstraint
    {
        private DateEntriesConstraint(object expected) : base(expected)
        {
            this.Using<IEnumerable<DateEntry>>(new DateEntrySequenceEqualityComparer());
        }

        internal static DateEntriesConstraint Sequence(IEnumerable<DateEntry> dateEntries)
        {
            return new DateEntriesConstraint(dateEntries);
        }
    }
}