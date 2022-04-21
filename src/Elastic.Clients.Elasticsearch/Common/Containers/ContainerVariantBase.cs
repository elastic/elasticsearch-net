// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

public abstract class ContainerVariantBase
{
	[JsonIgnore]
	internal abstract string VariantName { get; }
}

public abstract class ContainerVariantBase<TVariantContainer> : ContainerVariantBase where TVariantContainer : IContainer
{
	internal TVariantContainer WrapInContainer() => (TVariantContainer)Activator.CreateInstance(typeof(TVariantContainer), this);
}
