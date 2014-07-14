using System;
using System.Collections.Generic;
using System.Linq;
using AutoPoco.Engine;

namespace Nest.Tests.MockData.DataSources
{
	public class IpSource : DatasourceBase<String>
    {
        private static readonly Random _random = new Random();

        private static readonly string[] _ips = new[]{
            "127.0.0.1",
            "10.0.0.1",
            "10.0.0.2",
            "192.168.0.1"
        };

		public override string Next(IGenerationSession session)
		{
			string ip = _ips[_random.Next(0, _ips.Length)];
			return ip;
		}
    }
}
