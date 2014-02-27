using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net
{
	public interface IStringifier
	{
		string Stringify(object o);
	}
}
