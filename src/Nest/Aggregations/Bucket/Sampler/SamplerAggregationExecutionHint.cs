// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[StringEnum]
	public enum SamplerAggregationExecutionHint
	{
		[EnumMember(Value = "map")]
		Map,

		[EnumMember(Value = "global_ordinals")]
		GlobalOrdinals,

		[EnumMember(Value = "bytes_hash")]
		BytesHash
	}
}
