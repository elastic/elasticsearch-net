// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum TermVectorOption
	{
		[EnumMember(Value = "no")]
		No,

		[EnumMember(Value = "yes")]
		Yes,

		[EnumMember(Value = "with_offsets")]
		WithOffsets,

		[EnumMember(Value = "with_positions")]
		WithPositions,

		[EnumMember(Value = "with_positions_offsets")]
		WithPositionsOffsets,

		[EnumMember(Value = "with_positions_offsets_payloads")]
		WithPositionsOffsetsPayloads
	}
}
