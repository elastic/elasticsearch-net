// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
