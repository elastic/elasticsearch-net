using System;
using System.Collections.Generic;
using System.Linq;
using ApiGenerator.Overrides.Descriptors;
using Newtonsoft.Json;

namespace ApiGenerator.Domain
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
		
		[JsonProperty("documentation")]
		public string OfficialDocumentationLink { get; set; }
		
		public UrlInformation Url { get; set; }
		
		public Body Body { get; set; }

		[JsonProperty("methods")]
		public IReadOnlyCollection<string> HttpMethods { get; set; }
		
		public IEndpointOverrides Overrides { get; internal set; }

		public RequestInterface RequestInterface => new RequestInterface
		{
			CsharpNames = CsharpNames,
			UrlParts = Url.Parts,
			PartialParameters = Body == null ? Enumerable.Empty<QueryParameters>().ToList() : Url.Params.Values.Where(p=>p.RenderPartial && !p.Skip).ToList(),
			OfficialDocumentationLink = OfficialDocumentationLink
		};
		
		public RequestPartialImplementation RequestPartialImplementation => new RequestPartialImplementation
		{
			CsharpNames = CsharpNames,
			OfficialDocumentationLink = OfficialDocumentationLink,
			Paths = Url.Paths,
			Parts = Url.Parts,
			Params = Url.Params.Values.Where(p=>!p.Skip).ToList(),
			Constructors = Constructor.RequestConstructors(CsharpNames, Url, inheritsFromPlainRequestBase: true).ToList(),
			GenericConstructors = Constructor.RequestConstructors(CsharpNames, Url, inheritsFromPlainRequestBase: false).ToList(),
			HasBody = Body != null,
		};
		
		public DescriptorPartialImplementation DescriptorPartialImplementation => new DescriptorPartialImplementation
		{
			CsharpNames = CsharpNames, 
			OfficialDocumentationLink = OfficialDocumentationLink,
			Constructors = Constructor.DescriptorConstructors(CsharpNames, Url).ToList(),
			Paths = Url.Paths,
			Parts = Url.Parts,
			Params = Url.Params.Values.Where(p=>!p.Skip).ToList(),
			HasBody = Body != null,
		};
		
		public RequestParameterImplementation RequestParameterImplementation => new RequestParameterImplementation
		{
			CsharpNames = CsharpNames,
			OfficialDocumentationLink = OfficialDocumentationLink,
			Params = Url.Params.Values.Where(p=>!p.Skip).ToList(),
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
		
		private List<LowLevelClientMethod> _lowLevelClientMethods;
		public IReadOnlyCollection<LowLevelClientMethod> LowLevelClientMethods
		{
			get
			{
				if (_lowLevelClientMethods != null && _lowLevelClientMethods.Count > 0) return _lowLevelClientMethods;

				// enumerate once and cache
				_lowLevelClientMethods = new List<LowLevelClientMethod>();

				var httpMethod = PreferredHttpMethod;
				foreach (var path in Url.Paths)
				{
					var methodName = CsharpNames.PerPathMethodName(path.Path);
					var parts = new List<UrlPart>(path.Parts);

					if (Body != null)
						parts.Add(new UrlPart { Name = "body", Type = "PostData", Description = Body.Description });

					var args = parts
						.Select(p => p.Argument)
						.Concat(new[] { CsharpNames.ParametersName + " requestParameters = null" })
						.ToList();
					
					var apiMethod = new LowLevelClientMethod
					{
						Arguments = string.Join(", ", args),
						CsharpNames = CsharpNames,
						PerPathMethodName = methodName,
						HttpMethod = httpMethod,
						OfficialDocumentationLink = OfficialDocumentationLink,
						DeprecatedPath = null, //TODO
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
