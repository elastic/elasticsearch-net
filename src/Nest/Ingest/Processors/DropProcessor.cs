// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Drops the document without raising any errors. This is useful to prevent the document from getting indexed based on some condition.
	/// </summary>
	[InterfaceDataContract]
	public interface IDropProcessor : IProcessor { }

	/// <inheritdoc cref="IDropProcessor"/>
	public class DropProcessor : ProcessorBase, IDropProcessor
	{
		protected override string Name => "drop";
	}

	/// <inheritdoc cref="IDropProcessor"/>
	public class DropProcessorDescriptor : ProcessorDescriptorBase<DropProcessorDescriptor, IDropProcessor>, IDropProcessor
	{
		protected override string Name => "drop";
	}
}
