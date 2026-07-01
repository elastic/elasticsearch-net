// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch;

public sealed partial class Fields : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		// Infer.Fields<T>(...) takes only expressions, with no per-field boost overload, so the lambda form applies only
		// when every member maps to a member-access lambda and none carries a boost; otherwise fall back to the string array.
		if (writer.Options.UseStronglyTypedDocument && TryGetLambdaBodies(out var lambdas))
		{
			writer.WriteTypeRef("Elastic.Clients.Elasticsearch.Infer").Write(".Fields<")
				.Write(writer.Options.DocumentTypeName).Write(">(");

			for (var i = 0; i < lambdas.Count; i++)
			{
				if (i > 0)
					writer.Write(", ");

				writer.Write(lambdas[i]);
			}

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
