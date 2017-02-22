using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public class ExtendedBounds<T>
	{
		[JsonProperty("min")]
		public T Minimum { get; set; }

		[JsonProperty("max")]
		public T Maximum { get; set; }
	}
}