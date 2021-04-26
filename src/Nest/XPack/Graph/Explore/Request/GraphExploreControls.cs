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

namespace Nest
{
	public interface IGraphExploreControls
	{
		[DataMember(Name ="sample_diversity")]
		SampleDiversity SampleDiversity { get; set; }

		[DataMember(Name ="sample_size")]
		int? SampleSize { get; set; }

		[DataMember(Name ="timeout")]
		Time Timeout { get; set; }

		[DataMember(Name ="use_significance")]
		bool? UseSignificance { get; set; }
	}

	public class GraphExploreControls : IGraphExploreControls
	{
		public SampleDiversity SampleDiversity { get; set; }

		public int? SampleSize { get; set; }

		public Time Timeout { get; set; }
		public bool? UseSignificance { get; set; }
	}

	public class GraphExploreControlsDescriptor<T> : DescriptorBase<GraphExploreControlsDescriptor<T>, IGraphExploreControls>, IGraphExploreControls
		where T : class
	{
		SampleDiversity IGraphExploreControls.SampleDiversity { get; set; }
		int? IGraphExploreControls.SampleSize { get; set; }
		Time IGraphExploreControls.Timeout { get; set; }
		bool? IGraphExploreControls.UseSignificance { get; set; }

		public GraphExploreControlsDescriptor<T> UseSignificance(bool? useSignificance = true) => Assign(useSignificance, (a, v) => a.UseSignificance = v);

		public GraphExploreControlsDescriptor<T> SampleSize(int? sampleSize) => Assign(sampleSize, (a, v) => a.SampleSize = v);

		public GraphExploreControlsDescriptor<T> Timeout(Time time) => Assign(time, (a, v) => a.Timeout = v);

		public GraphExploreControlsDescriptor<T> SampleDiversity(Field field, int? maxDocumentsPerValue) =>
			Assign(new SampleDiversity { Field = field, MaxDocumentsPerValue = maxDocumentsPerValue }, (a, v) => a.SampleDiversity = v);

		public GraphExploreControlsDescriptor<T> SampleDiversity<TValue>(Expression<Func<T, TValue>> field, int? maxDocumentsPerValue) =>
			Assign(new SampleDiversity { Field = field, MaxDocumentsPerValue = maxDocumentsPerValue }, (a, v) => a.SampleDiversity = v);
	}
}
