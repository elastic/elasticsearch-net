// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;

namespace Nest
{
	/// <summary>
	/// An exception when handling <see cref="IGeoShape" /> in Well-Known Text format
	/// </summary>
	public class GeoWKTException : Exception
	{
		public GeoWKTException(string message)
			: base(message) { }

		public GeoWKTException(string message, int lineNumber, int position)
			: base($"{message} at line {lineNumber}, position {position}") { }
	}
}
