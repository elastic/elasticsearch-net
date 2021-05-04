// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class CatNodeAttributesRecord : ICatRecord
	{
		[DataMember(Name ="attr")]
		public string Attribute { get; set; }

		[DataMember(Name ="host")]
		public string Host { get; set; }

		// duration indices successful_shards failed_shards total_shards
		[DataMember(Name ="id")]
		public string Id { get; set; }

		[DataMember(Name ="ip")]
		public string Ip { get; set; }

		[DataMember(Name ="node")]
		public string Node { get; set; }

		[DataMember(Name ="port")]
		public long Port { get; set; }

		[DataMember(Name ="pid")]
		public long ProcessId { get; set; }

		[DataMember(Name ="value")]
		public string Value { get; set; }
	}
}
