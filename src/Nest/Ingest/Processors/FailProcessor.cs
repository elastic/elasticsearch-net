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
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Raises an exception. This is useful for when you expect a pipeline to
	/// fail and want to relay a specific message to the requester.
	/// </summary>
	[InterfaceDataContract]
	public interface IFailProcessor : IProcessor
	{
		/// <summary>
		/// The error message thrown by the processor. Supports template snippets.
		/// </summary>
		[DataMember(Name ="message")]
		string Message { get; set; }
	}

	/// <inheritdoc cref="IFailProcessor" />
	public class FailProcessor : ProcessorBase, IFailProcessor
	{
		/// <inheritdoc />
		public string Message { get; set; }
		protected override string Name => "fail";
	}

	/// <inheritdoc cref="IFailProcessor" />
	public class FailProcessorDescriptor
		: ProcessorDescriptorBase<FailProcessorDescriptor, IFailProcessor>, IFailProcessor
	{
		protected override string Name => "fail";

		string IFailProcessor.Message { get; set; }

		/// <inheritdoc cref="IFailProcessor.Message" />
		public FailProcessorDescriptor Message(string message) => Assign(message, (a, v) => a.Message = v);
	}
}
