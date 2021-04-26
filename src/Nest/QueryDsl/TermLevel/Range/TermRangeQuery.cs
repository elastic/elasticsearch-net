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

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(FieldNameQueryFormatter<TermRangeQuery, ITermRangeQuery>))]
	public interface ITermRangeQuery : IRangeQuery
	{
		[DataMember(Name = "gt")]
		string GreaterThan { get; set; }

		[DataMember(Name = "gte")]
		string GreaterThanOrEqualTo { get; set; }

		[DataMember(Name = "lt")]
		string LessThan { get; set; }

		[DataMember(Name = "lte")]
		string LessThanOrEqualTo { get; set; }
	}

	public class TermRangeQuery : FieldNameQueryBase, ITermRangeQuery
	{
		public string GreaterThan { get; set; }
		public string GreaterThanOrEqualTo { get; set; }
		public string LessThan { get; set; }
		public string LessThanOrEqualTo { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Range = this;

		internal static bool IsConditionless(ITermRangeQuery q) => q.Field.IsConditionless()
			|| q.GreaterThanOrEqualTo.IsNullOrEmpty()
			&& q.LessThanOrEqualTo.IsNullOrEmpty()
			&& q.GreaterThan.IsNullOrEmpty()
			&& q.LessThan.IsNullOrEmpty();
	}

	[DataContract]
	public class TermRangeQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<TermRangeQueryDescriptor<T>, ITermRangeQuery, T>
			, ITermRangeQuery where T : class
	{
		protected override bool Conditionless => TermRangeQuery.IsConditionless(this);
		string ITermRangeQuery.GreaterThan { get; set; }
		string ITermRangeQuery.GreaterThanOrEqualTo { get; set; }
		string ITermRangeQuery.LessThan { get; set; }
		string ITermRangeQuery.LessThanOrEqualTo { get; set; }

		public TermRangeQueryDescriptor<T> GreaterThan(string from) => Assign(from, (a, v) => a.GreaterThan = v);

		public TermRangeQueryDescriptor<T> GreaterThanOrEquals(string from) => Assign(from, (a, v) => a.GreaterThanOrEqualTo = v);

		public TermRangeQueryDescriptor<T> LessThan(string to) => Assign(to, (a, v) => a.LessThan = v);

		public TermRangeQueryDescriptor<T> LessThanOrEquals(string to) => Assign(to, (a, v) => a.LessThanOrEqualTo = v);
	}
}
