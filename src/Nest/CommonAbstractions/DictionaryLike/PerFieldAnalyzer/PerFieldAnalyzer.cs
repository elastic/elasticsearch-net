using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<PerFieldAnalyzer, Field, string>))]
	public interface IPerFieldAnalyzer : IIsADictionary<Field, string> { }

	public class PerFieldAnalyzer : IsADictionaryBase<Field, string>, IPerFieldAnalyzer
	{
		public PerFieldAnalyzer() : base() { }
		public PerFieldAnalyzer(IDictionary<Field, string> container) : base(container) { }
		public PerFieldAnalyzer(Dictionary<Field, string> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		public void Add(Field field, string analyzer) => BackingDictionary.Add(field, analyzer);
	}

	public class PerFieldAnalyzer<T> : PerFieldAnalyzer where T : class
	{
		public void Add(Expression<Func<T, object>>  field, string analyzer) => BackingDictionary.Add(field, analyzer);
	}

	public class PerFieldAnalyzerDescriptor<T> : IsADictionaryDescriptorBase<PerFieldAnalyzerDescriptor<T>, IPerFieldAnalyzer, Field, string>
		where T : class
	{
		public PerFieldAnalyzerDescriptor() : base(new PerFieldAnalyzer()) { }

		public PerFieldAnalyzerDescriptor<T> Field(Field field, string analyzer) => Assign(field, analyzer);

		public PerFieldAnalyzerDescriptor<T> Field(Expression<Func<T, object>> field, string analyzer) => Assign(field, analyzer);
	}
}
