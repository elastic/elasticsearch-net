using System;
using System.Collections.Generic;
using System.Text;
using Elastic.Transport;

namespace Nest
{
	// Stubs until we generate these - Allows the code to compile so we can identify real errors.
	
	public class Metrics : IUrlParameter
	{
		public string GetString(ITransportConfiguration settings) => throw new NotImplementedException();
	}

	public class ExpandWildcards
	{
	}

	public class ScrollId
	{
	}

	public class DateString
	{
	}

	public class Refresh { }

	public class WaitForActiveShards { }

	public class Types :  IUrlParameter
	{
		public string GetString(ITransportConfiguration settings) => throw new NotImplementedException();
	}

	public class Alias : IUrlParameter
	{
		public string GetString(ITransportConfiguration settings) => throw new NotImplementedException();
	}

	public class CategoryId : IUrlParameter
	{
		public string GetString(ITransportConfiguration settings) => throw new NotImplementedException();
	}
}
