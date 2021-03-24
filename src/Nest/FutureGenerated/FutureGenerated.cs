using System;
using Elastic.Transport;

namespace Nest
{
	// Stubs until we generate these - Allows the code to compile so we can identify real errors.

	public class Index { }

	public class RollupIndex { }

	public class JobId : IUrlParameter
	{
		public string GetString(ITransportConfiguration settings) => throw new NotImplementedException();
	}

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

	public class IndexAlias : IUrlParameter
	{
		public string GetString(ITransportConfiguration settings) => throw new NotImplementedException();
	}


	public class DateString
	{
	}

	public class NodeId { }

	//public class Refresh { }

	public class WaitForActiveShards { }

	public class Types : IUrlParameter
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

	//// TODO: Recently removed from spec during validation
	//public enum TimeUnit
	//{
	//	a
	//}
}
