// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public interface IAllocationAttributes : IIsADictionary<string, IList<string>>
	{
		IDictionary<string, IList<string>> Attributes { get; }
	}

	public class AllocationAttributes : IsADictionaryBase<string, IList<string>>, IAllocationAttributes
	{
		IDictionary<string, IList<string>> IAllocationAttributes.Attributes => BackingDictionary;

		public void Add(string attribute, params string[] values) => BackingDictionary.Add(attribute, values.ToList());

		public void Add(string attribute, IEnumerable<string> values) => BackingDictionary.Add(attribute, values.ToList());
	}

	public class AllocationAttributesDescriptor
		: IsADictionaryDescriptorBase<AllocationAttributesDescriptor, IAllocationAttributes, string, IList<string>>
	{
		public AllocationAttributesDescriptor() : base(new AllocationAttributes()) { }
	}
}
