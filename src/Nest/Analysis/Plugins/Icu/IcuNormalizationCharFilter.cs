using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Normalizes as defined here: http://userguide.icu-project.org/transforms/normalization
	/// Part of the `analysis-icu` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-icu.html
	/// </summary>
	public interface IIcuNormalizationCharFilter : ICharFilter
	{
		/// <summary>
		/// The type of normalization
		/// </summary>
		[JsonProperty("name")]
		IcuNormalizationType? Name { get; set; }

		/// <summary>
		/// Set the mode parameter to decompose to convert nfc to nfd or nfkc to nfkd respectively
		/// </summary>
		[JsonProperty("mode")]
		IcuNormalizationMode? Mode { get; set; }
	}
	/// <inheritdoc/>
	public class IcuNormalizationCharFilter : CharFilterBase, IIcuNormalizationCharFilter
	{
		public IcuNormalizationCharFilter() : base("icu_normalizer") { }

		/// <inheritdoc/>
		public IcuNormalizationType? Name { get; set; }

		/// <inheritdoc/>
		public IcuNormalizationMode? Mode { get; set; }
	}

	/// <inheritdoc/>
	public class IcuNormalizationCharFilterDescriptor
		: CharFilterDescriptorBase<IcuNormalizationCharFilterDescriptor, IIcuNormalizationCharFilter>, IIcuNormalizationCharFilter
	{
		protected override string Type => "icu_normalizer";
		IcuNormalizationType? IIcuNormalizationCharFilter.Name { get; set; }
		IcuNormalizationMode? IIcuNormalizationCharFilter.Mode { get; set; }

		/// <inheritdoc/>
		public IcuNormalizationCharFilterDescriptor Name(IcuNormalizationType? name = null) =>
			Assign(a => a.Name = name);

		/// <inheritdoc/>
		public IcuNormalizationCharFilterDescriptor Mode(IcuNormalizationMode? mode = null) =>
			Assign(a => a.Mode = mode);

	}
}
