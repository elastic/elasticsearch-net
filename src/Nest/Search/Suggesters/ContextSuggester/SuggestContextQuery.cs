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
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(SuggestContextQuery))]
	public interface ISuggestContextQuery
	{
		[DataMember(Name = "boost")]
		double? Boost { get; set; }

		[DataMember(Name = "context")]
		Context Context { get; set; }

		[DataMember(Name = "neighbours")]
		Union<Distance[], int[]> Neighbours { get; set; }

		[DataMember(Name = "precision")]
		Union<Distance, int> Precision { get; set; }

		[DataMember(Name = "prefix")]
		bool? Prefix { get; set; }
	}

	public class SuggestContextQuery : ISuggestContextQuery
	{
		public double? Boost { get; set; }

		public Context Context { get; set; }

		public Union<Distance[], int[]> Neighbours { get; set; }

		public Union<Distance, int> Precision { get; set; }

		public bool? Prefix { get; set; }
	}

	public class SuggestContextQueryDescriptor<T>
		: DescriptorBase<SuggestContextQueryDescriptor<T>, ISuggestContextQuery>, ISuggestContextQuery
	{
		double? ISuggestContextQuery.Boost { get; set; }
		Context ISuggestContextQuery.Context { get; set; }
		Union<Distance[], int[]> ISuggestContextQuery.Neighbours { get; set; }
		Union<Distance, int> ISuggestContextQuery.Precision { get; set; }
		bool? ISuggestContextQuery.Prefix { get; set; }

		public SuggestContextQueryDescriptor<T> Prefix(bool? prefix = true) => Assign(prefix, (a, v) => a.Prefix = v);

		public SuggestContextQueryDescriptor<T> Boost(double? boost) => Assign(boost, (a, v) => a.Boost = v);

		public SuggestContextQueryDescriptor<T> Context(string context) => Assign(context, (a, v) => a.Context = v);

		public SuggestContextQueryDescriptor<T> Context(GeoLocation context) => Assign(context, (a, v) => a.Context = v);

		public SuggestContextQueryDescriptor<T> Precision(Distance precision) => Assign(precision, (a, v) => a.Precision = v);

		public SuggestContextQueryDescriptor<T> Precision(int? precision) => Assign(precision, (a, v) => a.Precision = v);

		public SuggestContextQueryDescriptor<T> Neighbours(params int[] neighbours) => Assign(neighbours, (a, v) => a.Neighbours = v);

		public SuggestContextQueryDescriptor<T> Neighbours(params Distance[] neighbours) => Assign(neighbours, (a, v) => a.Neighbours = v);
	}

	public class SuggestContextQueriesDescriptor<T>
		: DescriptorPromiseBase<SuggestContextQueriesDescriptor<T>, IDictionary<string, IList<ISuggestContextQuery>>>
	{
		public SuggestContextQueriesDescriptor() : base(new Dictionary<string, IList<ISuggestContextQuery>>()) { }

		public SuggestContextQueriesDescriptor<T> Context(string name,
			params Func<SuggestContextQueryDescriptor<T>, ISuggestContextQuery>[] categoryDescriptors
		) =>
			AddContextQueries(name, categoryDescriptors?.Select(d => d?.Invoke(new SuggestContextQueryDescriptor<T>())).ToList());

		private SuggestContextQueriesDescriptor<T> AddContextQueries(string name, List<ISuggestContextQuery> contextQueries)
		{
			if (contextQueries != null)
				PromisedValue.Add(name, contextQueries);

			return this;
		}
	}
}
