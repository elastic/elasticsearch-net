// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A field of type token_count is really an integer field which accepts string values,
	/// analyzes them, then indexes the number of tokens in the string.
	/// </summary>
	[InterfaceDataContract]
	public interface ITokenCountProperty : IDocValuesProperty
	{
		[DataMember(Name ="analyzer")]
		string Analyzer { get; set; }

		[Obsolete("The server always treated this as a noop and has been removed in 7.10")]
		[DataMember(Name ="boost")]
		double? Boost { get; set; }

		[DataMember(Name ="enable_position_increments")]
		bool? EnablePositionIncrements { get; set; }

		[DataMember(Name ="index")]
		bool? Index { get; set; }

		[DataMember(Name ="null_value")]
		double? NullValue { get; set; }
	}

	/// <inheritdoc cref="ITokenCountProperty"/>
	[DebuggerDisplay("{DebugDisplay}")]
	public class TokenCountProperty : DocValuesPropertyBase, ITokenCountProperty
	{
		public TokenCountProperty() : base(FieldType.TokenCount) { }
		public string Analyzer { get; set; }
		public double? Boost { get; set; }
		public bool? EnablePositionIncrements { get; set; }
		public bool? Index { get; set; }
		public double? NullValue { get; set; }
	}

	/// <inheritdoc cref="ITokenCountProperty"/>
	[DebuggerDisplay("{DebugDisplay}")]
	public class TokenCountPropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<TokenCountPropertyDescriptor<T>, ITokenCountProperty, T>, ITokenCountProperty
		where T : class
	{
		public TokenCountPropertyDescriptor() : base(FieldType.TokenCount) { }
		string ITokenCountProperty.Analyzer { get; set; }
		double? ITokenCountProperty.Boost { get; set; }
		bool? ITokenCountProperty.EnablePositionIncrements { get; set; }
		bool? ITokenCountProperty.Index { get; set; }
		double? ITokenCountProperty.NullValue { get; set; }

		public TokenCountPropertyDescriptor<T> Analyzer(string analyzer) => Assign(analyzer, (a, v) => a.Analyzer = v);

		[Obsolete("The server always treated this as a noop and has been removed in 7.10")]
		public TokenCountPropertyDescriptor<T> Boost(double? boost) => Assign(boost, (a, v) => a.Boost = v);

		public TokenCountPropertyDescriptor<T> EnablePositionIncrements(bool? enablePositionIncrements = true) =>
			Assign(enablePositionIncrements, (a, v) => a.EnablePositionIncrements = v);

		public TokenCountPropertyDescriptor<T> Index(bool? index = true) => Assign(index, (a, v) => a.Index = v);
		public TokenCountPropertyDescriptor<T> NullValue(double? nullValue) => Assign(nullValue, (a, v) => a.NullValue = v);
	}
}
