// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[StringEnum]
	public enum DistanceUnit
	{
		[EnumMember(Value = "in")]
		Inch,

		[EnumMember(Value = "ft")]
		Feet,

		[EnumMember(Value = "yd")]
		Yards,

		[EnumMember(Value = "mi")]
		Miles,

		[EnumMember(Value = "nmi")]
		[AlternativeEnumMember("NM")]
		NauticalMiles,

		[EnumMember(Value = "km")]
		Kilometers,

		[EnumMember(Value = "m")]
		Meters,

		[EnumMember(Value = "cm")]
		Centimeters,

		[EnumMember(Value = "mm")]
		Millimeters
	}
}
