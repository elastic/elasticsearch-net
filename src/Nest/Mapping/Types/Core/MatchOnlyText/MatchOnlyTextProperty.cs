// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Diagnostics;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A variant of text that trades scoring and efficiency of positional queries for space efficiency. 
	/// This field effectively stores data the same way as a text field that only indexes documents 
	/// (index_options: docs) and disables norms (norms: false). 
	/// <para />
	/// MatchOnlyText fields are not used for sorting and seldom used for aggregations.
	/// </summary>
	[InterfaceDataContract]
	public interface IMatchOnlyTextProperty : ICoreProperty
	{
	}

	/// <inheritdoc cref="IMatchOnlyTextProperty"/>
	[DebuggerDisplay("{DebugDisplay}")]
	public class MatchOnlyTextProperty : CorePropertyBase, IMatchOnlyTextProperty
	{
		public MatchOnlyTextProperty() : base(FieldType.MatchOnlyText) { }
	}

	/// <inheritdoc cref="IMatchOnlyTextProperty"/>
	public class MatchOnlyTextPropertyDescriptor<T>
		: CorePropertyDescriptorBase<MatchOnlyTextPropertyDescriptor<T>, IMatchOnlyTextProperty, T>, IMatchOnlyTextProperty
		where T : class
	{
		public MatchOnlyTextPropertyDescriptor() : base(FieldType.MatchOnlyText) { }
	}
}
