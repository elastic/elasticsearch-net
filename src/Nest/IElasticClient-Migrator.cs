using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Nest
{
	public static class IElasticClientMigrator
	{
		public static IUnregisterPercolateResponse UnregisterPercolater<T>(
			this IElasticClient client,
			string name)
			where T : class
		{
			return client.UnregisterPercolator(u => u.Index<T>().Name(name));
		}

		public static IIndicesResponse MapFromAttributes<T>(
			this IElasticClient client
			)
			where T : class
		{
			return client.MapFluent<T>(m => m.MapFromAttributes());
		}

	}
}
