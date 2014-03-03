using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;

namespace Nest
{
	public enum GeoHashPrecision
	{
		/// <summary>
		/// 5,009.4km x 4,992.6km
		/// </summary>
		precision_1 = 1,
		
		/// <summary>
		/// 1,252.3km x 624.1km
		/// </summary>
		precision_2 = 2,
	
		/// <summary>
		/// 156.5km x 156km
		/// </summary>
		precision_3 = 3,
		
		/// <summary>
		/// 39.1km x 19.5km
		/// </summary>
		precision_4 = 4,
		
		/// <summary>
		/// 4.9km x 4.9km
		/// </summary>
		precision_5 = 5,
		
		/// <summary>
		/// 1.2km x 609.4m
		/// </summary>
		precision_6 = 6,
		
		/// <summary>
		/// 152.9m x 152.4m
		/// </summary>
		precision_7 = 7,
		
		/// <summary>
		// 38.2m x 19m
		/// </summary>
		precision_8 = 8,
		
		/// <summary>
		/// 4.8m x 4.8m
		/// </summary>
		precision_9 = 9,
		
		/// <summary>
		// 1.2m x 59.5cm
		/// </summary>
		precision_10 = 10,
		
		/// <summary>
		/// 14.9cm x 14.9cm
		/// </summary>
		precision_11 = 11,
		
		/// <summary>
		// 3.7cm x 1.9cm
		/// </summary>
		precision_12 = 12
}
}
