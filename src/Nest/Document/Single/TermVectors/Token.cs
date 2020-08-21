// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class Token
	{
		[DataMember(Name ="end_offset")]
		public int EndOffset { get; internal set; }

		[DataMember(Name ="payload")]
		public string Payload { get; internal set; }

		[DataMember(Name ="position")]
		public int Position { get; internal set; }

		[DataMember(Name ="start_offset")]
		public int StartOffset { get; internal set; }
	}
}
