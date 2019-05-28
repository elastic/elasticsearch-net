using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ApiGenerator.Domain
{
	public class LowLevelClientMethod
	{
		public CsharpNames CsharpNames { get; set; }

		public string Arguments { get; set; }
		public string OfficialDocumentationLink { get; set; }
		public string PerPathMethodName { get; set; }
		public string HttpMethod { get; set; }

		public string DeprecatedPath { get; set; }
		public UrlInformation Url { get; set; }
		public bool HasBody { get; set; }
		public IEnumerable<UrlPart> Parts { get; set; }
		public string Path { get; set; }


		public string UrlInCode
		{
			get
			{
				var url = Path.TrimStart('/');
				var pattern = string.Join("|", Url.Parts.Select(p => p.Name));
				var urlCode = $"\"{url}\"";
				if (Path.Contains("{"))
				{
					var patchedUrl = Regex.Replace(url, "{(" + pattern + ")}", "{$1:$1}");
					urlCode = $"Url($\"{patchedUrl}\")";
				}
				return urlCode;
			}
		}
	}
}
