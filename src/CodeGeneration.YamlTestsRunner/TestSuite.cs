using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FubuCore.Logging;
using FubuCore.Reflection;
using FubuCsProjFile.Templating.Runtime;
using Microsoft.SqlServer.Server;

namespace CodeGeneration.YamlTestsRunner
{
	using YamlTestSuite = Dictionary<string, object>;

	public class TestSuite
	{
		public string Description { get; set; }
		public IEnumerable<ITestStep> Steps { get; set; }
		public bool HasSetup { get; set; }
		public bool IsSetup { get; set; }

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
					case "set":
						yield return CreateSetStep(kv.Value as Dictionary<object, object>);
						break;
					case "is_true":
						yield return CreateIsTrueStep(kv.Value as string);
						break;
					case "is_false":
						yield return CreateIsFalseStep(kv.Value as string);
						break;
					case "lt":
						yield return CreateLowerThanStep(kv.Value as Dictionary<object, object>);
						break;
					case "gt":
						yield return CreateGreaterThanStep(kv.Value as Dictionary<object, object>);
						break;
				}

			}
		}

		private static LowerThanStep CreateLowerThanStep(Dictionary<object, object> value)
		{
			var kv = value.First();
			return new LowerThanStep { Value = kv.Value is int ? (int) kv.Value : 0, ResponseValue = kv.Key as string};
		}
		private static GreaterThanStep CreateGreaterThanStep(Dictionary<object, object> value)
		{
			var kv = value.First();
			return new GreaterThanStep { Value = kv.Value is int ? (int) kv.Value : 0, ResponseValue = kv.Key as string};
		}
		private static IsTrueStep CreateIsTrueStep(string value)
		{
			value = TrueFalseParam(value);
			return new IsTrueStep {ResponseValue = value};
		}

		private static string TrueFalseParam(string value)
		{
			value = Regex.Replace(value, @"\.(\d+)\.?", "[$1].");
			if (value.Length > 0)
				value = "." + value;
			return value;
		}

		private static IsFalseStep CreateIsFalseStep(string value)
		{
			value = TrueFalseParam(value);
			return new IsFalseStep { ResponseValue = value };
		}

		private static SetStep CreateSetStep(Dictionary<object, object> value)
		{
			var kv = value.First();
			return new SetStep() {VariableName = kv.Value as string, ResponseValue = kv.Key as string};
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
}
