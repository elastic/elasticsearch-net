using Newtonsoft.Json;

namespace Nest
{
	public interface IPolicy
	{
		[JsonProperty("phases")]
		Phases Phases { get; set; }
	}

	public class Policy : IPolicy
	{
		public Phases Phases { get; set; }
	}
}
