using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public class MinimumShouldMatch : Union<int, string>
	{
		public MinimumShouldMatch(int item) : base(item) { }

		public MinimumShouldMatch(string item) : base(item) { }

		public static implicit operator MinimumShouldMatch(string first) => new MinimumShouldMatch(first);
		public static implicit operator MinimumShouldMatch(int second) => new MinimumShouldMatch(second);
	}
}
