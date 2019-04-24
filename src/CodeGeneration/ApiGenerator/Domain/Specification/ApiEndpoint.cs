using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ApiGenerator.Overrides.Descriptors;

namespace ApiGenerator.Domain
{
	public class ApiEndpoint
	{
		public Body Body { get; set; }
		public string CsharpMethodName { get; set; }
		public string FileName { get; set; }

		private List<CsharpMethod> _csharpMethods;
		public IReadOnlyCollection<CsharpMethod> CsharpMethods
		{
			get
			{
				if (_csharpMethods != null) return _csharpMethods;

				// enumerate once and cache
				_csharpMethods = new List<CsharpMethod>();

				var httpMethod = Methods.First();
				var methodName = CsharpMethodName;
				foreach (var path in Url.ExposedApiPaths)
				{
					var parts = new List<UrlPart>(path.Parts);

					if (Body != null)
						parts.Add(new UrlPart { Name = "body", Type = "PostData", Description = Body.Description });

					if (Url.Params == null || !Url.Params.Any()) Url.Params = new SortedDictionary<string, QueryParameters>();
					var queryStringParamName = CsharpMethodName + "RequestParameters";
					var apiMethod = new CsharpMethod
					{
						QueryStringParamName = queryStringParamName,
						ReturnType = "TResponse",
						ReturnTypeGeneric = "<TResponse>",
						CallTypeGeneric = "TResponse",
						ReturnDescription = "",
						FullName = methodName,
						HttpMethod = httpMethod,
						Documentation = Documentation,
						ObsoleteMethodVersion = null, //TODO
						Path = path.Path,
						Parts = parts,
						Url = Url,
						HasBody = Body != null
					};
					PatchMethod(apiMethod);

					var args = parts
						.Select(p => p.Argument)
						.Concat(new[] { apiMethod.QueryStringParamName + " requestParameters = null" })
						.ToList();
					apiMethod.Arguments = string.Join(", ", args);
					_csharpMethods.Add(apiMethod);

					args = args.Concat(new[] { "CancellationToken ctx = default" }).ToList();
					apiMethod = new CsharpMethod
					{
						QueryStringParamName = queryStringParamName,
						ReturnType = "Task<TResponse>",
						ReturnTypeGeneric = "<TResponse>",
						CallTypeGeneric = "TResponse",
						ReturnDescription = "",
						FullName = methodName + "Async",
						HttpMethod = httpMethod,
						Documentation = Documentation,
						ObsoleteMethodVersion = null, //TODO
						Arguments = string.Join(", ", args),
						Path = path.Path,
						Parts = parts,
						Url = Url,
						HasBody = Body != null
					};
					PatchMethod(apiMethod);
					_csharpMethods.Add(apiMethod);
				}
				return _csharpMethods;
			}
		}

		private string _documentation;

		public string Documentation
		{
			get => string.IsNullOrWhiteSpace(_documentation) ? "TODO" : _documentation;
			set => _documentation = value;
		}

		public IEnumerable<string> Methods { get; set; }

		public string RestSpecName { get; set; }
		public UrlInformation Url { get; set; }
		public IEndpointOverrides Overrides { get; internal set; }

		public IEnumerable<CsharpMethod> GetCsharpMethods() => CsharpMethods.ToList()
			.DistinctBy(m => m.ReturnType + "--" + m.FullName + "--" + m.Arguments
		);


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

			method = Overrides?.PatchMethod(method) ?? method;

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
