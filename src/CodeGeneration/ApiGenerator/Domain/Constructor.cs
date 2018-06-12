using CsQuery.ExtensionMethods.Internal;

namespace ApiGenerator.Domain
{
	public class Constructor
	{
		public string Generated { get; set; }
		public string Body { get; set; }
		public string Url { get; set; }
		public string Description { get; set; }
		public string AdditionalCode { get; set; } = string.Empty;
	}

	public class FluentRouteSetter
	{
		public string Code { get; set; }
		public string XmlDoc { get; set; }
	}
}
