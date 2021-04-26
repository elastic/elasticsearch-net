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

namespace Nest
{
	[MapsApi("ingest.put_pipeline.json")]
	public partial interface IPutPipelineRequest : IPipeline { }

	public partial class PutPipelineRequest
	{
		public string Description { get; set; }
		public IEnumerable<IProcessor> OnFailure { get; set; }
		public IEnumerable<IProcessor> Processors { get; set; }
	}

	public partial class PutPipelineDescriptor
	{
		string IPipeline.Description { get; set; }
		IEnumerable<IProcessor> IPipeline.OnFailure { get; set; }
		IEnumerable<IProcessor> IPipeline.Processors { get; set; }

		/// <inheritdoc />
		public PutPipelineDescriptor Description(string description) => Assign(description, (a, v) => a.Description = v);

		/// <inheritdoc />
		public PutPipelineDescriptor Processors(IEnumerable<IProcessor> processors) => Assign(processors.ToListOrNullIfEmpty(), (a, v) => a.Processors = v);

		/// <inheritdoc />
		public PutPipelineDescriptor Processors(Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> selector) =>
			Assign(selector, (a, v) => a.Processors = v?.Invoke(new ProcessorsDescriptor())?.Value);

		/// <inheritdoc />
		public PutPipelineDescriptor OnFailure(IEnumerable<IProcessor> processors) => Assign(processors.ToListOrNullIfEmpty(), (a, v) => a.OnFailure = v);

		/// <inheritdoc />
		public PutPipelineDescriptor OnFailure(Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> selector) =>
			Assign(selector, (a, v) => a.OnFailure = v?.Invoke(new ProcessorsDescriptor())?.Value);
	}
}
