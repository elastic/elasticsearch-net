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

using Nest.Utf8Json;

namespace Nest
{
	public interface ISortOrder
	{
		string Key { get; set; }

		SortOrder Order { get; set; }
	}

	[JsonFormatter(typeof(SortOrderFormatter<HistogramOrder>))]
	public class HistogramOrder : ISortOrder
	{
		public static HistogramOrder CountAscending => new HistogramOrder { Key = "_count", Order = SortOrder.Ascending };
		public static HistogramOrder CountDescending => new HistogramOrder { Key = "_count", Order = SortOrder.Descending };
		public string Key { get; set; }

		public static HistogramOrder KeyAscending => new HistogramOrder { Key = "_key", Order = SortOrder.Ascending };
		public static HistogramOrder KeyDescending => new HistogramOrder { Key = "_key", Order = SortOrder.Descending };
		public SortOrder Order { get; set; }
	}
}
