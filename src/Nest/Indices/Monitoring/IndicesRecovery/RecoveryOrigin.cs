using System.Runtime.Serialization;

namespace Nest
{
	public class RecoveryOrigin
	{
		[DataMember(Name ="hostname")]
		public string HostName { get; internal set; }

		[DataMember(Name ="id")]
		public string Id { get; internal set; }

		[DataMember(Name ="ip")]
		public string Ip { get; internal set; }

		[DataMember(Name ="name")]
		public string Name { get; internal set; }
	}
}
