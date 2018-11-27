using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Normalizes as defined here: http://userguide.icu-project.org/transforms/normalization
	/// Part of the `analysis-icu` plugin: https://www.elastic.co/guide/en/elasticsearch/plugins/current/analysis-icu.html
	/// </summary>
	public interface IIcuNormalizationCharFilter : ICharFilter
	{
		/// <summary>
		/// Set the mode parameter to decompose to convert nfc to nfd or nfkc to nfkd respectively
		/// </summary>
		[DataMember(Name ="mode")]
		IcuNormalizationMode? Mode { get; set; }

		/// <summary>
		/// The type of normalization
		/// </summary>
		[DataMember(Name ="name")]
		IcuNormalizationType? Name { get; set; }
	}

	/// <inheritdoc />
	public class IcuNormalizationCharFilter : CharFilterBase, IIcuNormalizationCharFilter
	{
		public IcuNormalizationCharFilter() : base("icu_normalizer") { }

		/// <inheritdoc />
		public IcuNormalizationMode? Mode { get; set; }

		/// <inheritdoc />
		public IcuNormalizationType? Name { get; set; }
	}

	/// <inheritdoc />
	public class IcuNormalizationCharFilterDescriptor
		: CharFilterDescriptorBase<IcuNormalizationCharFilterDescriptor, IIcuNormalizationCharFilter>, IIcuNormalizationCharFilter
	{
		protected override string Type => "icu_normalizer";
		IcuNormalizationMode? IIcuNormalizationCharFilter.Mode { get; set; }
		IcuNormalizationType? IIcuNormalizationCharFilter.Name { get; set; }

		/// <inheritdoc />
		public IcuNormalizationCharFilterDescriptor Name(IcuNormalizationType? name = null) =>
			Assign(a => a.Name = name);

		/// <inheritdoc />
		public IcuNormalizationCharFilterDescriptor Mode(IcuNormalizationMode? mode = null) =>
			Assign(a => a.Mode = mode);
	}
}
