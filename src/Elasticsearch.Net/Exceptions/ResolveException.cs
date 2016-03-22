using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public class ResolveException : Exception
	{
		public ResolveException(string message) : base(message) { }
	}
}
