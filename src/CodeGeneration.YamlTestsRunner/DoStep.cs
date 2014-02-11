using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using YamlDotNet.Dynamic;

namespace CodeGeneration.YamlTestsRunner
{
	public class DoStep : ITestStep
	{

		public string Type { get { return "do"; } }
		public string Call { get; set; }
		public string Catch { get; set; }

		private string _rawElasticSearchCall = null;
		public string RawElasticSearchCall
		{
			get
			{
				if (!_rawElasticSearchCall.IsNullOrEmpty())
					return _rawElasticSearchCall;
				_rawElasticSearchCall = FindBestRawElasticSearchMatch();
				return _rawElasticSearchCall;
			}
		}
		public object Body { get; set; }
		public NameValueCollection QueryString { get; set; }

		private string FindBestRawElasticSearchMatch()
		{
			if (this.Call == "create")
			{
				this.Call = "index";
				if (this.QueryString == null)
					this.QueryString = new NameValueCollection();
				this.QueryString.Add("op_type", "create");
			}

			var re = "^" + this.Call.ToPascalCase() + @"(Get|Put|Head|Post|Delete)?\(";
			var calls = YamlTestsGenerator.RawElasticCalls
				.Where(c => Regex.IsMatch(c, re))
				.OrderByDescending(QueryStringCount)
				.ThenByDescending(MethodPreference)
				.ToList();

			var call = calls.FirstOrDefault();
			if (call == null)
			{
				//todo figure out what to do here.
				return string.Empty;
			}
			return this.GenerateCall(call);
		}

	

		private string GenerateCall(string call)
		{
			var s = "this.Do(()=> this._client.";
			var csharpMethod = call.Split('(').First();
			s += csharpMethod + "(";
			var csharpArguments = CsharpArguments(call);
			var args = csharpArguments
				.Select(this.GetQueryStringValue)
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
					(current, k) => current + string.Format("\t\t\t\t\t.Add(\"{0}\",{1})\r\n", k, this.GetQueryStringValue(k)));
				nv += "\t\t\t\t";
				args.Add(nv);
			}

			s += string.Join(", ", args);
			s += "));";
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
		private string GetQueryStringValue(string key)
		{
			var value = this.QueryString[key];
			if (value.StartsWith("$"))
				return value.Replace("$", "");
			return "\"" + value + "\"";
		}

		private IEnumerable<string> CsharpArguments(string call, bool inverse = false)
		{
			var csharpArguments = this.QueryString.AllKeys
				.Select(k => new {Key = k, Index = call.IndexOf(k + ",", System.StringComparison.Ordinal)})
				.Where(ki => inverse ? ki.Index < 0 : ki.Index >= 0)
				.OrderBy(ki => ki.Index)
				.Select(ki => ki.Key);
			return csharpArguments.ToList();
		}

		private int QueryStringCount(string method)
		{
			return QueryString.AllKeys.Count(k => method.Contains(k + ","));
		}
		private int MethodPreference(string method)
		{
			var postBoost = this.Body != null ? 10 : 0;
			var getBoost = this.Body == null ? 10 : 0;

			if (method.Contains("Post(")) return 5 + postBoost;
			if (method.Contains("Put(")) return 4 + postBoost;
			if (method.Contains("Get(")) return 3 + getBoost;
			if (method.Contains("Head(")) return 2 + postBoost;
			return 0;
		}
	}
}