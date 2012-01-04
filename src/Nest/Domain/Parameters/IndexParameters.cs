using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class IndexParameters : BaseParameters
	{
		public VersionType VersionType { get; set; }
		/// <summary>
		/// string because you can pass 5m, or 1h to ES
		/// </summary>
		public string Timeout { get; set; }
	}
}
