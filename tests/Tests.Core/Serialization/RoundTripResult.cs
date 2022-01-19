using System;
using Tests.Core.Extensions;

namespace Tests.Core.Serialization
{
	public class RoundTripResult<T> : DeserializationResult<T>
	{
		public int Iterations { get; set; }

		public override string ToString()
		{
			var s = $"RoundTrip: {Iterations.ToOrdinal()} iteration";
			s += Environment.NewLine;
			s += base.ToString();
			return s;
		}
	}
}
