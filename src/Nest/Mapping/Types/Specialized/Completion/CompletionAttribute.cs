// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;

namespace Nest
{
	public class CompletionAttribute : ElasticsearchDocValuesPropertyAttributeBase, ICompletionProperty
	{
		public CompletionAttribute() : base(FieldType.Completion) { }

		public string Analyzer
		{
			get => Self.Analyzer;
			set => Self.Analyzer = value;
		}

		public int MaxInputLength
		{
			get => Self.MaxInputLength.GetValueOrDefault();
			set => Self.MaxInputLength = value;
		}

		public bool PreservePositionIncrements
		{
			get => Self.PreservePositionIncrements.GetValueOrDefault();
			set => Self.PreservePositionIncrements = value;
		}

		public bool PreserveSeparators
		{
			get => Self.PreserveSeparators.GetValueOrDefault();
			set => Self.PreserveSeparators = value;
		}

		public string SearchAnalyzer
		{
			get => Self.SearchAnalyzer;
			set => Self.SearchAnalyzer = value;
		}

		string ICompletionProperty.Analyzer { get; set; }
		IList<ISuggestContext> ICompletionProperty.Contexts { get; set; }
		int? ICompletionProperty.MaxInputLength { get; set; }
		bool? ICompletionProperty.PreservePositionIncrements { get; set; }
		bool? ICompletionProperty.PreserveSeparators { get; set; }

		string ICompletionProperty.SearchAnalyzer { get; set; }
		private ICompletionProperty Self => this;
	}
}
