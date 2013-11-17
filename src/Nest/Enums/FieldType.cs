using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	/// <summary>
	/// Define the type of field content.
	/// </summary>
	public enum FieldType
	{
		/// <summary>
		/// Default. Will be defined by the type of property return.
		/// </summary>
		none,
		/// <summary>
		/// Geo based points.
		/// </summary>
		geo_point,
		/// <summary>
		/// The attachment type allows to index different “attachment” type field (encoded as base64), for example, microsoft office formats, open document formats, ePub, HTML...
		/// </summary>
		attachment,
		/// <summary>
		/// An ip mapping type allows to store ipv4 addresses in a numeric form allowing to easily sort, and range query it (using ip values).
		/// </summary>
		ip,
		/// <summary>
		/// The binary type is a base64 representation of binary data that can be stored in the index.
		/// </summary>
		binary,
		/// <summary>
		/// Text based string type.
		/// </summary>
		string_type,
		/// <summary>
		/// Integer type.
		/// </summary>
		integer_type,
		/// <summary>
		/// Long type.
		/// </summary>
		long_type,
		/// <summary>
		/// Float type.
		/// </summary>
		float_type,
		/// <summary>
		/// Double type.
		/// </summary>
		double_type,
		/// <summary>
		/// Date type.
		/// </summary>
		date_type,
		/// <summary>
		/// Boolean type.
		/// </summary>
		boolean_type,
		/// <summary>
		/// Nested type.
		/// </summary>
		nested,
        /// <summary>
        /// Completion type.
        /// </summary>
        completion,
		/// <summary>
		/// object type, no need to set this manually if its not a value type this will be set.
		/// Only set this if you need to force a value type to be mapped to an elasticsearch object type.
		/// </summary>
		@object
	}
}
