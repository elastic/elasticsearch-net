namespace Nest
{
	/// <summary>
	/// A char filter of type html_strip stripping out HTML elements from an analyzed text.
	/// </summary>
	public interface IHtmlStripCharFilter : ICharFilter { }
	/// <inheritdoc/>
	public class HtmlStripCharFilter : CharFilterBase, IHtmlStripCharFilter
	{
		public HtmlStripCharFilter() : base("html_strip") { }
	}

	/// <inheritdoc/>
	public class HtmlStripCharFilterDescriptor 
		: CharFilterDescriptorBase<HtmlStripCharFilterDescriptor, IHtmlStripCharFilter>, IHtmlStripCharFilter
	{
		protected override string Type => "html_strip";
	}

}