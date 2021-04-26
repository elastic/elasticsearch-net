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
	/// The destination for a transform.
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(TransformDestination))]
	public interface ITransformDestination
	{
		/// <summary>
		/// The destination index for the transform.
		/// </summary>
		[DataMember(Name = "index")]
		public IndexName Index { get; set; }

		/// <summary>
		/// The unique identifier for a pipeline.
		/// </summary>
		[DataMember(Name = "pipeline")]
		public string Pipeline { get; set; }
	}

	/// <inheritdoc />
	public class TransformDestination
		: ITransformDestination
	{
		/// <inheritdoc />
		public IndexName Index { get; set; }

		/// <inheritdoc />
		public string Pipeline { get; set; }
	}

	/// <inheritdoc cref="ITransformDestination"/>
	public class TransformDestinationDescriptor : DescriptorBase<TransformDestinationDescriptor, ITransformDestination>, ITransformDestination
	{
		IndexName ITransformDestination.Index { get; set; }
		string ITransformDestination.Pipeline { get; set; }

		/// <inheritdoc cref="ITransformDestination.Index"/>
		public TransformDestinationDescriptor Index(IndexName index) => Assign(index, (a, v) => a.Index = v);

		/// <inheritdoc cref="ITransformDestination.Index"/>
		public TransformDestinationDescriptor Index<T>() => Assign(typeof(T), (a, v) => a.Index = v);

		/// <inheritdoc cref="ITransformDestination.Pipeline"/>
		public TransformDestinationDescriptor Pipeline(string pipeline) => Assign(pipeline, (a, v) => a.Pipeline = v);
	}
}
