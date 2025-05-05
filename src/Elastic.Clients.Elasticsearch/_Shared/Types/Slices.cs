// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;

using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

public partial class Slices :
	IUrlParameter
{
	public string GetString(ITransportConfiguration settings) =>
		Tag switch
		{
			0 => Item1.ToString(CultureInfo.InvariantCulture),
			1 => UrlFormatter.CreateString(Item2, settings)!,
			_ => throw new InvalidOperationException()
		};
}
