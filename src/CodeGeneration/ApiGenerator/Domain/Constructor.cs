namespace ApiGenerator.Domain
{
	public class Constructor
	{
		public string AdditionalCode { get; set; } = string.Empty;
		public string Description { get; set; }
		public string Generated { get; set; }
		public string Url { get; set; }
	}

	public class FluentRouteSetter
	{
		public string Code { get; set; }
		public string XmlDoc { get; set; }
	}
}
