// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Highlighting for <see cref="IPhraseSuggester"/>
	/// </summary>
	[InterfaceDataContract]
	public interface IPhraseSuggestHighlight
	{
		/// <summary>
		/// The post tag
		/// </summary>
		[DataMember(Name = "post_tag")]
		string PostTag { get; set; }

		/// <summary>
		/// The pre tag
		/// </summary>
		[DataMember(Name = "pre_tag")]
		string PreTag { get; set; }
	}

	/// <inheritdoc />
	public class PhraseSuggestHighlight : IPhraseSuggestHighlight
	{
		/// <inheritdoc />
		public string PostTag { get; set; }
		/// <inheritdoc />
		public string PreTag { get; set; }
	}

	/// <inheritdoc cref="IPhraseSuggestHighlight" />
	public class PhraseSuggestHighlightDescriptor : DescriptorBase<PhraseSuggestHighlightDescriptor, IPhraseSuggestHighlight>, IPhraseSuggestHighlight
	{
		string IPhraseSuggestHighlight.PostTag { get; set; }
		string IPhraseSuggestHighlight.PreTag { get; set; }

		/// <inheritdoc cref="IPhraseSuggestHighlight.PreTag" />
		public PhraseSuggestHighlightDescriptor PreTag(string preTag) => Assign(preTag, (a, v) => a.PreTag = v);

		/// <inheritdoc cref="IPhraseSuggestHighlight.PostTag" />
		public PhraseSuggestHighlightDescriptor PostTag(string postTag) => Assign(postTag, (a, v) => a.PostTag = v);
	}
}
