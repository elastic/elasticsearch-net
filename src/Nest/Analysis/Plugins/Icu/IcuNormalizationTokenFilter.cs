using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Normalizes as defined here: http://userguide.icu-project.org/transforms/normalization
	/// Part of the `analysis-icu` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-icu.html
	/// </summary>
	public interface IIcuNormalizationTokenFilter : ITokenFilter
	{
		/// <summary>
		/// The type of normalization
		/// </summary>
		[JsonProperty("name")]
		IcuNormalizationType? Name { get; set; }
	}

	/// <inheritdoc/>
	public class IcuNormalizationTokenFilter : TokenFilterBase, IIcuNormalizationTokenFilter
	{
		public IcuNormalizationTokenFilter() : base("icu_normalizer") { }

		/// <inheritdoc/>
		public IcuNormalizationType? Name { get; set; }
	}

	///<inheritdoc/>
	public class IcuNormalizationTokenFilterDescriptor
		: TokenFilterDescriptorBase<IcuNormalizationTokenFilterDescriptor, IIcuNormalizationTokenFilter>, IIcuNormalizationTokenFilter
	{
		protected override string Type => "icu_normalizer";

		IcuNormalizationType? IIcuNormalizationTokenFilter.Name { get; set; }

		///<inheritdoc/>
		public IcuNormalizationTokenFilterDescriptor Name(IcuNormalizationType? name) => Assign(a => a.Name = name);
	}
}
