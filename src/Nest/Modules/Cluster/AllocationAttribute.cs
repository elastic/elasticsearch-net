using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public interface IAllocationAttributes : IHasADictionary
	{
		IDictionary<string, IList<string>> Attributes { get; } 
	}

	public class AllocationAttributes : IsADictionary<string, IList<string>>, IAllocationAttributes
	{
		IDictionary<string, IList<string>> IAllocationAttributes.Attributes => this.BackingDictionary;

		public void Add(string attribute, params string[] values) => this.BackingDictionary.Add(attribute, values.ToList());
		public void Add(string attribute, IEnumerable<string> values) => this.BackingDictionary.Add(attribute, values.ToList());
	}

	public class AllocationAttributesDescriptor : IsADictionaryDescriptor<AllocationAttributesDescriptor, IAllocationAttributes, string, IList<string>>, IAllocationAttributes
	{
		IDictionary<string, IList<string>> IAllocationAttributes.Attributes => this.BackingDictionary;

		public void Add(string attribute, params string[] values) => this.BackingDictionary.Add(attribute, values.ToList());
		public void Add(string attribute, IEnumerable<string> values) => this.BackingDictionary.Add(attribute, values.ToList());
	}
}
