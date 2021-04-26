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

using System.Collections.Generic;

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
