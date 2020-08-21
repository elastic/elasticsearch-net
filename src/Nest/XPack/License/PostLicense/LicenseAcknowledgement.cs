// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;

namespace Nest
{
	/// <summary>
	/// If the license is valid but is older or has less capabilities this will list out the reasons why a resubmission with acknowledge=true is
	/// required
	/// </summary>
	public class LicenseAcknowledgement
	{
		public IReadOnlyCollection<string> License { get; set; }
		public string Message { get; set; }
	}
}
