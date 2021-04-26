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

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(VerbatimDictionaryKeysFormatter<PerFieldAnalyzer, IPerFieldAnalyzer, Field, string>))]
	public interface IPerFieldAnalyzer : IIsADictionary<Field, string> { }

	public class PerFieldAnalyzer : IsADictionaryBase<Field, string>, IPerFieldAnalyzer
	{
		public PerFieldAnalyzer() { }

		public PerFieldAnalyzer(IDictionary<Field, string> container) : base(container) { }

		public PerFieldAnalyzer(Dictionary<Field, string> container) : base(container) { }

		public void Add(Field field, string analyzer) => BackingDictionary.Add(field, analyzer);
	}

	public class PerFieldAnalyzer<T> : PerFieldAnalyzer where T : class
	{
		public void Add<TValue>(Expression<Func<T, TValue>> field, string analyzer) => BackingDictionary.Add(field, analyzer);
	}

	public class PerFieldAnalyzerDescriptor<T> : IsADictionaryDescriptorBase<PerFieldAnalyzerDescriptor<T>, IPerFieldAnalyzer, Field, string>
		where T : class
	{
		public PerFieldAnalyzerDescriptor() : base(new PerFieldAnalyzer()) { }

		public PerFieldAnalyzerDescriptor<T> Field(Field field, string analyzer) => Assign(field, analyzer);

		public PerFieldAnalyzerDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field, string analyzer) => Assign(field, analyzer);
	}
}
