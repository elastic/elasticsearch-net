using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<IndexConstraints, Field, IIndexConstraint>))]
	public interface IIndexConstraints : IIsADictionary<Field, IIndexConstraint> { }

	public class IndexConstraints : IsADictionaryBase<Field, IIndexConstraint>, IIndexConstraints
	{
		public IndexConstraints() : base() { }
		public IndexConstraints(IDictionary<Field, IIndexConstraint> container) : base(container) { }
		public IndexConstraints(Dictionary<Field, IIndexConstraint> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		public void Add(Field field, IndexConstraint constraint) => BackingDictionary.Add(field, constraint);
	}

	public class IndexConstraintsDescriptor : IsADictionaryDescriptorBase<IndexConstraintsDescriptor, IIndexConstraints, Field, IIndexConstraint>
	{
		public IndexConstraintsDescriptor() : base(new IndexConstraints()) { }

		public IndexConstraintsDescriptor IndexConstraint(Field field, Func<IndexConstraintDescriptor, IIndexConstraint> selector) =>
			Assign(field, selector?.Invoke(new IndexConstraintDescriptor()));
	}
}
