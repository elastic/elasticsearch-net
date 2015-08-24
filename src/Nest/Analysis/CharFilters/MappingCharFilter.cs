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
	/// <inheritdoc/>
	public class MappingCharFilter : CharFilterBase, IMappingCharFilter
	{
		public MappingCharFilter() : base("mapping") { }

		/// <inheritdoc/>
		public IEnumerable<string> Mappings { get; set; }

		/// <inheritdoc/>
		public string MappingsPath { get; set; }
	}

	/// <inheritdoc/>
	public class MappingCharFilterDescriptor 
		: CharFilterDescriptorBase<MappingCharFilterDescriptor, IMappingCharFilter>, IMappingCharFilter
	{
		protected override string Type => "mapping";
		IEnumerable<string> IMappingCharFilter.Mappings { get; set; }
		string IMappingCharFilter.MappingsPath { get; set; }

		/// <inheritdoc/>
		public MappingCharFilterDescriptor Mappings(params string[] mappings) =>
			Assign(a => a.Mappings = mappings);

		/// <inheritdoc/>
		public MappingCharFilterDescriptor Mappings(IEnumerable<string> mappings) =>
			Assign(a => a.Mappings = mappings);

		/// <inheritdoc/>
		public MappingCharFilterDescriptor MappingsPath(string path) =>
			Assign(a => a.MappingsPath = path);

	}
}