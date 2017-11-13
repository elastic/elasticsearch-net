using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface ISimulatePipelineDocument
	{
		[JsonProperty("_index")]
		IndexName Index { get; set; }

		[JsonProperty("_type")]
		TypeName Type { get; set; }

		[JsonProperty("_id")]
		Id Id { get; set; }

		[JsonProperty("_source")]
		[JsonConverter(typeof(SourceConverter))]
		object Source { get; set; }
	}

	public class SimulatePipelineDocument : ISimulatePipelineDocument
	{
		public IndexName Index { get; set; }

		public TypeName Type { get; set; }

		public Id Id { get; set; }

		private object _source;
		public object Source
		{
			get => _source;
			set
			{
				_source = value;
				this.Index = this.Index ?? _source.GetType();
				this.Type = this.Type ?? _source.GetType();
				this.Id = this.Id ?? Id.From(_source);
			}
		}
	}

	public class SimulatePipelineDocumentDescriptor
		: DescriptorBase<SimulatePipelineDocumentDescriptor, ISimulatePipelineDocument>, ISimulatePipelineDocument
	{
		Id ISimulatePipelineDocument.Id { get; set; }
		IndexName ISimulatePipelineDocument.Index { get; set; }
		TypeName ISimulatePipelineDocument.Type { get; set; }
		object ISimulatePipelineDocument.Source { get; set; }

		public SimulatePipelineDocumentDescriptor Id(Id id) => Assign(a => a.Id = id);
		public SimulatePipelineDocumentDescriptor Index(IndexName index) => Assign(a => a.Index = index);
		public SimulatePipelineDocumentDescriptor Type(TypeName type) => Assign(a => a.Type = type);
		public SimulatePipelineDocumentDescriptor Source<T>(T source) where T : class => Assign(a =>
		{
			a.Source = source;
			a.Index = a.Index ?? source.GetType();
			a.Type = a.Type ?? source.GetType();
			a.Id = a.Id ?? Nest.Id.From(source);
		});
	}

	public class SimulatePipelineDocumentsDescriptor
		: DescriptorPromiseBase<SimulatePipelineDocumentsDescriptor, IList<ISimulatePipelineDocument>>
	{
		public SimulatePipelineDocumentsDescriptor() : base(new List<ISimulatePipelineDocument>()) { }

		public SimulatePipelineDocumentsDescriptor Document(Func<SimulatePipelineDocumentDescriptor, ISimulatePipelineDocument> selector) =>
			this.Assign(a => a.AddIfNotNull(selector?.Invoke(new SimulatePipelineDocumentDescriptor())));
	}
}
