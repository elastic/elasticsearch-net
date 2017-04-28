using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Nest
{
	public interface IXPackUsageResponse : IResponse
	{
	}

	public class XPackUsageResponse : ResponseBase, IXPackUsageResponse
	{
		public XPackUsage Graph { get; set; }
	}


	public class XPackUsage
	{
		public bool Available { get; internal set; }
		public bool Enabled { get; internal set; }
	}

}
