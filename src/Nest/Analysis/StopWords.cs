using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public class StopWords : Union<string, IEnumerable<string>>
	{
		public StopWords(string item) : base(item) { }
		public StopWords(IEnumerable<string> item) : base(item) { }

		public static implicit operator StopWords(string first) => new StopWords(first);
		public static implicit operator StopWords(List<string> second) => new StopWords(second);
		public static implicit operator StopWords(string[] second) => new StopWords(second);
	}
}
