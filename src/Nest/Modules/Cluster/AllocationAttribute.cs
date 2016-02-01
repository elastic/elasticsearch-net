using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public interface IAllocationAttributes : IIsADictionary<string, IList<string>>
	{
		IDictionary<string, IList<string>> Attributes { get; } 
	}

	public class AllocationAttributes : IsADictionaryBase<string, IList<string>>, IAllocationAttributes
	{
		IDictionary<string, IList<string>> IAllocationAttributes.Attributes => this.BackingDictionary;

		public void Add(string attribute, params string[] values) => this.BackingDictionary.Add(attribute, values.ToList());
		public void Add(string attribute, IEnumerable<string> values) => this.BackingDictionary.Add(attribute, values.ToList());
	}

	public class AllocationAttributesDescriptor : IsADictionaryDescriptorBase<AllocationAttributesDescriptor, IAllocationAttributes, string, IList<string>>
	{
		public AllocationAttributesDescriptor() : base(new AllocationAttributes()) { }
	}
}
