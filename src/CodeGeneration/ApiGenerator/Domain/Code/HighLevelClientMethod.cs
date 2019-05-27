using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CsQuery.Engine.PseudoClassSelectors;
using Microsoft.CodeAnalysis;

namespace ApiGenerator.Domain
{
	public class HighLevelModel
	{
		public CsharpNames CsharpNames { get; set; }
		public FluentMethod Fluent { get; set; }
		public BoundFluentMethod FluentBound { get; set; }
		public InitializerMethod Initializer { get; set; }

	}

	public abstract class MethodSyntaxBase
	{
		protected MethodSyntaxBase(CsharpNames names) => (CsharpNames) = (names);
		
		protected CsharpNames CsharpNames { get; }
		
		public string ResponseName => CsharpNames.GenericOrNonGenericResponseName;

		public string DocumentationCref => CsharpNames.RequestInterfaceName;
		public string ResponseGenerics => CsharpNames.ResponseGenerics.Any() ? $"<{string.Join(", ", CsharpNames.ResponseGenerics)}>" : null;
		
		public abstract string GenericWhereClause { get; }
	}

	public class InitializerMethod : MethodSyntaxBase
	{
		public InitializerMethod(CsharpNames names) : base(names) { }

		public string MethodName => CsharpNames.MethodName;
		public string ArgumentType => CsharpNames.RequestInterfaceName;
		
		public override string GenericWhereClause =>
			string.Join(" ", CsharpNames.ResponseGenerics
				.Where(g=>g.Contains("Document"))
				.Select(g=>$"where {g} : class")
			);

		private bool IsCatMethod => CsharpNames.Namespace == "Cat";
		
		public string DispatchMethod => IsCatMethod ? "DoCat" : "DoRequest";

		public string DispatchGenerics => IsCatMethod 
			? $"<{ArgumentType},{CsharpNames.ParametersName},{CsharpNames.RequestName.Replace("Request", "Record")}>"
			: $"<{ArgumentType},{ResponseName}>";

		public string DispatchParameters => IsCatMethod ? "request" : "request, request.RequestParameters";
	
	}

	public abstract class FluentSyntaxBase : MethodSyntaxBase
	{
		protected FluentSyntaxBase(CsharpNames names, IReadOnlyCollection<UrlPart> parts, bool selectorIsOptional) : base(names) =>
			(UrlParts, SelectorIsOptional) = (CreateDescriptorArgs(parts), selectorIsOptional);

		private IReadOnlyCollection<UrlPart> UrlParts { get; }
		private bool SelectorIsOptional { get; }

		public string MethodName => CsharpNames.MethodName;

		public string OptionalSelectorSuffix => SelectorIsOptional ? " = null" : string.Empty;

		public virtual string DescriptorName => CsharpNames.GenericOrNonGenericDescriptorName;
		public string Selector => $"Func<{DescriptorName}, {CsharpNames.RequestInterfaceName}>";

		public virtual string MethodGenerics => CsharpNames.HighLevelDescriptorMethodGenerics.Any()
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
			if (idPart != null && UrlInformation.IsADocumentRoute(parts))
			{
				requiredParts.Remove(idPart);
				var generic = CsharpNames.GenericsDeclaredOnDescriptor.Replace("<", "").Replace(">", "").Split(",").First().Trim();
				requiredParts.Add(new UrlPart
				{
					Name = idPart.Name,
					Required = idPart.Required,
					Description = idPart.Description,
					Options = idPart.Options,
					Type = idPart.Type,
					ClrTypeNameOverride = $"DocumentPath<{generic}>"
				});
			}

			return requiredParts;
		}
		
		public string DescriptorArguments()
		{
			if (!UrlParts.Any()) return null;

			string Optional(UrlPart p) => !p.Required && SelectorIsOptional ? " = null" : string.Empty;
			return string.Join(", ", UrlParts.Select(p => $"{p.ClrTypeName} {p.Name.ToCamelCase()}{Optional(p)}")) + ", ";
		}

		public string SelectorArguments()
		{
			var parts = UrlParts.Where(p => p.Required).ToList();
			if (!parts.Any()) return null;

			string ToArg(UrlPart p)
			{
				if (p.ClrTypeName.StartsWith("DocumentPath"))
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

	public class FluentMethod : FluentSyntaxBase
	{
		public FluentMethod(CsharpNames names, IReadOnlyCollection<UrlPart> parts, bool selectorIsOptional) : base(names, parts, selectorIsOptional) { }

		public override string GenericWhereClause =>
			string.Join(" ", CsharpNames.HighLevelDescriptorMethodGenerics
				.Where(g => g.Contains("Document"))
				.Select(g => $"where {g} : class")
			);
	}
	public class BoundFluentMethod : FluentSyntaxBase
	{
		public BoundFluentMethod(CsharpNames names, IReadOnlyCollection<UrlPart> parts, bool selectorIsOptional) : base(names, parts, selectorIsOptional) { }

		public override string DescriptorName => $"{CsharpNames.DescriptorName}<{CsharpNames.DescriptorBoundDocumentGeneric}>";
		public override string GenericWhereClause => $"where {CsharpNames.DescriptorBoundDocumentGeneric} : class";
		public override string MethodGenerics => $"<{CsharpNames.DescriptorBoundDocumentGeneric}>";
	}

	public class FluentSyntaxView
	{
		public FluentSyntaxView(FluentSyntaxBase syntax, bool async) => (Syntax , Async) = (syntax, async);

		public FluentSyntaxBase Syntax { get; }

		public bool Async { get; }
	}
	public class InitializerSyntaxView
	{
		public InitializerSyntaxView(InitializerMethod  syntax, bool async) => (Syntax , Async) = (syntax, async);

		public InitializerMethod Syntax { get; }

		public bool Async { get; }
	}

}
