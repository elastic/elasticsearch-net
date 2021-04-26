/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Collections.Generic;
using System.Collections.ObjectModel;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Formatters;


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
