// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[StringEnum]
	public enum DateMathOperation
	{
		[EnumMember(Value = "+")]
		Add,

		[EnumMember(Value = "-")]
		Subtract
	}

	public static class DateMathOperationExtensions
	{
		public static string GetStringValue(this DateMathOperation value)
		{
			switch (value)
			{
				case DateMathOperation.Add:
					return "+";
				case DateMathOperation.Subtract:
					return "-";
				default:
					throw new ArgumentOutOfRangeException(nameof(value), value, null);
			}
		}
	}
}
