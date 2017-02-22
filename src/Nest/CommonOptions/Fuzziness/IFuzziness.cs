using System;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonConverter(typeof(FuzzinessJsonConverter))]
	public interface IFuzziness
	{
		bool Auto { get;  }
		int? EditDistance { get;  }
		double? Ratio { get;  }
	}
}
