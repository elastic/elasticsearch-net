// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

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
