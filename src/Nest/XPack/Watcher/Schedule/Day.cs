// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum Day
	{
		[EnumMember(Value = "SUN")]
		Sunday,

		[EnumMember(Value = "MON")]
		Monday,

		[EnumMember(Value = "TUE")]
		Tuesday,

		[EnumMember(Value = "WED")]
		Wednesday,

		[EnumMember(Value = "THU")]
		Thursday,

		[EnumMember(Value = "FRI")]
		Friday,

		[EnumMember(Value = "SAT")]
		Saturday
	}
}
