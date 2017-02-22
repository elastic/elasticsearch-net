using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elasticsearch.Net_5_2_0
{
	public static class EnumExtensions
	{
		public static string GetStringValue(this HttpMethod enumValue)
		{
			switch (enumValue)
			{
				case HttpMethod.GET: return "GET";
				case HttpMethod.POST: return "POST";
				case HttpMethod.PUT: return "PUT";
				case HttpMethod.DELETE: return "DELETE";
				case HttpMethod.HEAD: return "HEAD";
				default:
					throw new ArgumentOutOfRangeException(nameof(enumValue), enumValue, null);
			}
		}
	}
}
