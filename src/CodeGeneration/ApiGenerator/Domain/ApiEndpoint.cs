using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ApiGenerator.Overrides.Descriptors;

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
					.Concat(new[] { "p.RequestParameters" });
				return methodArgs;
			}
		}
	}

	public class ApiEndpoint
	{
		private List<CsharpMethod> _csharpMethods;
		public ApiBody Body { get; set; }
		public string CsharpMethodName { get; set; }
		public string FileName { get; set; }

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
					foreach (var path in Url.ExposedApiPaths)
					{
						var parts = new List<ApiUrlPart>(path.Parts);
						var args = parts.Select(p => p.Argument);

						//.NET does not allow get requests to have a body payload.
						if (method != "GET" && Body != null)
							parts.Add(new ApiUrlPart
							{
								Name = "body",
								Type = "PostData",
								Description = Body.Description
							});

						if (Url.Params == null || !Url.Params.Any()) Url.Params = new Dictionary<string, ApiQueryParameters>();
						var queryStringParamName = CsharpMethodName + "RequestParameters";
						var apiMethod = new CsharpMethod
						{
							QueryStringParamName = queryStringParamName,
							ReturnType = "TResponse",
							ReturnTypeGeneric = "<TResponse>",
							CallTypeGeneric = "TResponse",
							ReturnDescription = "",
							FullName = methodName,
							HttpMethod = method,
							Documentation = Documentation,
							ObsoleteMethodVersion = obsoleteVersion,
							Path = path.Path,
							Parts = parts,
							Url = Url
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
							Documentation = Documentation,
							ObsoleteMethodVersion = obsoleteVersion,
							Arguments = string.Join(", ", args),
							Path = path.Path,
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

		private string _documentation;
		public string Documentation
		{
			get => string.IsNullOrWhiteSpace(_documentation) ? "TODO" : _documentation;
			set => _documentation = value;
		}

		public IEnumerable<string> Methods { get; set; }

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

		public string RestSpecName { get; set; }
		public ApiUrl Url { get; set; }

		public string PascalCase(string s) => ApiGenerator.PascalCase(s);

		public IEnumerable<CsharpMethod> GetCsharpMethods() => CsharpMethods.ToList()
			.DistinctBy(m => m.ReturnType + "--" + m.FullName + "--" + m.Arguments
			);

		public string OptionallyAppendHttpMethod(IEnumerable<string> availableMethods, string currentHttpMethod)
		{
			if (availableMethods.Count() == 1)
				return string.Empty;
			if (availableMethods.Count() == 2 && availableMethods.Contains("GET")) return currentHttpMethod == "GET" ? "Get" : string.Empty;

			return availableMethods.First() == currentHttpMethod ? string.Empty : PascalCase(currentHttpMethod);
		}


		private IEndpointOverrides GetOverrides()
		{
			var method = CsharpMethodName;
			if (CodeConfiguration.ApiNameMapping.TryGetValue(RestSpecName, out var mapsApiMethodName))
				method = mapsApiMethodName;
			else if (CodeConfiguration.MethodNameOverrides.TryGetValue(method, out var manualOverride))
				method = manualOverride;

			var typeName = "ApiGenerator.Overrides.Endpoints." + method + "Overrides";
			var type = CodeConfiguration.Assembly.GetType(typeName);
			if (type != null && Activator.CreateInstance(type) is IEndpointOverrides overrides)
				return overrides;

			return null;
		}


		private void PatchEndpoint()
		{
			var overrides = GetOverrides();
			PatchRequestParameters(overrides);
		}


		private void PatchRequestParameters(IEndpointOverrides overrides)
		{
			var newParams = ApiQueryParametersPatcher.Patch(Url.Path, Url.Params, overrides);
			Url.Params = newParams;
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

			method = GetOverrides()?.PatchMethod(method) ?? method;

			var key = method.QueryStringParamName.Replace("RequestParameters", "");
			if (CodeConfiguration.ApiNameMapping.TryGetValue(RestSpecName, out var mapsApiMethodName))
				method.QueryStringParamName = mapsApiMethodName + "RequestParameters";
			else if (CodeConfiguration.MethodNameOverrides.TryGetValue(key, out var manualOverride))
				method.QueryStringParamName = manualOverride + "RequestParameters";

			method.DescriptorType = method.QueryStringParamName.Replace("RequestParameters", "Descriptor");
			method.RequestType = method.QueryStringParamName.Replace("RequestParameters", "Request");
			if (CodeConfiguration.KnownRequests.TryGetValue("I" + method.RequestType, out var requestGeneric))
			{
				method.RequestTypeGeneric = requestGeneric;
				if (CodeConfiguration.NumberOfDeclaredRequests.TryGetValue("I" + method.RequestType, out var number))
					method.GenericAndNonGeneric = number > 1;

			}
			else method.RequestTypeUnmapped = true;

			if (CodeConfiguration.KnownDescriptors.TryGetValue(method.DescriptorType, out var generic))
				method.DescriptorTypeGeneric = generic;
			else method.Unmapped = true;
		}
	}
}
