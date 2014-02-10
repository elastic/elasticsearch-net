using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FubuCore.Logging;
using FubuCore.Reflection;
using FubuCsProjFile.Templating.Runtime;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using YamlDotNet.Dynamic;

namespace CodeGeneration.YamlTestsRunner
{
	using YamlTestSuite = Dictionary<string, object>;

	public class TestSuite
	{
		public string Description { get; set; }
		public IEnumerable<ITestStep> Steps { get; set; }


		public static TestSuite CreateFrom(YamlTestSuite untyped, string yaml)
		{
			if (untyped == null || untyped.Count == 0)
				return null;

			var untypedSuite = untyped.First();
			var suite = new TestSuite();
			suite.Description = untypedSuite.Key;
			suite.Steps = Actions(untypedSuite).ToList();
			return suite;

		}

		private static IEnumerable<ITestStep> Actions(KeyValuePair<string, object> untypedSuite)
		{
			var actions = untypedSuite.Value as List<object>;
			if (actions == null)
			{
				//TODO figure out what to do
				yield break;
			}

			foreach (var actionObject in actions)
			{
				var actionDictionary = actionObject as Dictionary<object, object>;
				if (actionDictionary == null || actionDictionary.Count == 0)
				{
					//TODO figure out what to do here
					continue;
				}
				var kv = actionDictionary.First();
				var testAction = kv.Key.ToString();
				switch (testAction)
				{
					case "do":
						yield return CreateDoStep(kv.Value as Dictionary<object, object>);
						break;
				}

			}
		}

		private static DoStep CreateDoStep(Dictionary<object, object> value)
		{
			if (value == null)
				return null;
			var o = value.First();
			var call = o.Key as string;
			string catchException = null;
			if (call == "catch")
			{
				catchException = o.Value as string;
				o = value.Last();
			}
			dynamic d = o;
			call = o.Key as string;
			var arguments = d.Value;
			var argumentString = arguments as string;
			if (argumentString != null)
			{
				return new DoStep {Call = call, Body = argumentString, Catch = catchException};
			}
			var complexArgument = arguments as Dictionary<object, object>;
			if (complexArgument != null)
			{
				object body = null;
				if (complexArgument.ContainsKey("body"))
				{
					body = complexArgument["body"];
					complexArgument.Remove("body");
				}
				var nv = new NameValueCollection();
				foreach (var kv in complexArgument)
					nv.Add(kv.Key as string, kv.Value.ToString());
				return new DoStep {Call = call, Body = body, QueryString = nv, Catch = catchException};
			}
			return new DoStep { Call = call , Catch = catchException};
		}
	}

	public interface ITestStep
	{
		string Type { get; }
	}

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

		public string SerializeBody()
		{
			if (this.Body == null)
				return null;
			var body = "_body = ";
			var s = this.Body as string;
			if (s != null)
			{
				body += string.Format("@\"{0}\";", EscapeQuotes(s));
				return body;
			}
			var ss = this.Body as IEnumerable<string>;
			if (ss != null)
			{
				body += string.Format("@\"{0}\";", string.Join("\n", ss.Select(EscapeQuotes)));
				return body;
			}
			
			var os = this.Body as IEnumerable<object>;
			if (os != null)
			{

				body += string.Format("@\"{0}\";", string.Join("\n", os
					.Select(oss=>EscapeQuotes(JsonConvert.SerializeObject(oss, Formatting.None)))));
				return body;
			}
			body += this.SerializeToAnonymousObject(this.Body) + ";";
			return body;
		}

		private string SerializeToAnonymousObject(object o)
		{
			var serializer = new JsonSerializer() { Formatting = Formatting.Indented };
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
			anon = Regex.Replace(anon, @"^(\s+)(index)\.([^\.]+)=([^\r\n]+)", "$1$2= new { $3=$4 }", RegexOptions.Multiline);
			anon = Regex.Replace(anon, @"^(\s+)(discovery)\.([^\.]+)\.([^\.]+)=(.+)$", "$1$2= new { $3= new { $4= $5 } }", RegexOptions.Multiline);
			return anon;
		}

		private string EscapeQuotes(string s)
		{
			return s.Replace("\"", "\"\"");
		}

		private string GenerateCall(string call)
		{
			var s = "this._client.";
			var csharpMethod = call.Split('(').First();
			s += csharpMethod + "(";
			var csharpArguments = this.QueryString.AllKeys
				.Select(k => new {Key = k, Index = call.IndexOf(k + ",", System.StringComparison.Ordinal)})
				.Where(ki => ki.Index > 0)
				.OrderBy(ki => ki.Index)
				.Select(ki => ki.Key);
			foreach (var key in csharpArguments)
				s += "\"" + this.QueryString[key] + "\", ";
			if (this.Body != null)
			{
				s += "_body, ";
			}
			else if (call.Contains("object body,"))
			{
				s += "null, ";
			}
			s += "nv=>nv);";
			return s;
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
