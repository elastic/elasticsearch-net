namespace Nest
{
	/// <summary>
	/// A char filter of type html_strip stripping out HTML elements from an analyzed text.
	/// </summary>
	public class HtmlStripCharFilter : CharFilterBase
	{
		public HtmlStripCharFilter()
			: base("html_strip")
		{

		}
	}
}