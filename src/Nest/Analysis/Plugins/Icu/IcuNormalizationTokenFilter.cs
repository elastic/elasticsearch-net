using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Normalizes as defined here: http://userguide.icu-project.org/transforms/normalization
	/// </summary>
	/// <remarks>
	/// Requires analysis-icu plugin to be installed
	/// </remarks>
	public interface IIcuNormalizationTokenFilter : ITokenFilter
	{
		/// <summary>
		/// The type of normalization
		/// </summary>
		[DataMember(Name ="name")]
		IcuNormalizationType? Name { get; set; }
	}

	/// <inheritdoc />
	public class IcuNormalizationTokenFilter : TokenFilterBase, IIcuNormalizationTokenFilter
	{
		public IcuNormalizationTokenFilter() : base("icu_normalizer") { }

		/// <inheritdoc />
		public IcuNormalizationType? Name { get; set; }
	}

	/// <inheritdoc />
	public class IcuNormalizationTokenFilterDescriptor
		: TokenFilterDescriptorBase<IcuNormalizationTokenFilterDescriptor, IIcuNormalizationTokenFilter>, IIcuNormalizationTokenFilter
	{
		protected override string Type => "icu_normalizer";

		IcuNormalizationType? IIcuNormalizationTokenFilter.Name { get; set; }

		/// <inheritdoc />
		public IcuNormalizationTokenFilterDescriptor Name(IcuNormalizationType? name) => Assign(name, (a, v) => a.Name = v);
	}
}
