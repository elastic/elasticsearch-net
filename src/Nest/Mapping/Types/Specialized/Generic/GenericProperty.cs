// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A generic property to map properties that may be of different types.
	/// Not all methods are valid for all types.
	/// </summary>
	[InterfaceDataContract]
	public interface IGenericProperty : IDocValuesProperty
	{
		[DataMember(Name ="analyzer")]
		string Analyzer { get; set; }

		[DataMember(Name ="boost")]
		double? Boost { get; set; }

		[DataMember(Name ="fielddata")]
		IStringFielddata Fielddata { get; set; }

		[DataMember(Name ="ignore_above")]
		int? IgnoreAbove { get; set; }

		[DataMember(Name ="index")]
		bool? Index { get; set; }

		[DataMember(Name ="index_options")]
		IndexOptions? IndexOptions { get; set; }

		[DataMember(Name ="norms")]
		bool? Norms { get; set; }

		[DataMember(Name ="null_value")]
		string NullValue { get; set; }

		[DataMember(Name ="position_increment_gap")]
		int? PositionIncrementGap { get; set; }

		[DataMember(Name ="search_analyzer")]
		string SearchAnalyzer { get; set; }

		[DataMember(Name ="term_vector")]
		TermVectorOption? TermVector { get; set; }
	}

	/// <inheritdoc cref="IGenericProperty"/>
	[DebuggerDisplay("{DebugDisplay}")]
	public class GenericProperty : DocValuesPropertyBase, IGenericProperty
	{
		public GenericProperty() : base(FieldType.Object) => TypeOverride = null;

		public string Analyzer { get; set; }
		public double? Boost { get; set; }
		public IStringFielddata Fielddata { get; set; }
		public int? IgnoreAbove { get; set; }
		public bool? Index { get; set; }
		public IndexOptions? IndexOptions { get; set; }
		public bool? Norms { get; set; }
		public string NullValue { get; set; }
		public int? PositionIncrementGap { get; set; }
		public string SearchAnalyzer { get; set; }

		public TermVectorOption? TermVector { get; set; }

		public string Type
		{
			get => TypeOverride;
			set => TypeOverride = value;
		}
	}

	/// <inheritdoc cref="IGenericProperty"/>
	[DebuggerDisplay("{DebugDisplay}")]
	public class GenericPropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<GenericPropertyDescriptor<T>, IGenericProperty, T>, IGenericProperty
		where T : class
	{
		public GenericPropertyDescriptor() : base(FieldType.Object) => TypeOverride = null;

		string IGenericProperty.Analyzer { get; set; }
		double? IGenericProperty.Boost { get; set; }
		IStringFielddata IGenericProperty.Fielddata { get; set; }
		int? IGenericProperty.IgnoreAbove { get; set; }
		bool? IGenericProperty.Index { get; set; }
		IndexOptions? IGenericProperty.IndexOptions { get; set; }
		bool? IGenericProperty.Norms { get; set; }
		string IGenericProperty.NullValue { get; set; }
		int? IGenericProperty.PositionIncrementGap { get; set; }
		string IGenericProperty.SearchAnalyzer { get; set; }
		TermVectorOption? IGenericProperty.TermVector { get; set; }

		public GenericPropertyDescriptor<T> Type(string type) => Assign(type, (a, v) => TypeOverride = v);

		public GenericPropertyDescriptor<T> Index(bool? index = true) => Assign(index, (a, v) => a.Index = v);

		public GenericPropertyDescriptor<T> Boost(double? boost) => Assign(boost, (a, v) => a.Boost = v);

		public GenericPropertyDescriptor<T> NullValue(string nullValue) => Assign(nullValue, (a, v) => a.NullValue = v);

		public GenericPropertyDescriptor<T> TermVector(TermVectorOption? termVector) => Assign(termVector, (a, v) => a.TermVector = v);

		public GenericPropertyDescriptor<T> IndexOptions(IndexOptions? indexOptions) => Assign(indexOptions, (a, v) => a.IndexOptions = v);

		public GenericPropertyDescriptor<T> Analyzer(string analyzer) => Assign(analyzer, (a, v) => a.Analyzer = v);

		public GenericPropertyDescriptor<T> SearchAnalyzer(string searchAnalyzer) => Assign(searchAnalyzer, (a, v) => a.SearchAnalyzer = v);

		public GenericPropertyDescriptor<T> Norms(bool? enabled = true) => Assign(enabled, (a, v) => a.Norms = v);

		public GenericPropertyDescriptor<T> IgnoreAbove(int? ignoreAbove) => Assign(ignoreAbove, (a, v) => a.IgnoreAbove = v);

		public GenericPropertyDescriptor<T> PositionIncrementGap(int? positionIncrementGap) =>
			Assign(positionIncrementGap, (a, v) => a.PositionIncrementGap = v);

		public GenericPropertyDescriptor<T> Fielddata(Func<StringFielddataDescriptor, IStringFielddata> selector) =>
			Assign(selector, (a, v) => a.Fielddata = v?.Invoke(new StringFielddataDescriptor()));
	}
}
