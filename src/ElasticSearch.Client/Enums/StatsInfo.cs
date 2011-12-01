using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.Client
{
	[Flags]
	public enum StatsInfo
	{
		None = 0x0,
		Docs = 0x1,
		Store = 0x2,
		Indexing = 0x4,
		Get = 0x8,
		Search = 0x10,
		Merge = 0x20,
		Flush = 0x40,
		All = Docs | Store | Indexing | Get | Search | Merge | Flush
		 
	}
}
