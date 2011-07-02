using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.Client
{
	public enum Replication
	{
		Sync, //default in ES
		Async
	}
}
