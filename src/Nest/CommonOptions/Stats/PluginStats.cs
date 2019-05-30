using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class PluginStats
	{
		[DataMember(Name ="classname")]
		public string ClassName { get; set; }

		[DataMember(Name ="description")]
		public string Description { get; set; }

		[DataMember(Name ="elasticsearch_version")]
		public string ElasticsearchVersion { get; set; }

		[DataMember(Name ="isolated")]
		public bool Isolated { get; set; }

		[DataMember(Name ="jvm")]
		public bool Jvm { get; set; }

		[DataMember(Name ="name")]
		public string Name { get; set; }

		[DataMember(Name ="site")]
		public bool Site { get; set; }

		[DataMember(Name = "has_native_controller")]
		public bool? HasNativeController { get; set; }

		[DataMember(Name ="java_version")]
		public string JavaVersion { get; set; }

		[DataMember(Name ="version")]
		public string Version { get; set; }
	}
}
