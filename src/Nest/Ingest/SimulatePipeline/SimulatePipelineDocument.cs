// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface ISimulatePipelineDocument
	{
		[DataMember(Name = "_id")]
		Id Id { get; set; }

		[DataMember(Name = "_index")]
		IndexName Index { get; set; }

		[DataMember(Name = "_source")]
		[JsonFormatter(typeof(SourceFormatter<>))]
		object Source { get; set; }
	}

	public class SimulatePipelineDocument : ISimulatePipelineDocument
	{
		private object _source;

		public Id Id { get; set; }
		public IndexName Index { get; set; }

		public object Source
		{
			get => _source;
			set
			{
				_source = value;
				Index = Index ?? _source.GetType();
				Id = Id ?? Id.From(_source);
			}
		}
	}

	public class SimulatePipelineDocumentDescriptor
		: DescriptorBase<SimulatePipelineDocumentDescriptor, ISimulatePipelineDocument>, ISimulatePipelineDocument
	{
		Id ISimulatePipelineDocument.Id { get; set; }
		IndexName ISimulatePipelineDocument.Index { get; set; }
		object ISimulatePipelineDocument.Source { get; set; }

		public SimulatePipelineDocumentDescriptor Id(Id id) => Assign(id, (a, v) => a.Id = v);

		public SimulatePipelineDocumentDescriptor Index(IndexName index) => Assign(index, (a, v) => a.Index = v);

		public SimulatePipelineDocumentDescriptor Source<T>(T source) where T : class => Assign(source, (a, v) =>
		{
			a.Source = v;
			a.Index = a.Index ?? v.GetType();
			a.Id = a.Id ?? Nest.Id.From(v);
		});
	}

	public class SimulatePipelineDocumentsDescriptor
		: DescriptorPromiseBase<SimulatePipelineDocumentsDescriptor, IList<ISimulatePipelineDocument>>
	{
		public SimulatePipelineDocumentsDescriptor() : base(new List<ISimulatePipelineDocument>()) { }

		public SimulatePipelineDocumentsDescriptor Document(Func<SimulatePipelineDocumentDescriptor, ISimulatePipelineDocument> selector) =>
			Assign(selector, (a, v) => a.AddIfNotNull(v?.Invoke(new SimulatePipelineDocumentDescriptor())));
	}
}
