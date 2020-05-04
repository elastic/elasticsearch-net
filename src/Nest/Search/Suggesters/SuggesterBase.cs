// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// A suggester that suggests similar looking terms based on a provided text
	/// </summary>
	public interface ISuggester
	{
		/// <summary>
		/// The analyzer to analyse the suggest text with.
		/// Defaults to the search analyzer of the suggest field.
		/// </summary>
		[DataMember(Name ="analyzer")]
		string Analyzer { get; set; }

		/// <summary>
		/// The name of the field on which to run the query
		/// </summary>
		[DataMember(Name ="field")]
		Field Field { get; set; }

		/// <summary>
		/// The number of suggestions to return. Defaults to 5
		/// </summary>
		[DataMember(Name ="size")]
		int? Size { get; set; }
	}

	/// <inheritdoc />
	public abstract class SuggesterBase : ISuggester
	{
		/// <inheritdoc />
		public string Analyzer { get; set; }
		/// <inheritdoc />
		public Field Field { get; set; }
		/// <inheritdoc />
		public int? Size { get; set; }
	}

	/// <inheritdoc cref="ISuggester" />
	[DataContract]
	public abstract class SuggestDescriptorBase<TDescriptor, TInterface, T> : DescriptorBase<TDescriptor, TInterface>, ISuggester
		where TDescriptor : SuggestDescriptorBase<TDescriptor, TInterface, T>, TInterface, ISuggester
		where TInterface : class, ISuggester
	{
		string ISuggester.Analyzer { get; set; }
		Field ISuggester.Field { get; set; }

		int? ISuggester.Size { get; set; }

		/// <inheritdoc cref="ISuggester.Size" />
		public TDescriptor Size(int? size) => Assign(size, (a, v) => a.Size = v);

		/// <inheritdoc cref="ISuggester.Analyzer" />
		public TDescriptor Analyzer(string analyzer) => Assign(analyzer, (a, v) => a.Analyzer = v);

		/// <inheritdoc cref="ISuggester.Field" />
		public TDescriptor Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="ISuggester.Field" />
		public TDescriptor Field<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.Field = v);
	}
}
