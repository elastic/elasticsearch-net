// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Linq;
using ApiGenerator.Domain.Specification;

namespace ApiGenerator.Domain.Code.HighLevel.Methods 
{
	public class FluentMethod : FluentSyntaxBase
	{
		public FluentMethod(CsharpNames names, IReadOnlyCollection<UrlPart> parts, bool selectorIsOptional, string link, string summary) 
			: base(names, parts, selectorIsOptional, link, summary) { }

		public override string GenericWhereClause =>
			string.Join(" ", CsharpNames.HighLevelDescriptorMethodGenerics
				.Where(g => g.Contains("Document"))
				.Select(g => $"where {g} : class")
			);
	}
}