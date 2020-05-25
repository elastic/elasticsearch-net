// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class AnalyzeToken
	{
		[DataMember(Name ="end_offset")]
		public long EndOffset { get; internal set; }

		[DataMember(Name ="position")]
		public long Position { get; internal set; }

		[DataMember(Name ="position_length")]
		public long? PositionLength { get; internal set; }

		[DataMember(Name ="start_offset")]
		public long StartOffset { get; internal set; }

		[DataMember(Name ="token")]
		public string Token { get; internal set; }

		[DataMember(Name ="type")]
		public string Type { get; internal set; }
	}
}
