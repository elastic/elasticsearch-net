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
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(RescoreQuery))]
	public interface IRescoreQuery
	{
		[DataMember(Name = "rescore_query")]
		QueryContainer Query { get; set; }

		[DataMember(Name = "query_weight")]
		double? QueryWeight { get; set; }

		[DataMember(Name = "rescore_query_weight")]
		double? RescoreQueryWeight { get; set; }

		[DataMember(Name = "score_mode")]
		ScoreMode? ScoreMode { get; set; }
	}

	public class RescoreQuery : IRescoreQuery
	{
		public QueryContainer Query { get; set; }
		public double? QueryWeight { get; set; }
		public double? RescoreQueryWeight { get; set; }
		public ScoreMode? ScoreMode { get; set; }
	}

	public class RescoreQueryDescriptor<T> : DescriptorBase<RescoreQueryDescriptor<T>, IRescoreQuery>, IRescoreQuery
		where T : class
	{
		QueryContainer IRescoreQuery.Query { get; set; }
		double? IRescoreQuery.QueryWeight { get; set; }
		double? IRescoreQuery.RescoreQueryWeight { get; set; }
		ScoreMode? IRescoreQuery.ScoreMode { get; set; }

		public virtual RescoreQueryDescriptor<T> QueryWeight(double? queryWeight) => Assign(queryWeight, (a, v) => a.QueryWeight = v);

		public virtual RescoreQueryDescriptor<T> RescoreQueryWeight(double? rescoreQueryWeight) =>
			Assign(rescoreQueryWeight, (a, v) => a.RescoreQueryWeight = v);

		public virtual RescoreQueryDescriptor<T> ScoreMode(ScoreMode? scoreMode) => Assign(scoreMode, (a, v) => a.ScoreMode = v);

		public virtual RescoreQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> query) =>
			Assign(query, (a, v) => a.Query = v?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
