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
