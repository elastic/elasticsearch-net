// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch.Core.Search;

public partial class SourceConfigParam :
	IUrlParameter
{
	public string GetString(ITransportConfiguration settings) =>
		Tag switch
		{
			0 => UrlFormatter.CreateString(Item1, settings)!,
			1 => UrlFormatter.CreateString(Item2, settings)!,
			_ => throw new InvalidOperationException()
		};
}
