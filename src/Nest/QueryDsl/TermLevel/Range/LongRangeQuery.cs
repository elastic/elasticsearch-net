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
	[JsonFormatter(typeof(FieldNameQueryFormatter<LongRangeQuery, ILongRangeQuery>))]
	public interface ILongRangeQuery : IRangeQuery
	{
		[DataMember(Name ="gt")]
		long? GreaterThan { get; set; }

		[DataMember(Name ="gte")]
		long? GreaterThanOrEqualTo { get; set; }

		[DataMember(Name ="lt")]
		long? LessThan { get; set; }

		[DataMember(Name ="lte")]
		long? LessThanOrEqualTo { get; set; }

		[DataMember(Name ="relation")]
		RangeRelation? Relation { get; set; }
	}

	public class LongRangeQuery : FieldNameQueryBase, ILongRangeQuery
	{
		public long? GreaterThan { get; set; }
		public long? GreaterThanOrEqualTo { get; set; }
		public long? LessThan { get; set; }
		public long? LessThanOrEqualTo { get; set; }

		public RangeRelation? Relation { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Range = this;

		internal static bool IsConditionless(ILongRangeQuery q) => q.Field.IsConditionless()
			|| q.GreaterThanOrEqualTo == null
			&& q.LessThanOrEqualTo == null
			&& q.GreaterThan == null
			&& q.LessThan == null;
	}

	[DataContract]
	public class LongRangeQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<LongRangeQueryDescriptor<T>, ILongRangeQuery, T>
			, ILongRangeQuery where T : class
	{
		protected override bool Conditionless => LongRangeQuery.IsConditionless(this);
		long? ILongRangeQuery.GreaterThan { get; set; }
		long? ILongRangeQuery.GreaterThanOrEqualTo { get; set; }
		long? ILongRangeQuery.LessThan { get; set; }
		long? ILongRangeQuery.LessThanOrEqualTo { get; set; }
		RangeRelation? ILongRangeQuery.Relation { get; set; }

		public LongRangeQueryDescriptor<T> Relation(RangeRelation? relation) => Assign(relation, (a, v) => a.Relation = v);

		public LongRangeQueryDescriptor<T> GreaterThan(long? from) => Assign(from, (a, v) => a.GreaterThan = v);

		public LongRangeQueryDescriptor<T> GreaterThanOrEquals(long? from) => Assign(from, (a, v) => a.GreaterThanOrEqualTo = v);

		public LongRangeQueryDescriptor<T> LessThan(long? to) => Assign(to, (a, v) => a.LessThan = v);

		public LongRangeQueryDescriptor<T> LessThanOrEquals(long? to) => Assign(to, (a, v) => a.LessThanOrEqualTo = v);
	}
}
