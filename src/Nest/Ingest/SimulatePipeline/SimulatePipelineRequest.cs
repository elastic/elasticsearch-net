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

namespace Nest
{
	[MapsApi("ingest.simulate.json")]
	public partial interface ISimulatePipelineRequest
	{
		[DataMember(Name ="docs")]
		IEnumerable<ISimulatePipelineDocument> Documents { get; set; }

		[DataMember(Name ="pipeline")]
		IPipeline Pipeline { get; set; }
	}

	public partial class SimulatePipelineRequest
	{
		public IEnumerable<ISimulatePipelineDocument> Documents { get; set; }
		public IPipeline Pipeline { get; set; }
	}

	public partial class SimulatePipelineDescriptor
	{
		IEnumerable<ISimulatePipelineDocument> ISimulatePipelineRequest.Documents { get; set; }
		IPipeline ISimulatePipelineRequest.Pipeline { get; set; }

		public SimulatePipelineDescriptor Pipeline(Func<PipelineDescriptor, IPipeline> pipeline) =>
			Assign(pipeline, (a, v) => a.Pipeline = v?.Invoke(new PipelineDescriptor()));

		public SimulatePipelineDescriptor Documents(IEnumerable<ISimulatePipelineDocument> documents) => Assign(documents, (a, v) => a.Documents = v);

		public SimulatePipelineDescriptor Documents(Func<SimulatePipelineDocumentsDescriptor, IPromise<IList<ISimulatePipelineDocument>>> selector) =>
			Assign(selector, (a, v) => a.Documents = v?.Invoke(new SimulatePipelineDocumentsDescriptor())?.Value);
	}
}
