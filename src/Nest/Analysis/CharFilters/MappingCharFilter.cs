using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A char filter of type mapping replacing characters of an analyzed text with given mapping.
	/// </summary>
	public interface IMappingCharFilter : ICharFilter
	{
		[JsonProperty("mappings")]
		IEnumerable<string> Mappings { get; set; }

		[JsonProperty("mappings_path")]
		string MappingsPath { get; set; }
	}
	public class MappingCharFilter : CharFilterBase, IMappingCharFilter
	{
		public MappingCharFilter() : base("mapping") { }

		public IEnumerable<string> Mappings { get; set; }

		public string MappingsPath { get; set; }
	}

	public class MappingCharFilterDescriptor 
		: CharFilterDescriptorBase<MappingCharFilterDescriptor, IMappingCharFilter>, IMappingCharFilter
	{
		protected override string Type => "mapping";
		IEnumerable<string> IMappingCharFilter.Mappings { get; set; }
		string IMappingCharFilter.MappingsPath { get; set; }

		public MappingCharFilterDescriptor Mappings(params string[] mappings) =>
			Assign(a => a.Mappings = mappings);

		public MappingCharFilterDescriptor Mappings(IEnumerable<string> mappings) =>
			Assign(a => a.Mappings = mappings);

		public MappingCharFilterDescriptor MappingsPath(string path) =>
			Assign(a => a.MappingsPath = path);

	}
}