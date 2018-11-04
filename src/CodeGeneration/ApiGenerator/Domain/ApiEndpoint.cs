using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ApiGenerator.Overrides.Allow404;
using ApiGenerator.Overrides.Descriptors;
using CsQuery.ExtensionMethods.Internal;

namespace ApiGenerator.Domain
{
	public class RawDispatchInfo
	{
		public CsharpMethod CsharpMethod { get; set; }

		public string IfCheck
		{
			get
			{
				var parts = CsharpMethod.Parts.Where(p => p.Name != "body").ToList();
				if (!parts.Any()) return string.Empty;

				var allPartsAreRequired = parts.Any() && parts.All(p => p.Required);
				var call = allPartsAreRequired ? "AllSetNoFallback" : "AllSet";
				var assignments = parts
					.Select(p => $"p.RouteValues.{p.Name.ToPascalCase()}")
					.ToList();

				return $"{call}({string.Join(", ", assignments)})";
			}
		}

		public IEnumerable<string> MethodArguments
		{
			get
			{
				var methodArgs = CsharpMethod.Parts
					.Select(p =>
					{
						if (p.Name == "body") return "body";

						switch (p.Type)
						{
							case "enum":
								return $"p.RouteValues.{p.Name.ToPascalCase()}.Value";
							case "long":
								return $"long.Parse(p.RouteValues.{p.Name.ToPascalCase()})";
							default:
								return $"p.RouteValues.{p.Name.ToPascalCase()}";
						}
					})
					.Concat(new[] { "u => p.RequestParameters" });
				return methodArgs;
			}
		}
	}

	public class ApiEndpoint
	{
		private List<CsharpMethod> _csharpMethods;
		public ApiBody Body { get; set; }

		public string CsharpMethodName { get; set; }

