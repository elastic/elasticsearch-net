using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FubuCore.Logging;
using FubuCore.Reflection;
using FubuCsProjFile.Templating.Runtime;
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
				s += "\"SERIALIZED BODY HERE\", ";
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
			if (method.Contains("Post(")) return 5;
			if (method.Contains("Put(")) return 4;
			if (method.Contains("Get(")) return 3;
			if (method.Contains("Head(")) return 2;
			return 0;
		}
	} 
}
