using Newtonsoft.Json;
using Utf8Json;

namespace Nest
{
	[JsonConverter(typeof(FuzzinessJsonConverter))]
	[JsonFormatter(typeof(FuzzinessFormatter))]
	public interface IFuzziness
	{
		bool Auto { get; }
		int? EditDistance { get; }
		double? Ratio { get; }
	}
}
