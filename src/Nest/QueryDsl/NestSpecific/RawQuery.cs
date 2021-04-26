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

namespace Nest
{
	public interface IRawQuery : IQuery
	{
		string Raw { get; set; }
	}

	public class RawQuery : QueryBase, IRawQuery
	{
		public RawQuery() { }

		public RawQuery(string rawQuery) => Raw = rawQuery;

		public string Raw { get; set; }

		protected override bool Conditionless => Raw.IsNullOrEmpty();

		internal override void InternalWrapInContainer(IQueryContainer container) => container.RawQuery = this;
	}

	public class RawQueryDescriptor : QueryDescriptorBase<RawQueryDescriptor, IRawQuery>, IRawQuery
	{
		public RawQueryDescriptor() { }

		public RawQueryDescriptor(string rawQuery) => Self.Raw = rawQuery;

		protected override bool Conditionless => Self.Raw.IsNullOrEmpty();
		string IRawQuery.Raw { get; set; }

		public RawQueryDescriptor Raw(string rawQuery) =>
			Assign(rawQuery, (a, v) => a.Raw = v);
	}
}
