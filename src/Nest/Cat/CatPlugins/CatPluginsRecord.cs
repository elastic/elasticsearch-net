// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class CatPluginsRecord : ICatRecord
	{
		[DataMember(Name ="component")]
		public string Component { get; set; }

		[DataMember(Name ="description")]
		public string Description { get; set; }

		[DataMember(Name ="id")]
		public string Id { get; set; }

		[DataMember(Name ="isolation")]
		public string Isolation { get; set; }

		[DataMember(Name ="name")]
		public string Name { get; set; }

		[DataMember(Name ="type")]
		public string Type { get; set; }

		[DataMember(Name ="url")]
		public string Url { get; set; }

		[DataMember(Name ="version")]
		public string Version { get; set; }
	}
}
