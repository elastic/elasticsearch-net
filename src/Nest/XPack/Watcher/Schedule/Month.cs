// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum Month
	{
		[EnumMember(Value = "january")]
		January,

		[EnumMember(Value = "february")]
		February,

		[EnumMember(Value = "march")]
		March,

		[EnumMember(Value = "april")]
		April,

		[EnumMember(Value = "may")]
		May,

		[EnumMember(Value = "june")]
		June,

		[EnumMember(Value = "july")]
		July,

		[EnumMember(Value = "august")]
		August,

		[EnumMember(Value = "september")]
		September,

		[EnumMember(Value = "october")]
		October,

		[EnumMember(Value = "november")]
		November,

		[EnumMember(Value = "december")]
		December
	}
}
