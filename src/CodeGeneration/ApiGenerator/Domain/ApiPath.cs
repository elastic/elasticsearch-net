using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiGenerator.Domain {
	public class ApiPath
	{
		public string Path { get; }

		public List<ApiUrlPart> Parts { get; }

		public ApiPath(string path, IDictionary<string, ApiUrlPart> allParts)
		{
			Path = LeadingBackslash(path);
			if (allParts == null)
			{
				Parts = new List<ApiUrlPart>();
				return;
			}
			var parts =
				from p in allParts
				//so deliciously side effect-y but at least its more isolated then in ApiEndpoint.CsharpMethods
				let name = p.Value.Name = p.Key
				where path.Contains($"{{{name}}}")
				orderby path.IndexOf($"{{{name}}}", StringComparison.Ordinal)
				select p.Value;
			Parts = parts.ToList();
		}

		public string ConstructorArguments => string.Join(", ", Parts.Select(p => $"{p.ClrTypeName} {p.Name}"));
		public string RequestBaseArguments =>
			!Parts.Any() ? string.Empty
				: "r => r." + string.Join(".", Parts.Select(p => $"{(p.Required ? "Required" : "Optional")}(\"{p.Name}\", {p.Name})"));

		public string TypedSubClassBaseArguments => string.Join(", ", Parts.Select(p => p.Name));

		private static string[] ResolvabeFromT = { "index"};
		public bool HasResolvableArguments => Parts.Any(p => ResolvabeFromT.Contains(p.Name));
		public string AutoResolveConstructorArguments => string.Join(", ", Parts.Where(p  => !ResolvabeFromT.Contains(p.Name)).Select(p => $"{p.ClrTypeName} {p.Name}"));

		public string AutoResolveBaseArguments(string generic) => string.Join(", ", Parts.Select(p => !ResolvabeFromT.Contains(p.Name) ? p.Name : $"typeof({generic})"));

		public string GetXmlDocs(string indent, bool skipResolvable = false, bool documentConstructor = false)
		{
			var doc = $@"///<summary>{Path}</summary>";
			var parts = Parts.Where(p => !skipResolvable || !ResolvabeFromT.Contains(p.Name)).ToList();
			if (!parts.Any()) return doc;

			doc += indent;
			doc += string.Join(indent, parts.Select(ParamDoc));
			return doc;

			string ParamDoc(ApiUrlPart p) => P(p.Name, GetDescription(p));

			string GetDescription(ApiUrlPart p)
			{
				if (documentConstructor) return "The document used to resolve the path from";
				return p.Required ? "this parameter is required" : "Optional, accepts null";
			}
		}
		public string GetDocumentPathXmlDocs(string indent)
		{
			var doc = $@"///<summary>{Path}</summary>";
			doc += indent;
			doc += P("path", "Describe the id and index of the document through <see cref=\"DocumentPath<>\" />");
			return doc;

		}


		private string P(string name, string description) => $"///<param name=\"{name}\">{description}</param>";


		private string LeadingBackslash(string p) => p.StartsWith("/") ? p : $"/{p}";
	}
}
