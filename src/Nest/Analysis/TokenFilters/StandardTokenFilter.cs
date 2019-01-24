using System;

namespace Nest
{
	/// <summary>
	/// A token filter of type standard that normalizes tokens extracted with the Standard Tokenizer.
	/// </summary>
	[Obsolete(" The `standard` token filter has been deprecated because it doesn't change anything in the stream. It will be removed in 7.0.")]
	public interface IStandardTokenFilter : ITokenFilter { }

	/// <inheritdoc />
	[Obsolete(" The `standard` token filter has been deprecated because it doesn't change anything in the stream. It will be removed in 7.0.")]
	public class StandardTokenFilter : TokenFilterBase, IStandardTokenFilter
	{
		public StandardTokenFilter() : base("standard") { }
	}

	/// <inheritdoc />
	[Obsolete(" The `standard` token filter has been deprecated because it doesn't change anything in the stream. It will be removed in 7.0.")]
	public class StandardTokenFilterDescriptor
		: TokenFilterDescriptorBase<StandardTokenFilterDescriptor, IStandardTokenFilter>, IStandardTokenFilter
	{
		protected override string Type => "standard";
	}
}
