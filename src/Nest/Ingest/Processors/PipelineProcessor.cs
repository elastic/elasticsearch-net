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

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary> Executes another pipeline.</summary>
	[InterfaceDataContract]
	public interface IPipelineProcessor : IProcessor
	{
		/// <summary>The name of the pipeline to execute. </summary>
		[DataMember(Name = "name")]
		string ProcessorName { get; set; }
	}

	/// <inheritdoc cref="IPipelineProcessor" />
	public class PipelineProcessor : ProcessorBase, IPipelineProcessor
	{
		/// <inheritdoc />
		[DataMember(Name = "name")]
		public string ProcessorName { get; set; }

		internal const string ProcessorTypeName = "pipeline";
		protected override string Name => ProcessorTypeName;
	}

	/// <inheritdoc cref="IPipelineProcessor" />
	public class PipelineProcessorDescriptor
		: ProcessorDescriptorBase<PipelineProcessorDescriptor, IPipelineProcessor>, IPipelineProcessor
	{
		protected override string Name => PipelineProcessor.ProcessorTypeName;
		string IPipelineProcessor.ProcessorName { get; set; }

		/// <inheritdoc cref="IPipelineProcessor.ProcessorName"/>
		public PipelineProcessorDescriptor ProcessorName(string processorName) => Assign(processorName, (a, v) => a.ProcessorName = v);
	}
}
