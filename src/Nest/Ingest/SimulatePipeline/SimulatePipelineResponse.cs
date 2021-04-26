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
	public class SimulatePipelineResponse : ResponseBase
	{
		[DataMember(Name ="docs")]
		public IReadOnlyCollection<PipelineSimulation> Documents { get; internal set; } = EmptyReadOnly<PipelineSimulation>.Collection;
	}

	[DataContract]
	public class PipelineSimulation
	{
		[DataMember(Name ="doc")]
		public DocumentSimulation Document { get; internal set; }

		[DataMember(Name ="processor_results")]
		public IReadOnlyCollection<PipelineSimulation> ProcessorResults { get; internal set; } = EmptyReadOnly<PipelineSimulation>.Collection;

		[DataMember(Name ="tag")]
		public string Tag { get; internal set; }
	}

	[DataContract]
	public class DocumentSimulation
	{
		[DataMember(Name ="_id")]
		public string Id { get; internal set; }

		[DataMember(Name ="_index")]
		public string Index { get; internal set; }

		[DataMember(Name ="_ingest")]
		public Ingest Ingest { get; internal set; }

		[DataMember(Name ="_parent")]
		public string Parent { get; internal set; }

		[DataMember(Name ="_routing")]
		public string Routing { get; internal set; }

		[DataMember(Name ="_source")]
		public ILazyDocument Source { get; internal set; }
	}

	[DataContract]
	public class Ingest
	{
		[DataMember(Name ="timestamp")]
		public DateTime Timestamp { get; internal set; }
	}
}
