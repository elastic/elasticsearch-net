// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.CodeAnalysis;

namespace RequestConverter.Tests;

internal static class CompilationHarness
{
	public static List<MetadataReference> ReferenceAssemblies()
	{
		var tpa = (string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")!;
		return tpa
			.Split(Path.PathSeparator)
			.Where(p => p.Length > 0 && File.Exists(p))
			.Select(p => (MetadataReference)MetadataReference.CreateFromFile(p))
			.ToList();
	}
}
