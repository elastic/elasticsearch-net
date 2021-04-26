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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(RangeAggregation))]
	public interface IRangeAggregation : IBucketAggregation
	{
		[DataMember(Name ="field")]
		Field Field { get; set; }

		[DataMember(Name ="ranges")]
		IEnumerable<IAggregationRange> Ranges { get; set; }

		[DataMember(Name ="script")]
		IScript Script { get; set; }
	}

	public class RangeAggregation : BucketAggregationBase, IRangeAggregation
	{
		internal RangeAggregation() { }

		public RangeAggregation(string name) : base(name) { }

		public Field Field { get; set; }
		public IEnumerable<IAggregationRange> Ranges { get; set; }
		public IScript Script { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.Range = this;
	}

	public class RangeAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<RangeAggregationDescriptor<T>, IRangeAggregation, T>, IRangeAggregation
		where T : class
	{
		Field IRangeAggregation.Field { get; set; }

		IEnumerable<IAggregationRange> IRangeAggregation.Ranges { get; set; }

		IScript IRangeAggregation.Script { get; set; }

		public RangeAggregationDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public RangeAggregationDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.Field = v);

		public RangeAggregationDescriptor<T> Script(string script) => Assign((InlineScript)script, (a, v) => a.Script = v);

		public RangeAggregationDescriptor<T> Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(scriptSelector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));

		public RangeAggregationDescriptor<T> Ranges(params Func<AggregationRangeDescriptor, IAggregationRange>[] ranges) =>
			Assign(ranges.Select(r => r(new AggregationRangeDescriptor())), (a, v) => a.Ranges = v);
	}
}
