// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Elastic.Transport
{
	/// <summary>
	/// A hint to <see cref="ITransportSerializer"/> how to format the json.
	/// Implementation of <see cref="ITransportSerializer"/> might choose to ignore this hint though.
	/// </summary>
	public enum SerializationFormatting
	{
		None,
		Indented
	}
}
