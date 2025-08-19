// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

[JsonConverter(typeof(Json.DateMathTimeUnitConverter))]
public enum DateMathTimeUnit
{
	[EnumMember(Value = "s")]
	Second,

	[EnumMember(Value = "m")]
	Minute,

	[EnumMember(Value = "h")]
	Hour,

	[EnumMember(Value = "d")]
	Day,

	[EnumMember(Value = "w")]
	Week,

	[EnumMember(Value = "M")]
	Month,

	[EnumMember(Value = "y")]
	Year
}

public static class DateMathTimeUnitExtensions
{
	public static string GetStringValue(this DateMathTimeUnit value) =>
		value switch
		{
			DateMathTimeUnit.Second => "s",
			DateMathTimeUnit.Minute => "m",
			DateMathTimeUnit.Hour => "h",
			DateMathTimeUnit.Day => "d",
			DateMathTimeUnit.Week => "w",
			DateMathTimeUnit.Month => "M",
			DateMathTimeUnit.Year => "y",
			_ => throw new ArgumentOutOfRangeException(nameof(value), value, null),
		};
}