		public IEnumerable<CsharpMethod> CsharpMethods
		{
			get
			{
				if (_csharpMethods != null)
				{
					foreach (var csharpMethod in _csharpMethods)
						yield return csharpMethod;

					yield break;
				}

				// enumerate once and cache
				_csharpMethods = new List<CsharpMethod>();

				PatchEndpoint();

				var methods = new Dictionary<string, string>(RemovedMethods);
				foreach (var method in Methods) methods.Add(method, null);
				foreach (var kv in methods)
				{
					var method = kv.Key;
					var obsoleteVersion = kv.Value;
					var methodName = CsharpMethodName + OptionallyAppendHttpMethod(methods.Keys, method);
					//the distinctby here catches aliases routes i.e
					//  /_cluster/nodes/{node_id}/hotthreads vs  /_cluster/nodes/{node_id}/hot_threads
					foreach (var path in Url.Paths.DistinctBy(p => p.Replace("_", "")))
					{
						var parts = (Url.Parts ?? new Dictionary<string, ApiUrlPart>())
							.Where(p => path.Contains("{" + p.Key + "}"))
							.OrderBy(p => path.IndexOf("{" + p.Key, StringComparison.Ordinal))
							.Select(p =>
							{
								p.Value.Name = p.Key;
								return p.Value;
							})
							.ToList();

						var args = parts.Select(p => p.Argument);

						//.NET does not allow get requests to have a body payload.
						if (method != "GET" && Body != null)
							parts.Add(new ApiUrlPart
							{
								Name = "body",
								Type = "PostData<object>",
								Description = Body.Description
							});
						var queryStringParamName = "FluentQueryString";
						if (Url.Params == null || !Url.Params.Any()) Url.Params = new Dictionary<string, ApiQueryParameters>();
						queryStringParamName = CsharpMethodName + "RequestParameters";
						var paraIndent = "\r\n\t\t///";
						var explanationOfT =
								paraIndent + "<para> - T, an object you own that the elasticsearch response will be deserialized to </para>"
								+ paraIndent + "<para> - byte[], no deserialization, but the response stream will be closed </para>"
								+ paraIndent + "<para> - Stream, no deserialization, response stream is your responsibility </para>"
								+ paraIndent + "<para> - VoidResponse, no deserialization, response stream never read and closed </para>"
								+ paraIndent
								+ "<para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth </para>"
							;
						var apiMethod = new CsharpMethod
						{
							QueryStringParamName = queryStringParamName,
							ReturnType = "ElasticsearchResponse<T>",
							ReturnTypeGeneric = "<T>",
							CallTypeGeneric = "T",
							ReturnDescription =
								"ElasticsearchResponse&lt;T&gt; where the behavior depends on the type of T:"
								+ explanationOfT,
							FullName = methodName,
							HttpMethod = method,
							Documentation = Documentation,
							ObsoleteMethodVersion = obsoleteVersion,
							Path = path,
							Parts = parts,
							Url = Url
						};
						PatchMethod(apiMethod);

						args = args.Concat(new[]
							{
								"Func<" + apiMethod.QueryStringParamName + ", " + apiMethod.QueryStringParamName + "> requestParameters = null"
							})
							.ToList();
						apiMethod.Arguments = string.Join(", ", args);
						_csharpMethods.Add(apiMethod);
						yield return apiMethod;

						args = args.Concat(new[]
							{
								"CancellationToken cancellationToken = default(CancellationToken)"
							})
							.ToList();
						apiMethod = new CsharpMethod
						{
							QueryStringParamName = queryStringParamName,
							ReturnType = "Task<ElasticsearchResponse<T>>",
							ReturnTypeGeneric = "<T>",
							CallTypeGeneric = "T",
							ReturnDescription =
								"A task of ElasticsearchResponse&lt;T&gt; where the behaviour depends on the type of T:"
								+ explanationOfT,
							FullName = methodName + "Async",
							HttpMethod = method,
							Documentation = Documentation,
							ObsoleteMethodVersion = obsoleteVersion,
							Arguments = string.Join(", ", args),
							Path = path,
							Parts = parts,
							Url = Url
						};
						PatchMethod(apiMethod);
						_csharpMethods.Add(apiMethod);
						yield return apiMethod;

						//No need for deserialization state when returning dynamicdictionary

						var explanationOfDynamic =
							paraIndent +
							"<para> - Dynamic dictionary is a special dynamic type that allows json to be traversed safely </para>"
							+ paraIndent +
							"<para> - i.e result.Response.hits.hits[0].property.nested[\"nested_deeper\"] </para>"
							+ paraIndent +
							"<para> - can be safely dispatched to a nullable type even if intermediate properties do not exist </para>";

						var defaultBoundGeneric = Url.Path.Contains("_cat") ? "string" : "DynamicDictionary";

						apiMethod = new CsharpMethod
						{
							QueryStringParamName = queryStringParamName,
							ReturnType = $"ElasticsearchResponse<{defaultBoundGeneric}>",
							ReturnTypeGeneric = null,
							//CallTypeGeneric = defaultBoundGeneric == "DynamicDictionary" ? "Dictionary<string, object>" : defaultBoundGeneric,
							CallTypeGeneric = defaultBoundGeneric,
							ReturnDescription =
								"ElasticsearchResponse&lt;DynamicDictionary&gt;"
								+ explanationOfDynamic,
							FullName = methodName,
							HttpMethod = method,
							Documentation = Documentation,
							ObsoleteMethodVersion = obsoleteVersion,
							Arguments = string.Join(", ", args),
							Path = path,
							Parts = parts,
							Url = Url
						};
						PatchMethod(apiMethod);
						_csharpMethods.Add(apiMethod);
						yield return apiMethod;

						args = args.Concat(new[]
							{
								"CancellationToken cancellationToken = default(CancellationToken)"
							})
							.ToList();
						apiMethod = new CsharpMethod
						{
							QueryStringParamName = queryStringParamName,
							ReturnType = $"Task<ElasticsearchResponse<{defaultBoundGeneric}>>",
							ReturnTypeGeneric = null,
							//CallTypeGeneric = defaultBoundGeneric == "DynamicDictionary" ? "Dictionary<string, object>" : defaultBoundGeneric,
							CallTypeGeneric = defaultBoundGeneric,
							ReturnDescription =
								"A task of ElasticsearchResponse&lt;DynamicDictionary$gt;"
								+ explanationOfDynamic,
							FullName = methodName + "Async",
							HttpMethod = method,
							Documentation = Documentation,
							ObsoleteMethodVersion = obsoleteVersion,
							Arguments = string.Join(", ", args),
							Path = path,
							Parts = parts,
							Url = Url
						};
						PatchMethod(apiMethod);
						_csharpMethods.Add(apiMethod);
						yield return apiMethod;
					}
				}
			}
		}

		public string Documentation { get; set; }
		public IEnumerable<string> Methods { get; set; }
		public IDictionary<string, ApiQueryParameters> ObsoleteQueryParameters { get; set; }

		public IDictionary<string, IEnumerable<RawDispatchInfo>> RawDispatches => GetCsharpMethods()
			.GroupBy(p => p.HttpMethod)
			.ToDictionary(kv => kv.Key, kv => kv
				.DistinctBy(m => m.Path)
				.OrderByDescending(m => m.Parts.Count())
				.Select(m =>
					new RawDispatchInfo
					{
						CsharpMethod = m,
					}
				)
			);

		public IDictionary<string, string> RemovedMethods { get; set; } = new Dictionary<string, string>();
		public ApiUrl Url { get; set; }

		public string PascalCase(string s) => ApiGenerator.PascalCase(s);

		public IEnumerable<CsharpMethod> GetCsharpMethods() => CsharpMethods.ToList()
			.Where(m => m.CallTypeGeneric != "DynamicDictionary" && m.CallTypeGeneric != "string")
			.DistinctBy(m => m.ReturnType + "--" + m.FullName + "--" + m.Arguments
			);

		public string OptionallyAppendHttpMethod(IEnumerable<string> availableMethods, string currentHttpMethod)
		{
			if (availableMethods.Count() == 1)
				return string.Empty;
			if (availableMethods.Count() == 2 && availableMethods.Contains("GET")) return currentHttpMethod == "GET" ? "Get" : string.Empty;

			return availableMethods.First() == currentHttpMethod ? string.Empty : PascalCase(currentHttpMethod);
		}

