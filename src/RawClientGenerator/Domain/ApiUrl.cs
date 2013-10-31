using System.Collections.Generic;

namespace RawClientGenerator
{
	public class ApiUrl
	{
		public string Path { get; set; }
		public IEnumerable<string> Paths { get; set; }
		public IDictionary<string, ApiUrlPart> Parts { get; set; }
		public IDictionary<string, ApiQueryParameters> Params { get; set; }
	}
}