using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ShardPath
	{
		[DataMember(Name ="data_path")]
		public string DataPath { get; internal set; }

		[DataMember(Name ="is_custom_data_path")]
		public bool IsCustomDataPath { get; internal set; }

		[DataMember(Name ="state_path")]
		public string StatePath { get; internal set; }
	}
}
