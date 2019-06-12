using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ApiGenerator.Domain.Specification;

namespace ApiGenerator.Domain.Code.LowLevel
{
	public class LowLevelClientMethod
	{
		public CsharpNames CsharpNames { get; set; }

		public string Arguments { get; set; }
		public string OfficialDocumentationLink { get; set; }
		public string PerPathMethodName { get; set; }
		public string HttpMethod { get; set; }

		public DeprecatedPath DeprecatedPath { get; set; }
		public UrlInformation Url { get; set; }
		public bool HasBody { get; set; }
		public IEnumerable<UrlPart> Parts { get; set; }
		public string Path { get; set; }


		public string UrlInCode
		{
			get
			{
				string Evaluator(Match m)
				{
					var arg = m.Groups.Last().Value.ToCamelCase();
					return $"{{{arg}:{arg}}}";
				}

				var url = Path.TrimStart('/');
				var pattern = string.Join("|", Url.OriginalParts.Select(p => p.Key));
				var urlCode = $"\"{url}\"";
				if (Path.Contains("{"))
				{
					var patchedUrl = Regex.Replace(url, "{(" + pattern + ")}", Evaluator);
					urlCode = $"Url($\"{patchedUrl}\")";
				}
				return urlCode;
			}
		}
	}
}