		private void PatchEndpoint()
		{
			//rename the {metric} route param to something more specific on XpackWatcherStats
			// TODO: find a better place to do this
			if (CsharpMethodName == "XpackWatcherStats")
			{
				var metric = Url.Parts.First(p => p.Key == "metric");

				var apiUrlPart = metric.Value;
				apiUrlPart.Name = "watcher_stats_metric";

				if (Url.Parts.Remove("metric")) Url.Parts.Add("watcher_stats_metric", apiUrlPart);

				Url.Path = RenameMetricUrlPathParam(Url.Path);
				Url.Paths = Url.Paths.Select(RenameMetricUrlPathParam);
			}
		}

		private static string RenameMetricUrlPathParam(string path) => path.Replace("{metric}", "{watcher_stats_metric}");

		//Patches a method name for the exceptions (IndicesStats needs better unique names for all the url endpoints)
		//or to get rid of double verbs in an method name i,e ClusterGetSettingsGet > ClusterGetSettings
		public void PatchMethod(CsharpMethod method)
		{
			Func<string, bool> ms = s => method.FullName.StartsWith(s);
			Func<string, bool> pc = s => method.Path.Contains(s);

			if (ms("Indices") && !pc("{index}"))
				method.FullName = (method.FullName + "ForAll").Replace("AsyncForAll", "ForAllAsync");

			if (ms("Nodes") && !pc("{node_id}"))
				method.FullName = (method.FullName + "ForAll").Replace("AsyncForAll", "ForAllAsync");

			//remove duplicate occurance of the HTTP method name
			var m = method.HttpMethod.ToPascalCase();
			method.FullName =
				Regex.Replace(method.FullName, m, a => a.Index != method.FullName.IndexOf(m, StringComparison.Ordinal) ? "" : m);

			foreach (var param in RestApiSpec.CommonApiQueryParameters)
			{
				if (!method.Url.Params.ContainsKey(param.Key))
					method.Url.Params.Add(param.Key, param.Value);
			}
			if (ObsoleteQueryParameters != null)
			{
				foreach (var param in ObsoleteQueryParameters)
				{
					if (!method.Url.Params.ContainsKey(param.Key))
						method.Url.Params.Add(param.Key, param.Value);
				}
			}

			string manualOverride;
			var key = method.QueryStringParamName.Replace("RequestParameters", "");
			if (CodeConfiguration.MethodNameOverrides.TryGetValue(key, out manualOverride))
				method.QueryStringParamName = manualOverride + "RequestParameters";

			method.DescriptorType = method.QueryStringParamName.Replace("RequestParameters", "Descriptor");
			method.RequestType = method.QueryStringParamName.Replace("RequestParameters", "Request");
			string requestGeneric;
			if (CodeConfiguration.KnownRequests.TryGetValue("I" + method.RequestType, out requestGeneric))
				method.RequestTypeGeneric = requestGeneric;
			else method.RequestTypeUnmapped = true;

			method.Allow404 = ApiEndpointsThatAllow404.Endpoints.Contains(method.DescriptorType.Replace("Descriptor", ""));

			string generic;
			if (CodeConfiguration.KnownDescriptors.TryGetValue(method.DescriptorType, out generic))
				method.DescriptorTypeGeneric = generic;
			else method.Unmapped = true;

			try
			{
				IEnumerable<string> skipList = new List<string>();
				IDictionary<string, string> queryStringParamsRenameList = new Dictionary<string, string>();

				var typeName = "ApiGenerator.Overrides.Descriptors." + method.DescriptorType + "Overrides";
				var type = CodeConfiguration.Assembly.GetType(typeName);
				if (type != null)
				{
					var overrides = Activator.CreateInstance(type) as IDescriptorOverrides;
					if (overrides != null)
					{
						skipList = overrides.SkipQueryStringParams ?? skipList;
						queryStringParamsRenameList = overrides.RenameQueryStringParams ?? queryStringParamsRenameList;
						method = overrides.PatchMethod(method);
					}
				}

				var globalQueryStringRenames = new Dictionary<string, string>
				{
					{ "_source", "source_enabled" },
					{ "_source_include", "source_include" },
					{ "_source_exclude", "source_exclude" },
					{ "q", "query_on_query_string" },
				};

				foreach (var kv in globalQueryStringRenames)
				{
					if (!queryStringParamsRenameList.ContainsKey(kv.Key))
						queryStringParamsRenameList[kv.Key] = kv.Value;
				}

				var patchedParams = new Dictionary<string, ApiQueryParameters>();
				foreach (var kv in method.Url.Params)
				{
					if (kv.Value.OriginalQueryStringParamName.IsNullOrEmpty())
						kv.Value.OriginalQueryStringParamName = kv.Key;
					if (skipList.Contains(kv.Key))
						continue;

					string newName;
					if (!queryStringParamsRenameList.TryGetValue(kv.Key, out newName))
					{
						patchedParams.Add(kv.Key, kv.Value);
						continue;
					}

					patchedParams.Add(newName, kv.Value);
				}

				method.Url.Params = patchedParams;
			}
			// ReSharper disable once EmptyGeneralCatchClause
			catch { }
		}
	}
}
