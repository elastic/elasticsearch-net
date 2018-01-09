﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ApiGenerator.Overrides;
using ApiGenerator.Overrides.Descriptors;
using CsQuery.ExtensionMethods.Internal;

namespace ApiGenerator.Domain
{
	public class RawDispatchInfo
	{
		public CsharpMethod CsharpMethod { get; set; }

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
					.Concat(new[] {"p.RequestParameters"});
				return methodArgs;
			}
		}

		public string IfCheck
		{
			get
			{
				var parts = this.CsharpMethod.Parts.Where(p => p.Name != "body").ToList();
				if (!parts.Any()) return string.Empty;

				var allPartsAreRequired = parts.Any() && parts.All(p => p.Required);
				var call = allPartsAreRequired ? "AllSetNoFallback" : "AllSet";
				var assignments = parts
					.Select(p => $"p.RouteValues.{p.Name.ToPascalCase()}")
					.ToList();

				return $"{call}({string.Join(", ", assignments)})";
			}
		}
	}

	public class ApiEndpoint
	{
		private List<CsharpMethod> _csharpMethods;

		public string CsharpMethodName { get; set; }
		public string Documentation { get; set; }
		public IEnumerable<string> Methods { get; set; }
		public IDictionary<string, string> RemovedMethods { get; set; } = new Dictionary<string, string>();
		public ApiUrl Url { get; set; }
		public ApiBody Body { get; set; }

		public string PascalCase(string s)
		{
			return ApiGenerator.PascalCase(s);
		}

		public IEnumerable<CsharpMethod> GetCsharpMethods()
		{
			//we distinct by here to catch aliased endpoints like:
			//  /_cluster/nodes/hotthreads and /_nodes/hotthreads
			return this.CsharpMethods.ToList()
				.DistinctBy(m => m.ReturnType + "--" + m.FullName + "--" + m.Arguments
				);
		}

		public IDictionary<string, IEnumerable<RawDispatchInfo>> RawDispatches
		{
			get
			{
				return this.GetCsharpMethods()
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
			}
		}

		public string OptionallyAppendHttpMethod(IEnumerable<string> availableMethods, string currentHttpMethod)
		{
			if (availableMethods.Count() == 1)
				return string.Empty;
			if (availableMethods.Count() == 2 && availableMethods.Contains("GET"))
			{
				//if on operation has two endpoints and one of them is GET always favor the other as default
				return currentHttpMethod == "GET" ? "Get" : string.Empty;
			}

			return availableMethods.First() == currentHttpMethod ? string.Empty : this.PascalCase(currentHttpMethod);
		}

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

				this.PatchEndpoint();

				var methods = new Dictionary<string, string>(this.RemovedMethods);
				foreach (var method in this.Methods) methods.Add(method, null);
				foreach (var kv in methods)
				{
					var method = kv.Key;
					var obsoleteVersion = kv.Value;
					var methodName = this.CsharpMethodName + this.OptionallyAppendHttpMethod(methods.Keys, method);
					//the distinctby here catches aliases routes i.e
					//  /_cluster/nodes/{node_id}/hotthreads vs  /_cluster/nodes/{node_id}/hot_threads
					foreach (var path in this.Url.Paths.DistinctBy(p => p.Replace("_", "")))
					{
						var parts = (this.Url.Parts ?? new Dictionary<string, ApiUrlPart>())
							.Where(p => path.Contains("{" + p.Key + "}"))
							.OrderBy(p => path.IndexOf("{" + p.Key, System.StringComparison.Ordinal))
							.Select(p =>
							{
								p.Value.Name = p.Key;
								return p.Value;
							})
							.ToList();

						var args = parts.Select(p => p.Argument);

						//.NET does not allow get requests to have a body payload.
						if (method != "GET" && this.Body != null)
						{
							parts.Add(new ApiUrlPart
							{
								Name = "body",
								Type = "PostData",
								Description = this.Body.Description
							});
						}
						if (this.Url.Params == null || !this.Url.Params.Any())
						{
							this.Url.Params = new Dictionary<string, ApiQueryParameters>();
						}
						var queryStringParamName = this.CsharpMethodName + "RequestParameters";
						var apiMethod = new CsharpMethod
						{
							QueryStringParamName = queryStringParamName,
							ReturnType = "TResponse",
							ReturnTypeGeneric = "<TResponse>",
							CallTypeGeneric = "TResponse",
							ReturnDescription = "",
							FullName = methodName,
							HttpMethod = method,
							Documentation = this.Documentation,
							ObsoleteMethodVersion = obsoleteVersion,
							Path = path,
							Parts = parts,
							Url = this.Url
						};
						PatchMethod(apiMethod);

						args = args.Concat(new[]
							{
								apiMethod.QueryStringParamName + " requestParameters = null"
							})
							.ToList();
						apiMethod.Arguments = string.Join(", ", args);
						_csharpMethods.Add(apiMethod);
						yield return apiMethod;

						args = args.Concat(new[]
							{
								"CancellationToken ctx = default(CancellationToken)"
							})
							.ToList();
						apiMethod = new CsharpMethod
						{
							QueryStringParamName = queryStringParamName,
							ReturnType = "Task<TResponse>",
							ReturnTypeGeneric = "<TResponse>",
							CallTypeGeneric = "TResponse",
							ReturnDescription = "",
							FullName = methodName + "Async",
							HttpMethod = method,
							Documentation = this.Documentation,
							ObsoleteMethodVersion = obsoleteVersion,
							Arguments = string.Join(", ", args),
							Path = path,
							Parts = parts,
							Url = this.Url
						};
						PatchMethod(apiMethod);
						_csharpMethods.Add(apiMethod);
						yield return apiMethod;

					}
				}
			}
		}

		private IEndpointOverrides GetOverrides()
		{
			var method = this.CsharpMethodName;
			if (CodeConfiguration.MethodNameOverrides.TryGetValue(method, out var manualOverride))
				method = manualOverride;

			var typeName = "ApiGenerator.Overrides.Endpoints." + method + "Overrides";
			var type = CodeConfiguration.Assembly.GetType(typeName);
			if (type != null && Activator.CreateInstance(type) is IEndpointOverrides overrides)
				return overrides;
			return null;
		}


		private void PatchEndpoint()
		{
			var overrides = this.GetOverrides();
			PatchRequestParameters(overrides);

			//rename the {metric} route param to something more specific on XpackWatcherStats
			// TODO: find a better place to do this
			if (this.CsharpMethodName == "XpackWatcherStats")
			{
				var metric = this.Url.Parts.First(p => p.Key == "metric");

				var apiUrlPart = metric.Value;
				apiUrlPart.Name = "watcher_stats_metric";

				if (this.Url.Parts.Remove("metric"))
				{
					this.Url.Parts.Add("watcher_stats_metric", apiUrlPart);
				}

				this.Url.Path = RenameMetricUrlPathParam(this.Url.Path);
				this.Url.Paths = this.Url.Paths.Select(RenameMetricUrlPathParam);
			}
		}

		private IList<string> DeclaredKeys => this.Url.Params.Select(p => p.Value.OriginalQueryStringParamName ?? p.Key).ToList();

		private IList<string> CreateList(IEndpointOverrides global, IEndpointOverrides local, string type, Func<IEndpointOverrides, IEnumerable<string>> from)
		{
			var list = new List<string>();
			if (global != null) list.AddRange(from(global));
			if (local != null)
			{
				var localList = from(local).ToList();
				list.AddRange(localList);
				var name = local.GetType().Name;
				foreach (var p in localList.Except(DeclaredKeys))
					ApiGenerator.Warnings.Add($"On {name} {type} key '{p}' is not found in spec");
			}
			return list.Distinct().ToList();
		}
		private IList<string> CreateSkipList(IEndpointOverrides global, IEndpointOverrides local) =>
			CreateList(global, local, "skip", e => e.SkipQueryStringParams);

		private IList<string> CreatePartialList(IEndpointOverrides global, IEndpointOverrides local) =>
			CreateList(global, local, "partial", e => e.RenderPartial);

		private IDictionary<string, string> CreateLookup(
			IEndpointOverrides global, IEndpointOverrides local, string type, Func<IEndpointOverrides, IDictionary<string, string>> from)
		{
			var d = new Dictionary<string, string>();
			foreach (var kv in from(global)) d[kv.Key] = kv.Value;

			if (local == null) return d;

			var localDictionary = from(local);
			foreach (var kv in localDictionary) d[kv.Key] = kv.Value;

			var name = local.GetType().Name;
			foreach (var p in localDictionary.Keys.Except(DeclaredKeys))
				ApiGenerator.Warnings.Add($"On {name} {type} key '{p}' is not found in spec");

			return d;
		}
		private IDictionary<string, string> CreateRenameLookup(IEndpointOverrides global, IEndpointOverrides local) =>
			CreateLookup(global, local, "rename", e => e.RenameQueryStringParams);

		private IDictionary<string, string> CreateObsoleteLookup(IEndpointOverrides global, IEndpointOverrides local) =>
			CreateLookup(global, local, "obsolete", e => e.ObsoleteQueryStringParams);

		private void PatchRequestParameters(IEndpointOverrides overrides)
		{
			if (this.Url.Params == null) return; //TODO render common seperately, this causes common to sometimes not be rendered
			foreach (var param in RestApiSpec.CommonApiQueryParameters)
			{
				if (!this.Url.Params.ContainsKey(param.Key))
					this.Url.Params.Add(param.Key, param.Value);
			}

			var globalOverrides = new GlobalOverrides();
			var skipList = CreateSkipList(globalOverrides, overrides);
			var partialList = CreatePartialList(globalOverrides, overrides);
			var renameLookup = CreateRenameLookup(globalOverrides, overrides);
			var obsoleteLookup = CreateObsoleteLookup(globalOverrides, overrides);

			var patchedParams = new Dictionary<string, ApiQueryParameters>();
			foreach (var kv in this.Url.Params)
			{
				kv.Value.OriginalQueryStringParamName = kv.Key;

				if (skipList.Contains(kv.Key)) continue;

				if (!renameLookup.TryGetValue(kv.Key, out var key)) key = kv.Key;

				if (partialList.Contains(key)) kv.Value.RenderPartial = true;

				if (obsoleteLookup.TryGetValue(kv.Key, out var obsolete)) kv.Value.Obsolete = obsolete;

				//make sure source_enabled takes a boolean only
				if (key == "source_enabled") kv.Value.Type = "boolean";

				patchedParams.Add(key, kv.Value);
			}

			this.Url.Params = patchedParams;
		}

		private static string RenameMetricUrlPathParam(string path)
		{
			return path.Replace("{metric}", "{watcher_stats_metric}");
		}

		//Patches a method name for the exceptions (IndicesStats needs better unique names for all the url endpoints)
		//or to get rid of double verbs in an method name i,e ClusterGetSettingsGet > ClusterGetSettings
		public void PatchMethod(CsharpMethod method)
		{
			if (method == null) return;
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

			method = this.GetOverrides()?.PatchMethod(method) ?? method;

			var key = method.QueryStringParamName.Replace("RequestParameters", "");
			if (CodeConfiguration.MethodNameOverrides.TryGetValue(key, out var manualOverride))
				method.QueryStringParamName = manualOverride + "RequestParameters";

			method.DescriptorType = method.QueryStringParamName.Replace("RequestParameters", "Descriptor");
			method.RequestType = method.QueryStringParamName.Replace("RequestParameters", "Request");
			if (CodeConfiguration.KnownRequests.TryGetValue("I" + method.RequestType, out var requestGeneric))
				method.RequestTypeGeneric = requestGeneric;
			else method.RequestTypeUnmapped = true;

			if (CodeConfiguration.KnownDescriptors.TryGetValue(method.DescriptorType, out var generic))
				method.DescriptorTypeGeneric = generic;
			else method.Unmapped = true;

		}
	}
}
