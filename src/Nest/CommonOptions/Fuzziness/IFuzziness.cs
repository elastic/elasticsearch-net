using System.Runtime.Serialization;
using Utf8Json;

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
