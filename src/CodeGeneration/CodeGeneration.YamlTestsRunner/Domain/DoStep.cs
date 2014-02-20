using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CsQuery.ExtensionMethods.Internal;

namespace CodeGeneration.YamlTestsRunner.Domain
{
	public class DoStep : ITestStep
	{

		public string Type { get { return "do"; } }
		public string Call { get; set; }
		public string Catch { get; set; }
		public string TestDescription { get; set; }

		private string _rawElasticSearchCall = null;
		public string RawElasticSearchCall
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(_rawElasticSearchCall))
					return _rawElasticSearchCall;
				_rawElasticSearchCall = FindBestRawElasticSearchMatch();
				return _rawElasticSearchCall;
			}
		}
		public object Body { get; set; }
		public Dictionary<string, object> QueryString { get; set; }

		private string FindBestRawElasticSearchMatch()
		{
			this.PatchCall();

			var re = "^" + this.Call.ToPascalCase() + @"(Get|Put|Head|Post|Delete)?(ForAll)?\(";
			var counts = YamlTestsGenerator.RawElasticCalls
				.Where(c => Regex.IsMatch(c, re))
				.Where(c=> this.Body == null || c.Contains("object body"))
				.Select(s => new {Method = s, Count = QueryStringCount(s)})
				.OrderByDescending(s=>s.Count)
				.ToList();

			var calls = YamlTestsGenerator.RawElasticCalls
				.Where(c => Regex.IsMatch(c, re))
				.Where(c=> this.Body == null || c.Contains("object body"))
				.OrderByDescending(QueryStringCount)
				//.ThenByDescending(MethodPreference)
				.ToList();

			var call = calls.FirstOrDefault();
			if (call == null)
			{
				//todo figure out what to do here.
				return string.Empty;
			}
			return this.GenerateCall(call);
		}

		private void PatchCall()
		{
			if (this.Call == "create")
			{
				this.Call = "index";
				if (this.QueryString == null)
					this.QueryString = new Dictionary<string, object>();
				this.QueryString.Add("op_type", "create");
			}
			else if (!this.Catch.IsNullOrEmpty() &&
				(this.Call == "indices.delete_alias"
				|| this.Call == "indices.delete_warmer"
				|| this.Call == "indices.delete_mapping"
				|| this.Call == "indices.put_alias"
				|| this.Call == "indices.put_warmer"
				|| this.Call == "indices.put_mapping"))
			{
				Func<string, bool> m = (s) => Regex.IsMatch(this.TestDescription, "(blank|empty|missing) " + s);

				if (!this.QueryString.ContainsKey("index") && m("index"))
					this.QueryString["index"] = string.Empty;
				if (!this.QueryString.ContainsKey("type") && m("type"))
					this.QueryString["type"] = string.Empty;
				if (!this.QueryString.ContainsKey("name") 
					&& (m("name") || m("alias") || m("(.+_)?warmer")))
					this.QueryString["name"] = string.Empty;
			}
		}
		private double QueryStringCount(string method)
		{
			var matches = (double)this.QueryString.Keys.Count(k => method.Contains(k + ","));
			return 1 + matches / (method.Count(c=>c==','));

		}
		private string GetQueryStringValue(string key)
		{
			var value = this.QueryString[key].ToStringRepresentation("\t\t\t\t\t");

			return value;
		}
	

		private string GenerateCall(string call)
		{
			var s = "this.Do(()=> _client.";
			var csharpMethod = call.Split('(').First();
			s += csharpMethod + "(";
			var csharpArguments = CsharpArguments(call);
			var args = csharpArguments
				.Select(this.GetMethodArgument)
				.ToList();
			if (this.Body != null)
			{
				args.Add("_body");
			}
			else if (call.Contains("object body,"))
			{
				args.Add("null");
			}
			var queryStringKeys = this.CsharpArguments(call, inverse: true);
			if (queryStringKeys.Any())
			{
				var nv = "nv=>nv\r\n";
				nv += queryStringKeys.Aggregate("",
					(current, k) => current + string.Format("\t\t\t\t\t.Add(\"{0}\", {1})\r\n", k, this.GetQueryStringValue(k)));
				nv += "\t\t\t\t";
				args.Add(nv);
			}

			s += string.Join(", ", args);
			s += ")";
			if (!string.IsNullOrEmpty(this.Catch))
				s += ", shouldCatch: " + this.Catch.ToStringRepresentation();
			s += ");";
			return s;
		}
	

		public static string SerializeBody(DoStep o)
		{
			if (o == null || o.Body == null)
				return null;
			var body = "_body = ";
			body += o.Body.ToStringRepresentation();
			body += ";\n";
			return body;
		}
		private string GetMethodArgument(string key)
		{
			var value = this.QueryString[key];
			string v = string.Empty;
			if (value is IEnumerable<object>)
			{
				var os = value as IEnumerable<object>;
				v = string.Join(",", os
					.Select(oss => oss
						.ToString()
					)
					);
			}
			else v = value.ToString();
			v = v.Replace("Ã¤Â¸Â­Ã¦â€“â€¡", "ä¸­æ–‡");
			if (v.StartsWith("$"))
				return "(string)" + v.Replace("$", "");
			return "\"" + v + "\"";
		}
		

		private IEnumerable<string> CsharpArguments(string call, bool inverse = false)
		{
			var csharpArguments = this.QueryString.Keys
				.Select(k => new {Key = k, Index = call.IndexOf(k + ",", System.StringComparison.Ordinal)})
				.Where(ki => inverse ? ki.Index < 0 : ki.Index >= 0)
				.OrderBy(ki => ki.Index)
				.Select(ki => ki.Key);
			return csharpArguments.ToList();
		}
	
	}
}