using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[JsonFormatter(typeof(FuzzinessInterfaceFormatter))]
	public interface IFuzziness
	{
		bool Auto { get; }
		int? EditDistance { get; }
		double? Ratio { get; }
	}
}
