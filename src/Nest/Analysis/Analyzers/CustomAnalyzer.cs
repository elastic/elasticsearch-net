// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// An analyzer of type custom that allows to combine a Tokenizer with zero or more Token Filters,
	/// and zero or more Char Filters.
	/// <para>
	/// The custom analyzer accepts a logical/registered name of the tokenizer to use, and a list of
	/// logical/registered names of token filters.
	/// </para>
	/// </summary>
	public interface ICustomAnalyzer : IAnalyzer
	{
		/// <summary>
		/// The logical / registered name of the tokenizer to use.
		/// </summary>
		[DataMember(Name ="char_filter")]
		[JsonFormatter(typeof(SingleOrEnumerableFormatter<string>))]
		IEnumerable<string> CharFilter { get; set; }

		/// <summary>
		/// An optional list of logical / registered name of token filters.
		/// </summary>
		[DataMember(Name ="filter")]
		[JsonFormatter(typeof(SingleOrEnumerableFormatter<string>))]
		IEnumerable<string> Filter { get; set; }

		/// <summary>
		/// An optional number of positions to increment between each field value of a
		/// field using this analyzer.
		/// <para />
		/// Deprecated, use <see cref="PositionIncrementGap"/>
		/// </summary>
		[Obsolete("Deprecated, use PositionIncrementGap instead")]
		[DataMember(Name ="position_offset_gap")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? PositionOffsetGap { get; set; }

		/// <summary>
		/// When indexing an array of text values, Elasticsearch inserts a fake "gap" between the last term of one value
		/// and the first term of the next value to ensure that a phrase query doesn’t match two terms from different array elements.
		/// Defaults to 100.
		/// </summary>
		[DataMember(Name ="position_increment_gap")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? PositionIncrementGap { get; set; }

		/// <summary>
		/// An optional list of logical / registered name of char filters.
		/// </summary>
		[DataMember(Name ="tokenizer")]
		string Tokenizer { get; set; }
	}

	public class CustomAnalyzer : AnalyzerBase, ICustomAnalyzer
	{
		public CustomAnalyzer() : base("custom") { }

		/// <inheritdoc />
		public IEnumerable<string> CharFilter { get; set; }

		/// <inheritdoc />
		public IEnumerable<string> Filter { get; set; }

		/// <inheritdoc />
		[Obsolete("Deprecated, use PositionIncrementGap instead")]
		public int? PositionOffsetGap { get; set; }

		/// <inheritdoc />
		public int? PositionIncrementGap { get; set; }

		/// <inheritdoc />
		public string Tokenizer { get; set; }
	}

	public class CustomAnalyzerDescriptor
		: AnalyzerDescriptorBase<CustomAnalyzerDescriptor, ICustomAnalyzer>, ICustomAnalyzer
	{
		protected override string Type => "custom";

		IEnumerable<string> ICustomAnalyzer.CharFilter { get; set; }
		IEnumerable<string> ICustomAnalyzer.Filter { get; set; }
		int? ICustomAnalyzer.PositionOffsetGap { get; set; }
		int? ICustomAnalyzer.PositionIncrementGap { get; set; }
		string ICustomAnalyzer.Tokenizer { get; set; }

		/// <inheritdoc />
		public CustomAnalyzerDescriptor Filters(params string[] filters) => Assign(filters, (a, v) => a.Filter = v);

		/// <inheritdoc />
		public CustomAnalyzerDescriptor Filters(IEnumerable<string> filters) => Assign(filters, (a, v) => a.Filter = v);

		/// <inheritdoc />
		public CustomAnalyzerDescriptor CharFilters(params string[] charFilters) => Assign(charFilters, (a, v) => a.CharFilter = v);

		/// <inheritdoc />
		public CustomAnalyzerDescriptor CharFilters(IEnumerable<string> charFilters) => Assign(charFilters, (a, v) => a.CharFilter = v);

		/// <inheritdoc />
		public CustomAnalyzerDescriptor Tokenizer(string tokenizer) => Assign(tokenizer, (a, v) => a.Tokenizer = v);

		/// <inheritdoc />
		[Obsolete("Deprecated, use PositionIncrementGap instead")]
		public CustomAnalyzerDescriptor PositionOffsetGap(int? positionOffsetGap) =>
			Assign(positionOffsetGap, (a, v) => a.PositionOffsetGap = v);

		/// <inheritdoc />
		public CustomAnalyzerDescriptor PositionIncrementGap(int? positionIncrementGap) =>
			Assign(positionIncrementGap, (a, v) => a.PositionIncrementGap = v);
	}
}
