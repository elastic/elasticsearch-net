// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum MultiValueMode
	{
		[EnumMember(Value = "min")]
		Min,

		[EnumMember(Value = "max")]
		Max,

		[EnumMember(Value = "avg")]
		Average,

		[EnumMember(Value = "sum")]
		Sum
	}
}
