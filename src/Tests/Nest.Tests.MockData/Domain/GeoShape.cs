using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest.Tests.MockData.Domain
{
	/// <summary>
	/// A really naive geojson shape, checkout GeoJson.net
	/// https://github.com/jbattermann/GeoJSON.Net
	/// For a better implementation, although the project seems a bit dead having looked
	/// at the issue list
	/// </summary>
	public class GeoShape
	{
		public string Type { get; set; }
		public List<double> Coordinates { get; set; }
	}
}
