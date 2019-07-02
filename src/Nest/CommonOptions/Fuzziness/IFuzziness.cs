using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(FuzzinessInterfaceFormatter))]
	public interface IFuzziness
	{
		bool Auto { get; }
		int? Low { get; }
		int? High { get; }
		int? EditDistance { get; }
		double? Ratio { get; }
	}
}
