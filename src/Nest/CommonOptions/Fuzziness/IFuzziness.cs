using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(FuzzinessJsonConverter))]
	public interface IFuzziness
	{
		bool Auto { get;  }
		int? EditDistance { get;  }

		[Obsolete("Deprecated. Setting this is a noop")]
		double? Ratio { get;  }
	}
}
