// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch;

public sealed partial class PropertyName : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		var name = Name ?? Expression?.ToString() ?? Property?.Name;

		// Descriptor mode: the fluent overloads take an Expression<Func<TDocument, object?>> directly, so emit a bare
		// member-access lambda (x => x.Path) rather than the Infer.Property<T>(...) factory the initializer mode uses.
		if (writer.EffectiveSyntaxMode == RequestConverter.SyntaxMode.Descriptor
			&& writer.Options.UseStronglyTypedDocument
			&& RequestConverter.FieldPath.TryToLambdaBody(name, out var propertyLambda))
		{
			writer.Write(propertyLambda);
			return;
		}

		if (writer.Options.UseStronglyTypedDocument && RequestConverter.FieldPath.TryToLambdaBody(name, out var lambda))
		{
			writer.WriteTypeRef("Elastic.Clients.Elasticsearch.Infer").Write(".Property<")
				.Write(writer.Options.DocumentTypeName).Write(">(").Write(lambda).Write(")");
			return;
		}

		writer.WriteString(name);
	}
}
