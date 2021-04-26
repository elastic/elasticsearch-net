/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;

namespace Nest
{
	// TODO: Change to WKTException in 8.x
	/// <summary>
	/// An exception when handling shapes in Well-Known Text (WKT) format
	/// </summary>
	public class GeoWKTException : Exception
	{
		public GeoWKTException(string message)
			: base(message) { }

		public GeoWKTException(string message, int lineNumber, int position)
			: base($"{message} at line {lineNumber}, position {position}") { }
	}
}
