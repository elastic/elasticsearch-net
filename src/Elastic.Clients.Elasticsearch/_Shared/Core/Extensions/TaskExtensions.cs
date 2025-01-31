// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

// Adapted from https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/core/Azure.Core/src/Shared/TaskExtensions.cs
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Elastic.Clients.Elasticsearch;

internal static class TaskExtensions
{
	[Conditional("DEBUG")]
	private static void VerifyTaskCompleted(bool isCompleted)
	{
		if (!isCompleted)
		{
			if (Debugger.IsAttached)
			{
				Debugger.Break();
			}
			// Throw an InvalidOperationException instead of using
			// Debug.Assert because that brings down xUnit immediately
			throw new InvalidOperationException("Task is not completed");
		}
	}

	public static T EnsureCompleted<T>(this ValueTask<T> task)
	{
#if DEBUG
		VerifyTaskCompleted(task.IsCompleted);
#endif
#pragma warning disable VSTHRD002 // Avoid problematic synchronous waits
		return task.GetAwaiter().GetResult();
#pragma warning restore VSTHRD002 // Avoid problematic synchronous waits
	}
}
