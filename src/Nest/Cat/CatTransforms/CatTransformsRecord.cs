using System;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class CatTransformsRecord : ICatRecord
	{
		[DataMember(Name ="id")]
		public string Id { get; internal set; }

		[DataMember(Name ="create_time")]
		public DateTimeOffset CreateTime { get; internal set; }

		[DataMember(Name ="version")]
		public string Version { get; internal set; }

		[DataMember(Name ="source_index")]
		public string SourceIndex { get; internal set; }

		[DataMember(Name ="dest_index")]
		public string DestinationIndex { get; internal set; }

		[DataMember(Name ="pipeline")]
		public string Pipeline { get; internal set; }

		[DataMember(Name ="description")]
		public string Description { get; internal set; }

		[DataMember(Name ="transform_type")]
		public TransformType TransformType { get; internal set; }

		[DataMember(Name ="frequency")]
		public Time Frequency { get; internal set; }

		[DataMember(Name ="max_page_search_size")]
		public string MaxPageSearchSize { get; internal set; }

		[DataMember(Name ="state")]
		public TransformState State { get; internal set; }
	}
}
