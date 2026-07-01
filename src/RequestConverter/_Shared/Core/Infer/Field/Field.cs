// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch;

public sealed partial class Field : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		var name = Name ?? Expression?.ToString() ?? Property?.Name;

		// Descriptor mode: the fluent .Field(...) overload takes an Expression<Func<TDocument, object?>> directly, so emit
		// a bare member-access lambda (x => x.Path) rather than the Infer.Field<T>(...) factory the initializer mode uses.
		// A per-field boost has no fluent overload, so a boosted field falls through to the string form below.
		if (writer.EffectiveSyntaxMode == RequestConverter.SyntaxMode.Descriptor
			&& writer.Options.UseStronglyTypedDocument
			&& !Boost.HasValue
			&& RequestConverter.FieldPath.TryToLambdaBody(name, out var fieldLambda))
		{
			writer.Write(fieldLambda);
			return;
		}

		if (writer.Options.UseStronglyTypedDocument && RequestConverter.FieldPath.TryToLambdaBody(name, out var lambda))
		{
			writer.WriteTypeRef("Elastic.Clients.Elasticsearch.Infer").Write(".Field<")
				.Write(writer.Options.DocumentTypeName).Write(">(").Write(lambda);

			if (Boost.HasValue)
				writer.Write(", ").Write(Boost.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));

			writer.Write(")");
			return;
		}

		// A per-field boost ("title^2") is parsed into Name + Boost, so re-append it or it's lost.
		if (Boost.HasValue)
			name += "^" + Boost.Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

		writer.WriteString(name);
	}
}
