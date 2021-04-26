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
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(NestedQuery))]
	public interface INestedQuery : IQuery
	{
		[DataMember(Name ="ignore_unmapped")]
		bool? IgnoreUnmapped { get; set; }

		[DataMember(Name ="inner_hits")]
		IInnerHits InnerHits { get; set; }

		[DataMember(Name ="path")]
		Field Path { get; set; }

		[DataMember(Name ="query")]
		QueryContainer Query { get; set; }

		[DataMember(Name ="score_mode")]
		NestedScoreMode? ScoreMode { get; set; }
	}

	public class NestedQuery : QueryBase, INestedQuery
	{
		public bool? IgnoreUnmapped { get; set; }
		public IInnerHits InnerHits { get; set; }
		public Field Path { get; set; }
		public QueryContainer Query { get; set; }
		public NestedScoreMode? ScoreMode { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Nested = this;

		internal static bool IsConditionless(INestedQuery q) => q.Path == null || q.Query.IsConditionless();
	}

	public class NestedQueryDescriptor<T>
		: QueryDescriptorBase<NestedQueryDescriptor<T>, INestedQuery>
			, INestedQuery where T : class
	{
		protected override bool Conditionless => NestedQuery.IsConditionless(this);
		bool? INestedQuery.IgnoreUnmapped { get; set; }
		IInnerHits INestedQuery.InnerHits { get; set; }
		Field INestedQuery.Path { get; set; }
		QueryContainer INestedQuery.Query { get; set; }
		NestedScoreMode? INestedQuery.ScoreMode { get; set; }

		public NestedQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> selector) =>
			Assign(selector, (a, v) => a.Query = v?.Invoke(new QueryContainerDescriptor<T>()));

		public NestedQueryDescriptor<T> ScoreMode(NestedScoreMode? scoreMode) => Assign(scoreMode, (a, v) => a.ScoreMode = v);

		public NestedQueryDescriptor<T> Path(Field path) => Assign(path, (a, v) => a.Path = v);

		public NestedQueryDescriptor<T> Path<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.Path = v);

		public NestedQueryDescriptor<T> InnerHits(Func<InnerHitsDescriptor<T>, IInnerHits> selector = null) =>
			Assign(selector.InvokeOrDefault(new InnerHitsDescriptor<T>()), (a, v) => a.InnerHits = v);

		public NestedQueryDescriptor<T> IgnoreUnmapped(bool? ignoreUnmapped = true) =>
			Assign(ignoreUnmapped, (a, v) => a.IgnoreUnmapped = v);
	}
}
