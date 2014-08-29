using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public interface IClusterRerouteResponse : IResponse
	{
	}
	public class ClusterRerouteResponse : BaseResponse, IClusterRerouteResponse
	{
	}
}
