using System;

namespace Nest.Tests.Unit.Internals.Serialize
{
    internal class Flight
    {
        public DateTime DepartureDate { get; set; }
        public DateTime DepartureDateUtc { get; set; }
        public DateTime DepartureDateLocal { get; set; }
        public DateTimeOffset DepartureDateOffset { get; set; }
        public DateTimeOffset DepartureDateOffsetZero { get; set; }

        protected bool Equals(Flight other)
        {
            return DepartureDate.Equals(other.DepartureDate) &&
                   DepartureDateUtc.Equals(other.DepartureDateUtc) &&
                   DepartureDateLocal.Equals(other.DepartureDateLocal) &&
                   DepartureDateOffset.Equals(other.DepartureDateOffset) &&
                   DepartureDateOffsetZero.Equals(other.DepartureDateOffsetZero);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Flight)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = DepartureDate.GetHashCode();
                hashCode = (hashCode * 397) ^ DepartureDateUtc.GetHashCode();
                hashCode = (hashCode * 397) ^ DepartureDateLocal.GetHashCode();
                hashCode = (hashCode * 397) ^ DepartureDateOffset.GetHashCode();
                hashCode = (hashCode * 397) ^ DepartureDateOffsetZero.GetHashCode();
                return hashCode;
            }
        }
    }
}