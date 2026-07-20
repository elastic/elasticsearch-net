// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch;

public sealed partial class Fields : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		// The lambda form applies only when every member maps to a member-access lambda and none carries a boost (there is no
		// per-field boost expression overload); otherwise fall back to the string array.
		if (writer.Options.UseStronglyTypedDocument && TryGetLambdaBodies(out var lambdas))
		{
			// Descriptor mode: the fluent setter has a `params Expression<Func<TDocument, object?>>[]` overload, so emit bare
			// member-access lambdas directly rather than the Infer.Fields<T>(...) factory the initializer mode uses.
			if (writer.EffectiveSyntaxMode != RequestConverter.SyntaxMode.Descriptor)
				writer.WriteTypeRef("Elastic.Clients.Elasticsearch.Infer").Write(".Fields<")
					.Write(writer.Options.DocumentTypeName).Write(">(");

			for (var i = 0; i < lambdas.Count; i++)
			{
				if (i > 0)
					writer.Write(", ");

				writer.Write(lambdas[i]);
			}

			if (writer.EffectiveSyntaxMode != RequestConverter.SyntaxMode.Descriptor)
				writer.Write(")");

			return;
		}

		writer.WriteImplicitArray(ListOfFields, static (w, field) => w.WriteValue(field));
	}

	private bool TryGetLambdaBodies(out System.Collections.Generic.List<string> lambdas)
	{
		lambdas = new System.Collections.Generic.List<string>(ListOfFields.Count);
		if (ListOfFields.Count == 0)
			return false;

		foreach (var field in ListOfFields)
		{
			var name = field.Name ?? field.Expression?.ToString() ?? field.Property?.Name;
			if (field.Boost.HasValue || !RequestConverter.FieldPath.TryToLambdaBody(name, out var lambda))
				return false;

			lambdas.Add(lambda!);
		}

		return true;
	}
}
