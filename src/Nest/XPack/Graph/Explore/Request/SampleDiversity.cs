using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public class SampleDiversity
	{
		[JsonProperty("field")]
		public Field Field { get; set; }

		[JsonProperty("max_docs_per_value")]
		public int? MaxDocumentsPerValue { get; set; }
	}
}
