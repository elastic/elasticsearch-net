using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Transforms are used to process Unicode text in many different ways, such as case mapping,
	/// normalization, transliteration and bidirectional text handling.
	/// Part of the `analysis-icu` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-icu.html
	/// </summary>
	public interface IIcuTransformTokenFilter : ITokenFilter
	{
		/// <summary>
		/// Specify text direction with the dir parameter which accepts forward (default) for LTR and reverse for RTL.
		/// </summary>
		[JsonProperty("dir")]
		IcuTransformDirection? Direction { get; set; }

		[JsonProperty("id")]
		string Id { get; set; }
	}

	/// <inheritdoc/>
	public class IcuTransformTokenFilter : TokenFilterBase, IIcuTransformTokenFilter
	{
		public IcuTransformTokenFilter() : base("icu_transform") { }

		/// <inheritdoc/>
		public IcuTransformDirection? Direction { get; set; }

		public string Id { get; set; }
	}

	///<inheritdoc/>
	public class IcuTransformTokenFilterDescriptor
		: TokenFilterDescriptorBase<IcuTransformTokenFilterDescriptor, IIcuTransformTokenFilter>, IIcuTransformTokenFilter
	{
		protected override string Type => "icu_transform";

		IcuTransformDirection? IIcuTransformTokenFilter.Direction { get; set; }
		string IIcuTransformTokenFilter.Id { get; set; }

		///<inheritdoc/>
		public IcuTransformTokenFilterDescriptor Direction(IcuTransformDirection? direction) => Assign(a => a.Direction = direction);

		///<inheritdoc/>
		public IcuTransformTokenFilterDescriptor Id(string id) => Assign(a => a.Id = id);
	}
}
