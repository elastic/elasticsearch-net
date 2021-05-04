// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nest.Utf8Json;

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
