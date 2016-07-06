using System.Collections.Generic;

namespace ApiGenerator.Domain
{
	public class Constructor
	{
		public string Generated { get; set; }
		public string Url { get; set; }
		public string Description { get; set; }
		public string AdditionalCode { get; set;}
	}

	public class FluentRouteSetter
	{
		public string Code { get; set; }
		public string XmlDoc { get; set; }
	}
}