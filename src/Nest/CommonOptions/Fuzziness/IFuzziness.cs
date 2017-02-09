using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(FuzzinessJsonConverter))]
	public interface IFuzziness
	{
		bool Auto { get;  }
		int? EditDistance { get;  }
		double? Ratio { get;  }
	}
}
