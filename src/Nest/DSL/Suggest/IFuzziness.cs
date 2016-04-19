using System;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(FuzzinessConverter))]
	public interface IFuzziness
	{
		bool Auto { get;  }
		int? EditDistance { get;  }

		[Obsolete("Deprecated in Elasticsearch 2.0")]
		double? Ratio { get;  }
	}
}