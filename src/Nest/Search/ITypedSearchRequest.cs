// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary> Signals the type to deserialize hits into </summary>
	[InterfaceDataContract]
	public interface ITypedSearchRequest
	{
		Type ClrType { get; }
	}
}
