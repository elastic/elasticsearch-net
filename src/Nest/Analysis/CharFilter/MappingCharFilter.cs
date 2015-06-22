using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A char filter of type mapping replacing characters of an analyzed text with given mapping.
	/// </summary>
	public class MappingCharFilter : CharFilterBase
	{
		public MappingCharFilter()
			: base("mapping")
		{

		}
		[JsonProperty("mappings")]
		public IEnumerable<string> Mappings { get; set; }

		[JsonProperty("mappings_path")]
		public string MappingsPath { get; set; }
	}

}