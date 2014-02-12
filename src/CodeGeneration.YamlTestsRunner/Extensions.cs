using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace CodeGeneration.YamlTestsRunner
{
	public static class Extensions
	{
		public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
		{
			return items.GroupBy(property).Select(x => x.First());
		}

		public static string ToPascalCase(this string s)
		{
			var textInfo = new CultureInfo("en-US").TextInfo;
			s = textInfo.ToTitleCase(s.ToLowerInvariant()).Replace("_", string.Empty).Replace(".", string.Empty);
			s = Regex.Replace(s, @"\W+", string.Empty);
			return s;
		}

		public static string ToStringRepresentation(this object o, string indendation ="\t\t\t\t")
		{
			if (o == null)
				return null;
			var body = "";
			var s = o as string;
			if (s != null)
			{
				int i;
				if (int.TryParse(s, out i))
				{
					body += i.ToString(CultureInfo.InvariantCulture);
					return body;
				}
				body += s.SurroundWithQuotes()
					//yaml deserializer messes these up
					.Replace("Ã¤Â¸Â­Ã¦â€“â€¡", "ä¸­æ–‡");
				if (body.StartsWith("@\"$"))
					return "(string)" + body.Replace("@\"$", "").Trim('"');
				return body;
			}
			var ss = o as IEnumerable<string>;
			if (ss != null)
			{
				body += "new string [] {\n";
				body +=  string.Join(",\n", ss.Select(str=>indendation + "\t" + str.SurroundWithQuotes()));
				body += "\n" + indendation + "}";
				return body;
			}
			
			var si = o as IEnumerable<int>;
			if (si != null)
			{
				body +=  "new int [] {\n";
				body +=  string.Join(",\n", si.Select(str=>indendation + "\t" + si.ToString()));
				body += "\n" + indendation + "}";
				return body;
			}
			
			var os = o as IEnumerable<object>;
			if (os != null)
			{
				var inner =  string.Join(",\n", os
					.Select(oss=>indendation + "\t" + oss.SerializeToAnonymousObject(indendation, Formatting.None)));
				if (inner.StartsWith(indendation + "\tnew {"))
					body += "new dynamic[] {\n";
				else 
					body += "new [] {\n";
				body += inner;
				body += "\n" + indendation + "}";
				return body;
			}
			body += o.SerializeToAnonymousObject();
			return body;
		}

		public static string SerializeToAnonymousObject(this object o, string indentation = "\t\t\t\t", Formatting format = Formatting.Indented)
		{
			if (o is string)
				return o.ToString().SurroundWithQuotes();

			var serializer = new JsonSerializer() { Formatting = format };
			var stringWriter = new StringWriter();
			var writer = new JsonTextWriter(stringWriter);
			writer.QuoteName = false;
			serializer.Serialize(writer, o);
			writer.Close();
			//anonymousify the json
			var anon = stringWriter.ToString().Replace("{", "new {").Replace("]", "}").Replace("[", "new [] {").Replace(":", "=");
			//match indentation of the view	
			anon = Regex.Replace(anon, @"^(\s+)?", (m) =>
			{
				if (m.Index == 0)
					return m.Value;
				return "\t\t\t\t" + m.Value.Replace("  ", "\t");
			}, RegexOptions.Multiline);
			//escape c# keywords in the anon object
			anon = anon.Replace("default=", "@default=").Replace("params=", "@params=");
			//docs contain different types of anon objects, quick fix by making them a dynamic[]
			anon = anon.Replace("docs= new []", "docs= new dynamic[]");
			//fix empty untyped arrays, default to string
			anon = anon.Replace("new [] {}", "new string[] {}");
			//quick fixes for settings: index.* and discovery.zen.*
			//needs some recursive regex love perhaps in the future
			anon = Regex.Replace(anon, @"(index|discovery)\.([^=]+)=([^\r\n,]+)", " { \"$1.$2\", $3 }", RegexOptions.Multiline);
			anon = Regex.Replace(anon, @"new \{(\r\n\s+\{ "")(index|discovery)", "new Dictionary<string, object> {$1$2", RegexOptions.Multiline);
			return anon;
		}

		public static string EscapeQuotes(this string s)
		{
			return s.Replace("\"", "\"\"");
		}

		public static string SurroundWithQuotes(this string s)
		{
			return "@\"" + s.EscapeQuotes() + "\"";
		}
	}
}