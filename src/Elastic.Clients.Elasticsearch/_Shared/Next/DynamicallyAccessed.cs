// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal static class DynamicallyAccessed
{
	public static void AllConstructors(
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors |
									DynamicallyAccessedMemberTypes.NonPublicConstructors)]
		Type type)
	{
	}

	public static void PublicConstructors(
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
		Type type)
	{
	}

	public static void Interfaces(
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)]
		Type type)
	{
	}
}
