using System;

namespace Nest.Tests.Unit.Internals.Serialize.DateTimes
{
	public class Flight
    {
        public DateTime DepartureDate { get; set; }
        public DateTime DepartureDateUtc { get; set; }
        public DateTime DepartureDateLocal { get; set; }
        public DateTime DepartureDateUtcWithTicks { get; set; }
        public DateTimeOffset DepartureDateOffset { get; set; }
        public DateTimeOffset DepartureDateOffsetZero { get; set; }
        public DateTimeOffset DepartureDateOffsetNonLocal { get; set; }

        protected bool Equals(Flight other)
        {
            return DepartureDate.Ticks.Equals(other.DepartureDate.Ticks) &&
                   DepartureDateUtc.Ticks.Equals(other.DepartureDateUtc.Ticks) &&
                   DepartureDateLocal.Ticks.Equals(other.DepartureDateLocal.Ticks) &&
				   DepartureDateUtcWithTicks.Ticks.Equals(other.DepartureDateUtcWithTicks.Ticks) &&
				   DepartureDateOffset.Ticks.Equals(other.DepartureDateOffset.Ticks) &&
                   DepartureDateOffsetZero.Ticks.Equals(other.DepartureDateOffsetZero.Ticks) &&
                   DepartureDateOffsetNonLocal.Ticks.Equals(other.DepartureDateOffsetNonLocal.Ticks);
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
                hashCode = (hashCode * 397) ^ DepartureDateUtcWithTicks.GetHashCode();
                hashCode = (hashCode * 397) ^ DepartureDateOffset.GetHashCode();
                hashCode = (hashCode * 397) ^ DepartureDateOffsetZero.GetHashCode();
                hashCode = (hashCode * 397) ^ DepartureDateOffsetNonLocal.GetHashCode();
                return hashCode;
            }
        }
    }
}