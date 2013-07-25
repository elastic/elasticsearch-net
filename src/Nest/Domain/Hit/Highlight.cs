using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Nest
{
	public class Highlight
	{
		public string DocumentId { get; internal set; }
		public string Field { get; internal set; }
		public IEnumerable<string> Highlights { get; set; }
	}
}
