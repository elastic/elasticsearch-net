namespace Nest
{
	/// <summary>
	///The kstem token filter is a high performance filter for english. 
	///<para> All terms must already be lowercased (use lowercase filter) for this filter to work correctly.</para>
	/// </summary>
	public interface IKStemTokenFilter : ITokenFilter { }
	/// <inheritdoc/>
	public class KStemTokenFilter : TokenFilterBase, IKStemTokenFilter
	{
		public KStemTokenFilter() : base("kstem") { }
	}
	///<inheritdoc/>
	public class KStemTokenFilterDescriptor 
		: TokenFilterDescriptorBase<KStemTokenFilterDescriptor, IKStemTokenFilter>, IKStemTokenFilter
	{
		protected override string Type => "kstem";
	}

}