using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Cluster.ClusterSettings.ClusterPutSettings
{
	public interface IClusterSettings : IHasADictionary
	{
		//IIndicesRecoverySettings Recovery { get; set; }
	}

	public class ClusterSettings : IClusterSettings
	{
		
		IDictionary IHasADictionary.Dictionary => this.AnySettings;

		public Dictionary<string, object> AnySettings { get; set; }

	}
}
