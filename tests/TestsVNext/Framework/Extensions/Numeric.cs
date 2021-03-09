// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Tests.Framework.Extensions
{
	internal static class NumericExtensions
	{
		public static string ToOrdinal(this int num)
		{
			if (num <= 0) return num.ToString();

			switch (num % 100)
			{
				case 11:
				case 12:
				case 13:
					return num + "th";
			}

			switch (num % 10)
			{
				case 1:
					return num + "st";
				case 2:
					return num + "nd";
				case 3:
					return num + "rd";
				default:
					return num + "th";
			}
		}
	}
}
