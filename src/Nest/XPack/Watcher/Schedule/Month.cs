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
		[EnumMember(Value = "JAN")]
		January,

		[EnumMember(Value = "FEB")]
		February,

		[EnumMember(Value = "MAR")]
		March,

		[EnumMember(Value = "APR")]
		April,

		[EnumMember(Value = "MAY")]
		May,

		[EnumMember(Value = "JUN")]
		June,

		[EnumMember(Value = "JUL")]
		July,

		[EnumMember(Value = "AUG")]
		August,

		[EnumMember(Value = "SEP")]
		September,

		[EnumMember(Value = "OCT")]
		October,

		[EnumMember(Value = "NOV")]
		November,

		[EnumMember(Value = "DEC")]
		December
	}
}
