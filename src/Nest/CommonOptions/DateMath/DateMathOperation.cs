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
