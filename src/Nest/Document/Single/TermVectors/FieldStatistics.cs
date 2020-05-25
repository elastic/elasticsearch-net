// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class FieldStatistics
	{
		[DataMember(Name ="doc_count")]
		public int DocumentCount { get; internal set; }

		[DataMember(Name ="sum_doc_freq")]
		public long SumOfDocumentFrequencies { get; internal set; }

		[DataMember(Name ="sum_ttf")]
		public long SumOfTotalTermFrequencies { get; internal set; }
	}
}
