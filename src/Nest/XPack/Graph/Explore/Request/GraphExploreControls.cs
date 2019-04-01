using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGraphExploreControls
	{
		[JsonProperty("sample_diversity")]
		SampleDiversity SampleDiversity { get; set; }

		[JsonProperty("sample_size")]
		int? SampleSize { get; set; }

		[JsonProperty("timeout")]
		Time Timeout { get; set; }

		[JsonProperty("use_significance")]
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

		public GraphExploreControlsDescriptor<T> SamleDiversity(Field field, int? maxDocumentsPerValue) =>
			Assign(new SampleDiversity { Field = field, MaxDocumentsPerValue = maxDocumentsPerValue }, (a, v) => a.SampleDiversity = v);

		public GraphExploreControlsDescriptor<T> SamleDiversity(Expression<Func<T, object>> field, int? maxDocumentsPerValue) =>
			Assign(new SampleDiversity { Field = field, MaxDocumentsPerValue = maxDocumentsPerValue }, (a, v) => a.SampleDiversity = v);
	}
}
