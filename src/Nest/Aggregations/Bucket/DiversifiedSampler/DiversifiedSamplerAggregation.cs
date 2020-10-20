using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(DiversifiedSamplerAggregation))]
	public interface IDiversifiedSamplerAggregation : IBucketAggregation
	{
		[DataMember(Name ="execution_hint")]
		DiversifiedSamplerAggregationExecutionHint? ExecutionHint { get; set; }

		[DataMember(Name = "field")]
		Field Field { get; set; }

		[DataMember(Name ="max_docs_per_value")]
		int? MaxDocsPerValue { get; set; }

		[DataMember(Name ="script")]
		IScript Script { get; set; }

		[DataMember(Name ="shard_size")]
		int? ShardSize { get; set; }
	}

	public class DiversifiedSamplerAggregation : BucketAggregationBase, IDiversifiedSamplerAggregation
	{
		internal DiversifiedSamplerAggregation() { }

		public DiversifiedSamplerAggregation(string name) : base(name) { }

		public DiversifiedSamplerAggregationExecutionHint? ExecutionHint { get; set; }
		public Field Field { get; set; }
		public int? MaxDocsPerValue { get; set; }
		public IScript Script { get; set; }
		public int? ShardSize { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.DiversifiedSampler = this;
	}

	public class DiversifiedSamplerAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<DiversifiedSamplerAggregationDescriptor<T>, IDiversifiedSamplerAggregation, T>, IDiversifiedSamplerAggregation
		where T : class
	{
		DiversifiedSamplerAggregationExecutionHint? IDiversifiedSamplerAggregation.ExecutionHint { get; set; }
		Field IDiversifiedSamplerAggregation.Field { get; set; }
		int? IDiversifiedSamplerAggregation.MaxDocsPerValue { get; set; }
		IScript IDiversifiedSamplerAggregation.Script { get; set; }
		int? IDiversifiedSamplerAggregation.ShardSize { get; set; }

		public DiversifiedSamplerAggregationDescriptor<T> ExecutionHint(DiversifiedSamplerAggregationExecutionHint? executionHint) =>
			Assign(executionHint, (a, v) => a.ExecutionHint = v);

		public DiversifiedSamplerAggregationDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public DiversifiedSamplerAggregationDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.Field = v);

		public DiversifiedSamplerAggregationDescriptor<T> MaxDocsPerValue(int? maxDocs) => Assign(maxDocs, (a, v) => a.MaxDocsPerValue = v);

		public DiversifiedSamplerAggregationDescriptor<T> Script(string script) => Assign((InlineScript)script, (a, v) => a.Script = v);

		public DiversifiedSamplerAggregationDescriptor<T> Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(scriptSelector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));

		public DiversifiedSamplerAggregationDescriptor<T> ShardSize(int? shardSize) => Assign(shardSize, (a, v) => a.ShardSize = v);
	}
}
