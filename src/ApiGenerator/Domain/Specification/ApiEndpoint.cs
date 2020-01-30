using System;
using System.Collections.Generic;
using System.Linq;
using ApiGenerator.Configuration.Overrides;
using ApiGenerator.Domain.Code;
using ApiGenerator.Domain.Code.HighLevel.Methods;
using ApiGenerator.Domain.Code.HighLevel.Requests;
using ApiGenerator.Domain.Code.LowLevel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace ApiGenerator.Domain.Specification
{
	public class ApiEndpoint
	{
		/// <summary> The filename of the spec describing the api endpoint </summary>
		public string FileName { get; set; }

		/// <summary> The original name as declared in the spec </summary>
		public string Name { get; set; }

		/// <summary> The original namespace as declared in the spec </summary>
		public string Namespace { get; set; }

		/// <summary> The original method name as declared in the spec </summary>
		public string MethodName { get; set; }

		/// <summary> Computed Csharp identifier names </summary>
		public CsharpNames CsharpNames { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		[JsonProperty("stability")]
		public Stability Stability { get; set; }

		[JsonProperty("documentation")]
		public Documentation OfficialDocumentationLink { get; set; }

		public UrlInformation Url { get; set; }

		public Body Body { get; set; }

		[JsonProperty("methods")]
		public IReadOnlyCollection<string> HttpMethods { get; set; }

		public IEndpointOverrides Overrides { get; internal set; }

		public RequestInterface RequestInterface => new RequestInterface
		{
			CsharpNames = CsharpNames,
			UrlParts = Url.Parts,
			PartialParameters =
				Body == null ? Enumerable.Empty<QueryParameters>().ToList() : Url.Params.Values.Where(p => p.RenderPartial && !p.Skip).ToList(),
			OfficialDocumentationLink = OfficialDocumentationLink?.Url
		};

		public RequestPartialImplementation RequestPartialImplementation => new RequestPartialImplementation
		{
			CsharpNames = CsharpNames,
			OfficialDocumentationLink = OfficialDocumentationLink?.Url,
			Stability = Stability,
			Paths = Url.Paths,
			Parts = Url.Parts,
			Params = Url.Params.Values.Where(p => !p.Skip).ToList(),
			Constructors = Constructor.RequestConstructors(CsharpNames, Url, inheritsFromPlainRequestBase: true).ToList(),
			GenericConstructors = Constructor.RequestConstructors(CsharpNames, Url, inheritsFromPlainRequestBase: false).ToList(),
			HasBody = Body != null,
		};

		public DescriptorPartialImplementation DescriptorPartialImplementation => new DescriptorPartialImplementation
		{
			CsharpNames = CsharpNames,
			OfficialDocumentationLink = OfficialDocumentationLink?.Url,
			Constructors = Constructor.DescriptorConstructors(CsharpNames, Url).ToList(),
			Paths = Url.Paths,
			Parts = Url.Parts,
			Params = Url.Params.Values.Where(p => !p.Skip).ToList(),
			HasBody = Body != null,
		};

		public RequestParameterImplementation RequestParameterImplementation => new RequestParameterImplementation
		{
			CsharpNames = CsharpNames,
			OfficialDocumentationLink = OfficialDocumentationLink?.Url,
			Params = Url.Params.Values.Where(p => !p.Skip).ToList(),
			HttpMethod = PreferredHttpMethod
		};

		public string PreferredHttpMethod
		{
			get
			{
				var first = HttpMethods.First();
				if (HttpMethods.Count > 1 && first.ToUpperInvariant() == "GET")
					return HttpMethods.Last();

				return first;
			}
		}

		public string HighLevelMethodXmlDocDescription =>
			$"<c>{PreferredHttpMethod}</c> request to the <c>{Name}</c> API, read more about this API online:";

		public HighLevelModel HighLevelModel => new HighLevelModel
		{
			CsharpNames = CsharpNames,
			Fluent = new FluentMethod(CsharpNames, Url.Parts,
				selectorIsOptional: Body == null || !Body.Required || HttpMethods.Contains("GET"),
				link: OfficialDocumentationLink?.Url,
				summary: HighLevelMethodXmlDocDescription
			),
			FluentBound = !CsharpNames.DescriptorBindsOverMultipleDocuments
				? null
				: new BoundFluentMethod(CsharpNames, Url.Parts,
					selectorIsOptional: Body == null || !Body.Required || HttpMethods.Contains("GET"),
					link: OfficialDocumentationLink?.Url,
					summary: HighLevelMethodXmlDocDescription
				),
			Initializer = new InitializerMethod(CsharpNames,
				link: OfficialDocumentationLink?.Url,
				summary: HighLevelMethodXmlDocDescription
			)
		};

		private List<LowLevelClientMethod> _lowLevelClientMethods;

		public IReadOnlyCollection<LowLevelClientMethod> LowLevelClientMethods
		{
			get
			{
				if (_lowLevelClientMethods != null && _lowLevelClientMethods.Count > 0) return _lowLevelClientMethods;

				// enumerate once and cache
				_lowLevelClientMethods = new List<LowLevelClientMethod>();

				if (OfficialDocumentationLink == null)
					Generator.ApiGenerator.Warnings.Add($"API '{Name}' has no documentation");

				var httpMethod = PreferredHttpMethod;
				foreach (var path in Url.PathsWithDeprecations)
				{
					var methodName = CsharpNames.PerPathMethodName(path.Path);
					var parts = new List<UrlPart>(path.Parts);
					var mapsApiArgumentHints = parts.Select(p => p.Name).ToList();
					// TODO This is hack until we stop transforming the new spec format into the old
					if (Name == "index" && !mapsApiArgumentHints.Contains("id"))
						httpMethod = "POST";
					else if (Name == "index") httpMethod = PreferredHttpMethod;

					if (Body != null)
					{
						parts.Add(new UrlPart { Name = "body", Type = "PostData", Description = Body.Description });
						mapsApiArgumentHints.Add("body");
					}

					var args = parts
						.Select(p => p.Argument)
						.Concat(new[] { CsharpNames.ParametersName + " requestParameters = null" })
						.ToList();

					var apiMethod = new LowLevelClientMethod
					{
						Arguments = string.Join(", ", args),
						MapsApiArguments = string.Join(", ", mapsApiArgumentHints),
						CsharpNames = CsharpNames,
						PerPathMethodName = methodName,
						HttpMethod = httpMethod,
						OfficialDocumentationLink = OfficialDocumentationLink?.Url,
						Stability = Stability,
						DeprecatedPath = path.Deprecation,
						Path = path.Path,
						Parts = parts,
						Url = Url,
						HasBody = Body != null
					};
					_lowLevelClientMethods.Add(apiMethod);
				}
				return _lowLevelClientMethods;
			}
		}
	}
}
