using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ApiGenerator.Domain
{
	public class Constructor
	{
		private const string Indent = "\r\n\t\t";
		public string AdditionalCode { get; set; } = string.Empty;
		public bool Parameterless { get; set; }
		public string Body { get; set; }
		public string Description { get; set; }
		public string Generated { get; set; }
		public string Url { get; set; }

		public static IEnumerable<Constructor> DescriptorConstructors(CsharpMethod method)
		{
			var m = method.DescriptorType;
			var generic = method.DescriptorTypeGeneric.Replace("<", "").Replace(">", "").Split(",").First().Trim();
			var generateGeneric = !string.IsNullOrEmpty(method.DescriptorTypeGeneric);
			return GenerateConstructors(method, true, generateGeneric, m, generic);
		}

		public static IEnumerable<Constructor> RequestConstructors(CsharpMethod method, bool inheritsFromPlainRequestBase)
		{
			var generic = method.RequestTypeGeneric.Replace("<", "").Replace(">", "").Split(",").First().Trim();
			return GenerateConstructors(method, inheritsFromPlainRequestBase, !inheritsFromPlainRequestBase, method.RequestType, generic);
		}

		private static IEnumerable<Constructor> GenerateConstructors(
			CsharpMethod method,
			bool inheritsFromPlainRequestBase,
			bool generateGeneric,
			string typeName, string generic
		)
		{
			var url = method.Url;
			var ctors = new List<Constructor>();

			var paths = url.ExposedApiPaths.ToList();

			if (url.IsPartless) return ctors;

			ctors.AddRange(from path in paths
				let baseArgs = inheritsFromPlainRequestBase ? path.RequestBaseArguments : path.TypedSubClassBaseArguments
				let constParams = path.ConstructorArguments
				let generated = $"public {typeName}({constParams}) : base({baseArgs})"
				select new Constructor
				{
					Parameterless = string.IsNullOrEmpty(constParams),
					Generated = generated,
					Description = path.GetXmlDocs(Indent),
					//Body = isDocumentApi ? $" => Q(\"routing\", new Routing(() => AutoRouteDocument()));" : string.Empty
					Body = string.Empty
				});

			if (generateGeneric && !string.IsNullOrWhiteSpace(generic))
			{
				ctors.AddRange(from path in paths.Where(path => path.HasResolvableArguments)
					let baseArgs = path.AutoResolveBaseArguments(generic)
					let constructorArgs = path.AutoResolveConstructorArguments
					let baseOrThis = inheritsFromPlainRequestBase ? "this" : "base"
					let generated = $"public {typeName}({constructorArgs}) : {baseOrThis}({baseArgs})"
					select new Constructor
					{
						Parameterless = string.IsNullOrEmpty(constructorArgs),
						Generated = generated,
						Description = path.GetXmlDocs(Indent, skipResolvable: true),
						Body = string.Empty
					});

				if (url.TryGetDocumentApiPath(out var docPath))
				{
					var docPathBaseArgs = docPath.DocumentPathBaseArgument(generic);
					var docPathConstArgs = docPath.DocumentPathConstructorArgument(generic);
					ctors.Add(new Constructor
					{
						Parameterless = string.IsNullOrEmpty(docPathConstArgs),
						Generated = $"public {typeName}({docPathConstArgs}) : this({docPathBaseArgs})",
						AdditionalCode = $"partial void DocumentFromPath({generic} document);",
						Description = docPath.GetXmlDocs(Indent, skipResolvable: true, documentConstructor: true),
						Body = "=> DocumentFromPath(documentWithId);"
					});
				}
			}
			var constructors = ctors.GroupBy(c => c.Generated.Split(new[] { ':' }, 2)[0]).Select(g => g.Last()).ToList();
			if (!constructors.Any(c=>c.Parameterless))
			{
				constructors.Add(new Constructor
				{
					Parameterless = true,
					Generated = $"internal {typeName}() : base()",
					Description =
						$"///<summary>Used for serialization purposes, making sure we have a parameterless constructor</summary>{Indent}[SerializationConstructor]",
				});
			}
			return constructors;
		}
	}
}
