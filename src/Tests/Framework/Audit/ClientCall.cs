using Elasticsearch.Net.Connection;
using FluentAssertions;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.MockData;

namespace Tests.Framework
{
	public static class ClientCall
	{
		public static ClientCallAssertations OnCluster(VirtualizedCluster cluster) =>
			 new ClientCallAssertations(cluster);
	}
}