// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using System.Runtime.CompilerServices;
using VerifyTests;

namespace Tests;

public static class TestConfig
{
	[ModuleInitializer]
	public static void Init() =>
		VerifierSettings.DerivePathInfo(
			(sourceFile, projectDirectory, type, method) =>
			{
				return new(
					directory: Path.Combine(projectDirectory, "Snapshots"),
					typeName: type.Name,
					methodName: method.Name);
			});
}
