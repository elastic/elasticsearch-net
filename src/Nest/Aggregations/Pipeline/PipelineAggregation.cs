using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IPipelineAggregation : IAggregation
	{
		[JsonProperty("buckets_path")]
		string BucketsPath { get; set; }

		[JsonProperty("gap_policy")]
		GapPolicy? GapPolicy { get; set; }

		[JsonProperty("format")]
		string Format { get; set; }
	}

	public abstract class PipelineAggregationBase : AggregationBase, IPipelineAggregation
	{
		public PipelineAggregationBase(string name, string bucketsPath) : base(name)
		{
			this.BucketsPath = bucketsPath;
		}

		public string BucketsPath { get; set; }
		public string Format { get; set; }
		public GapPolicy? GapPolicy { get; set; }
	}

	public abstract class PipelineAggregationDescriptorBase<TPipelineAggregation, TPipelineAggregationInterface>
		: IPipelineAggregation
		where TPipelineAggregation : PipelineAggregationDescriptorBase<TPipelineAggregation, TPipelineAggregationInterface>
			, TPipelineAggregationInterface, IPipelineAggregation
		where TPipelineAggregationInterface : class, IPipelineAggregation
	{
		string IPipelineAggregation.BucketsPath { get; set; }
		string IPipelineAggregation.Format { get; set; }
		GapPolicy? IPipelineAggregation.GapPolicy { get; set; }

		protected TPipelineAggregation Assign(Action<TPipelineAggregationInterface> assigner) =>
			Fluent.Assign(((TPipelineAggregation)this), assigner);

		public TPipelineAggregation BucketsPath(string bucketsPath) => Assign(a => a.BucketsPath = bucketsPath);

		public TPipelineAggregation Format(string format) => Assign(a => a.Format = format);

		public TPipelineAggregation GapPolicy(GapPolicy gapPolicy) => Assign(a => a.GapPolicy = gapPolicy);
	}
}
