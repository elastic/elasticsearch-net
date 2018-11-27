using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// A token filter of type length that removes words that are too long or too short for the stream.
	/// </summary>
	public interface ILengthTokenFilter : ITokenFilter
	{
		/// <summary>
		/// The maximum number. Defaults to Integer.MAX_VALUE.
		/// </summary>
		[DataMember(Name ="max")]
		int? Max { get; set; }

		/// <summary>
		/// The minimum number. Defaults to 0.
		/// </summary>
		[DataMember(Name ="min")]
		int? Min { get; set; }
	}

	/// <inheritdoc />
	public class LengthTokenFilter : TokenFilterBase, ILengthTokenFilter
	{
		public LengthTokenFilter() : base("length") { }

		/// <inheritdoc />
		public int? Max { get; set; }

		/// <inheritdoc />
		public int? Min { get; set; }
	}

	/// <inheritdoc />
	public class LengthTokenFilterDescriptor
		: TokenFilterDescriptorBase<LengthTokenFilterDescriptor, ILengthTokenFilter>, ILengthTokenFilter
	{
		protected override string Type => "length";
		int? ILengthTokenFilter.Max { get; set; }

		int? ILengthTokenFilter.Min { get; set; }

		/// <inheritdoc />
		public LengthTokenFilterDescriptor Min(int? minimum) => Assign(a => a.Min = minimum);

		/// <inheritdoc />
		public LengthTokenFilterDescriptor Max(int? maximum) => Assign(a => a.Max = maximum);
	}
}
