// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	public enum GeoTilePrecision
	{
		/// <summary>
		/// Whole world
		/// </summary>
		Precision0 = 0,
		Precision1 = 1,

		/// <summary>
		/// Subcontinental area
		/// </summary>
		Precision2 = 2,

		/// <summary>
		/// Largest Country
		/// </summary>
		Precision3 = 3,
		Precision4 = 4,

		/// <summary>
		/// Large African Country
		/// </summary>
		Precision5 = 5,

		/// <summary>
		/// Large European Country
		/// </summary>
		Precision6 = 6,

		/// <summary>
		/// Small Country / US State
		/// </summary>
		Precision7 = 7,
		Precision8 = 8,

		/// <summary>
		/// Wide Area / Large Metropolitan Area
		/// </summary>
		Precision9 = 9,

		/// <summary>
		/// Metropolitan Area
		/// </summary>
		Precision10 = 10,

		/// <summary>
		/// City
		/// </summary>
		Precision11 = 11,

		/// <summary>
		/// City / Town / District
		/// </summary>
		Precision12 = 12,

		/// <summary>
		/// Village / Suburb
		/// </summary>
		Precision13 = 13,
		Precision14 = 14,

		/// <summary>
		/// Small Road
		/// </summary>
		Precision15 = 15,

		/// <summary>
		/// Street
		/// </summary>
		Precision16 = 16,

		/// <summary>
		/// Block / Park / Addresses
		/// </summary>
		Precision17 = 17,

		/// <summary>
		/// Some Buildings / Trees
		/// </summary>
		Precision18 = 18,

		/// <summary>
		/// Local highway / Crossing
		/// </summary>
		Precision19 = 19,
		Precision20 = 20,
		Precision21 = 21,
		Precision22 = 22,
		Precision23 = 23,
		Precision24 = 24,
		Precision25 = 25,
		Precision26 = 26,
		Precision27 = 27,
		Precision28 = 28,

		/// <summary>
		/// Produces cells that cover less than a 10cm by 10cm of land and so high-precision
		/// requests can be very costly in terms of RAM and result sizes.
		/// </summary>
		Precision29 = 29
	}
}
