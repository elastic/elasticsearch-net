using System;
using System.Collections.Generic;
using System.Linq;
using CsQuery.ExtensionMethods.Internal;

namespace ApiGenerator.Domain
{
	public static class CodeGenerator
	{
		public static string PropertyGenerator(string type, string name, string key, string setter) =>
			$"public {type} {name} {{ get => Q<{type}>(\"{key}\"); set => Q(\"{key}\", {setter}); }}";

		public static string Property(string type, string name, string key, string setter, string obsolete, params string[] doc)
		{
			var components = new List<string>();
			foreach (var d in RenderDocumentation(doc)) A(d);
			if (!string.IsNullOrWhiteSpace(obsolete)) A($"[Obsolete(\"Scheduled to be removed in 7.0, {obsolete}\")]");

			A(PropertyGenerator(type, name, key, setter));
			return string.Join("\r\n\t\t", components);

			void A(string s) => components.Add(s);
		}

		public static string Constructor(Constructor c)
		{
			var components = new List<string>();
			if (!c.Description.IsNullOrEmpty()) A(c.Description);
			var generated = c.Generated;
			if (c.Body.IsNullOrEmpty()) generated += "{}";
			A(generated);
			if (!c.Body.IsNullOrEmpty()) A(c.Body);
			if (!c.AdditionalCode.IsNullOrEmpty()) A(c.AdditionalCode);
			return string.Join("\r\n\t\t", components);
			void A(string s) => components.Add(s);
		}



		private static IEnumerable<string> RenderDocumentation(params string[] doc)
		{
			doc = (doc?.SelectMany(WrapDocumentation) ?? Enumerable.Empty<string>()).ToArray();
			switch (doc.Length)
			{
				case 0: yield break;
				case 1:
					yield return ($"///<summary>{doc[0]}</summary>");
					yield break;
				default:
					yield return "///<summary>";
					foreach (var d in doc) yield return $"/// {d}";
					yield return "///</summary>";
					yield break;
			}
		}
		private static string[] WrapDocumentation(string documentation)
		{
			const int max = 140;
			var lines = documentation.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
			var charCount = 0;
			return lines.GroupBy(Wrap).Select(l => string.Join(" ", l.ToArray())).ToArray();
			int Wrap(string w)
			{
				var increase = (charCount % max + w.Length + 1 >= max ? max - (charCount % max) : 0);
				return (charCount += increase + w.Length + 1) / max;
			}
		}
	}
}
