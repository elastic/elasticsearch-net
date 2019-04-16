using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class LatLon
	{
		[DataMember(Name = "lat")]
		public double? Lat { get; set; }

		[DataMember(Name = "lon")]
		public double? Lon { get; set; }
	}
}
