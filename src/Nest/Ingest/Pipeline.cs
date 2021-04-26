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
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(Pipeline))]
	public interface IPipeline
	{
		[DataMember(Name ="description")]
		string Description { get; set; }

		[DataMember(Name ="on_failure")]
		IEnumerable<IProcessor> OnFailure { get; set; }

		[DataMember(Name ="processors")]
		IEnumerable<IProcessor> Processors { get; set; }
	}

	public class Pipeline : IPipeline
	{
		public string Description { get; set; }

		public IEnumerable<IProcessor> OnFailure { get; set; }

		public IEnumerable<IProcessor> Processors { get; set; }
	}

	public class PipelineDescriptor : DescriptorBase<PipelineDescriptor, IPipeline>, IPipeline
	{
		string IPipeline.Description { get; set; }
		IEnumerable<IProcessor> IPipeline.OnFailure { get; set; }
		IEnumerable<IProcessor> IPipeline.Processors { get; set; }

		/// <inheritdoc />
		public PipelineDescriptor Description(string description) => Assign(description, (a, v) => a.Description = v);

		/// <inheritdoc />
		public PipelineDescriptor Processors(IEnumerable<IProcessor> processors) => Assign(processors.ToListOrNullIfEmpty(), (a, v) => a.Processors = v);

		/// <inheritdoc />
		public PipelineDescriptor Processors(Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> selector) =>
			Assign(selector, (a, v) => a.Processors = v?.Invoke(new ProcessorsDescriptor())?.Value);

		/// <inheritdoc />
		public PipelineDescriptor OnFailure(IEnumerable<IProcessor> processors) => Assign(processors.ToListOrNullIfEmpty(), (a, v) => a.OnFailure = v);

		/// <inheritdoc />
		public PipelineDescriptor OnFailure(Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> selector) =>
			Assign(selector, (a, v) => a.OnFailure = v?.Invoke(new ProcessorsDescriptor())?.Value);
	}
}
