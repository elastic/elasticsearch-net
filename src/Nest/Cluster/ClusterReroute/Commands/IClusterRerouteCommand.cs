using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[ContractJsonConverter(typeof(ClusterRerouteCommandJsonConverter))]
	public interface IClusterRerouteCommand
	{
		[JsonIgnore]
		string Name { get; }
	}
}