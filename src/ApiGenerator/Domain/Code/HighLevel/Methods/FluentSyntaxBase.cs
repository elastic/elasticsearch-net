// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Linq;
using ApiGenerator.Configuration;
using ApiGenerator.Domain.Specification;

namespace ApiGenerator.Domain.Code.HighLevel.Methods 
{
	public abstract class FluentSyntaxBase : MethodSyntaxBase
	{
		private readonly bool _selectorIsOptional;

		protected FluentSyntaxBase(CsharpNames names, IReadOnlyCollection<UrlPart> parts, bool selectorIsOptional, string link, string summary) 
			: base(names, link, summary) =>
			(UrlParts, _selectorIsOptional) = (CreateDescriptorArgs(parts), selectorIsOptional);

		private IReadOnlyCollection<UrlPart> UrlParts { get; }

		/// <summary>
		/// The selector is optional if so set by <see cref="ApiEndpoint.HighLevelModel"/> (has no or optional body)
		/// Or if there is a custom constructor on the descriptor in which case we assume that constructor holds all the required
		/// values
		/// </summary>
		private bool SelectorIsOptional => _selectorIsOptional || CodeConfiguration.DescriptorConstructors.ContainsKey(CsharpNames.DescriptorName);

		public string MethodName => CsharpNames.MethodName;

		public string OptionalSelectorSuffix => SelectorIsOptional ? " = null" : string.Empty;

		public virtual string DescriptorName => CsharpNames.GenericOrNonGenericDescriptorName;
		public virtual string Selector => $"Func<{DescriptorName}, {CsharpNames.GenericOrNonGenericInterfacePreference}>";

		public override string MethodGenerics =>
			CodeConfiguration.GenericOnlyInterfaces.Contains(CsharpNames.RequestInterfaceName)
				? CsharpNames.GenericsDeclaredOnRequest
				: DescriptorGenerics;
		
		public virtual string RequestMethodGenerics =>
			CodeConfiguration.GenericOnlyInterfaces.Contains(CsharpNames.RequestInterfaceName)
				? CsharpNames.GenericsDeclaredOnRequest
				: CsharpNames.GenericsDeclaredOnResponse;

		private string DescriptorGenerics => CsharpNames.HighLevelDescriptorMethodGenerics.Any()
			? $"<{string.Join(", ", CsharpNames.HighLevelDescriptorMethodGenerics)}>"
			: null;

		private List<UrlPart> CreateDescriptorArgs(IReadOnlyCollection<UrlPart> parts)
		{
			var requiredParts = parts.Where(p => p.Required).ToList();
			
			//Many api's return ALOT of information by default e.g get_alias or get_mapping
			//the client methods that take a descriptor default to forcing a choice on the user.
			//except for cat api's where the amount of information returned is manageable
			
			var willInferFromDocument = CsharpNames.GenericsDeclaredOnDescriptor?.Contains("Document") ?? false;
			if (!requiredParts.Any() && CsharpNames.Namespace != "Cat")
			{
				var candidates = new[]
				{
					//only make index part the first argument if the descriptor is not generic on T.*?Document
					parts.FirstOrDefault(p => p.Type == "list" && (p.Name == "index" || p.Name == "indices") && !willInferFromDocument),
					parts.FirstOrDefault(p => p.Name == "name"),
				};
				requiredParts = candidates.Where(p=>p!= null).Take(1).ToList();
			}
			if (!willInferFromDocument) return requiredParts;

			//if index, indices is required but the descriptor is generic these will be inferred so no need to pass explicitly
			requiredParts = requiredParts.Where(p => p.Name != "index" && p.Name != "indices").ToList();
			var idPart = requiredParts.FirstOrDefault(i => i.Name == "id");
			if ((idPart != null && UrlInformation.IsADocumentRoute(parts)) || IsDocumentRequest)
			{
				if (requiredParts.Contains(idPart)) requiredParts.Remove(idPart);
				var generic = GenericFirstArgument;
				var typeName = IsDocumentRequest ? generic : $"DocumentPath<{generic}>";
				var arg = IsDocumentRequest ? "document" : idPart.Name;
				requiredParts.Add(new UrlPart
				{
					Name = arg,
					Required = true,
					ClrTypeNameOverride = typeName
				});
			}

			return requiredParts;
		}

		private bool IsDocumentRequest => CodeConfiguration.DocumentRequests.Contains(CsharpNames.RequestInterfaceName);
		private string GenericFirstArgument => 
			CsharpNames.GenericsDeclaredOnDescriptor.Replace("<", "").Replace(">", "").Split(",").First().Trim();
		
		public string DescriptorArguments()
		{
			string codeArgs;
			if (CodeConfiguration.DescriptorConstructors.TryGetValue(CsharpNames.DescriptorName, out codeArgs))
				codeArgs += ",";
			
			if (!UrlParts.Any()) return codeArgs;

			string Optional(UrlPart p) => !p.Required && SelectorIsOptional ? " = null" : string.Empty;
			return codeArgs + string.Join(", ", UrlParts.Select(p => $"{p.HighLevelTypeName} {p.Name.ToCamelCase()}{Optional(p)}")) + ", ";
		}

		public string SelectorArguments()
		{
			string codeArgs = null;
			if (CodeConfiguration.DescriptorConstructors.TryGetValue(CsharpNames.DescriptorName, out codeArgs))
			{
				codeArgs = string.Join(", ", codeArgs.Split(',').Select(a=>a.Split(' ').Last()));
				return codeArgs;
			}
			
			var parts = UrlParts.Where(p => p.Required).ToList();
			if (!parts.Any()) return null;

			string ToArg(UrlPart p)
			{
				if (IsDocumentRequest) return "documentWithId: document";
				
				if (p.HighLevelTypeName.StartsWith("DocumentPath"))
					return "documentWithId: id?.Document, index: id?.Self?.Index, id: id?.Self?.Id";
				

				return $"{p.Name.ToCamelCase()}: {p.Name.ToCamelCase()}";
			}

			return string.Join(", ", parts.Select(p => ToArg(p)));
		}

		public string SelectorChainedDefaults()
		{
			var parts = UrlParts.Where(p => !p.Required).ToList();
			if (!parts.Any()) return null;

			return "." + string.Join(".", parts.Select(p => $"{p.Name.ToPascalCase()}({p.Name.ToCamelCase()}: {p.Name.ToCamelCase()})"));
		}
	}
}