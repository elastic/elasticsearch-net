using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.Client
{
	[Flags]
	public enum ClearCacheOptions
	{
		Id = 0x1,
		Filter = 0x2,
		FieldData = 0x4,
		Bloom = 0x8,
		All = Id | Filter | FieldData | Bloom
	}
}
