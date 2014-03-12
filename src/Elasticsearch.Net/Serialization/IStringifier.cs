using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net.Serialization
{
	public interface IStringifier
	{
		string Stringify(object o);
	}
}
