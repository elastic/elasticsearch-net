// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[StringEnum]
	public enum KeepTypesMode
	{
		[EnumMember(Value = "include")]
		Include,

		[EnumMember(Value = "exclude")]
		Exclude
	}

	/// <summary>
	/// A token filter of type keep that only keeps tokens with text contained in a predefined set of words.
	/// </summary>
	public interface IKeepTypesTokenFilter : ITokenFilter
	{
		/// <summary> Whether to include or exclude the types provided on <see cref="Types" /> </summary>
		[DataMember(Name = "mode")]
		KeepTypesMode? Mode { get; set; }

		/// <summary> A list of types to keep. </summary>
		[DataMember(Name = "types")]
		IEnumerable<string> Types { get; set; }
	}

	/// <inheritdoc cref="IKeepTypesTokenFilter" />
	public class KeepTypesTokenFilter : TokenFilterBase, IKeepTypesTokenFilter
	{
		public KeepTypesTokenFilter() : base("keep_types") { }

		/// <inheritdoc cref="IKeepTypesTokenFilter.Mode" />
		public KeepTypesMode? Mode { get; set; }

		/// <inheritdoc cref="IKeepTypesTokenFilter.Types" />
		public IEnumerable<string> Types { get; set; }
	}

	/// <inheritdoc cref="IKeepTypesTokenFilter" />
	public class KeepTypesTokenFilterDescriptor
		: TokenFilterDescriptorBase<KeepTypesTokenFilterDescriptor, IKeepTypesTokenFilter>, IKeepTypesTokenFilter
	{
		protected override string Type => "keep_types";
		KeepTypesMode? IKeepTypesTokenFilter.Mode { get; set; }

		IEnumerable<string> IKeepTypesTokenFilter.Types { get; set; }

		/// <inheritdoc cref="IKeepTypesTokenFilter.Types" />
		public KeepTypesTokenFilterDescriptor Types(IEnumerable<string> types) => Assign(types, (a, v) => a.Types = v);

		/// <inheritdoc cref="IKeepTypesTokenFilter.Types" />
		public KeepTypesTokenFilterDescriptor Types(params string[] types) => Assign(types, (a, v) => a.Types = v);

		/// <inheritdoc cref="IKeepTypesTokenFilter.Mode" />
		public KeepTypesTokenFilterDescriptor Mode(KeepTypesMode? mode) => Assign(mode, (a, v) => a.Mode = v);
	}
}
