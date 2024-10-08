// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

#nullable restore

using Elastic.Clients.Elasticsearch.Core;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using System;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Xpack;

[JsonConverter(typeof(XPackCategoryConverter))]
public enum XPackCategory
{
	[EnumMember(Value = "license")]
	License,
	[EnumMember(Value = "features")]
	Features,
	[EnumMember(Value = "build")]
	Build
}

internal sealed class XPackCategoryConverter : JsonConverter<XPackCategory>
{
	public override XPackCategory Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "license":
				return XPackCategory.License;
			case "features":
				return XPackCategory.Features;
			case "build":
				return XPackCategory.Build;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, XPackCategory value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case XPackCategory.License:
				writer.WriteStringValue("license");
				return;
			case XPackCategory.Features:
				writer.WriteStringValue("features");
				return;
			case XPackCategory.Build:
				writer.WriteStringValue("build");
				return;
		}

		writer.WriteNullValue();
	}
}