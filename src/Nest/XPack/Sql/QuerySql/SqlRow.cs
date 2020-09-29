// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Collections.ObjectModel;
using Nest.Utf8Json;
namespace Nest
{
	[JsonFormatter(typeof(SqlRowFormatter))]
	public class SqlRow : ReadOnlyCollection<SqlValue>
	{
		public SqlRow(IList<SqlValue> list) : base(list) { }
	}

	internal class SqlRowFormatter : IJsonFormatter<SqlRow>
	{
		private static readonly InterfaceListFormatter<SqlValue> SqlValueFormatter = new InterfaceListFormatter<SqlValue>();

		public void Serialize(ref JsonWriter writer, SqlRow value, IJsonFormatterResolver formatterResolver) =>
			SqlValueFormatter.Serialize(ref writer, value, formatterResolver);

		public SqlRow Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var values = SqlValueFormatter.Deserialize(ref reader, formatterResolver);
			return values == null
				? null
				: new SqlRow(values);
		}
	}
}
