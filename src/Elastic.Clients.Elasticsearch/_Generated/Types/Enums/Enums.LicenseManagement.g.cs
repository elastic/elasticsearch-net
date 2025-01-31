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

namespace Elastic.Clients.Elasticsearch.LicenseManagement;

[JsonConverter(typeof(LicenseStatusConverter))]
public enum LicenseStatus
{
	[EnumMember(Value = "valid")]
	Valid,
	[EnumMember(Value = "invalid")]
	Invalid,
	[EnumMember(Value = "expired")]
	Expired,
	[EnumMember(Value = "active")]
	Active
}

internal sealed partial class LicenseStatusConverter : System.Text.Json.Serialization.JsonConverter<LicenseStatus>
{
	private static readonly System.Text.Json.JsonEncodedText MemberValid = System.Text.Json.JsonEncodedText.Encode("valid");
	private static readonly System.Text.Json.JsonEncodedText MemberInvalid = System.Text.Json.JsonEncodedText.Encode("invalid");
	private static readonly System.Text.Json.JsonEncodedText MemberExpired = System.Text.Json.JsonEncodedText.Encode("expired");
	private static readonly System.Text.Json.JsonEncodedText MemberActive = System.Text.Json.JsonEncodedText.Encode("active");

	public override LicenseStatus Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.String);
		if (reader.ValueTextEquals(MemberValid))
		{
			return LicenseStatus.Valid;
		}

		if (reader.ValueTextEquals(MemberInvalid))
		{
			return LicenseStatus.Invalid;
		}

		if (reader.ValueTextEquals(MemberExpired))
		{
			return LicenseStatus.Expired;
		}

		if (reader.ValueTextEquals(MemberActive))
		{
			return LicenseStatus.Active;
		}

		throw new System.Text.Json.JsonException($"Unknown value '{reader.GetString()}' for enum '{nameof(LicenseStatus)}'.");
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, LicenseStatus value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value)
		{
			case LicenseStatus.Valid:
				writer.WriteStringValue(MemberValid);
				break;
			case LicenseStatus.Invalid:
				writer.WriteStringValue(MemberInvalid);
				break;
			case LicenseStatus.Expired:
				writer.WriteStringValue(MemberExpired);
				break;
			case LicenseStatus.Active:
				writer.WriteStringValue(MemberActive);
				break;
			default:
				throw new System.Text.Json.JsonException($"Invalid value '{value}' for enum '{nameof(LicenseStatus)}'.");
		}
	}
}

[JsonConverter(typeof(LicenseTypeConverter))]
public enum LicenseType
{
	[EnumMember(Value = "trial")]
	Trial,
	[EnumMember(Value = "standard")]
	Standard,
	[EnumMember(Value = "silver")]
	Silver,
	[EnumMember(Value = "platinum")]
	Platinum,
	[EnumMember(Value = "missing")]
	Missing,
	[EnumMember(Value = "gold")]
	Gold,
	[EnumMember(Value = "enterprise")]
	Enterprise,
	[EnumMember(Value = "dev")]
	Dev,
	[EnumMember(Value = "basic")]
	Basic
}

internal sealed partial class LicenseTypeConverter : System.Text.Json.Serialization.JsonConverter<LicenseType>
{
	private static readonly System.Text.Json.JsonEncodedText MemberTrial = System.Text.Json.JsonEncodedText.Encode("trial");
	private static readonly System.Text.Json.JsonEncodedText MemberStandard = System.Text.Json.JsonEncodedText.Encode("standard");
	private static readonly System.Text.Json.JsonEncodedText MemberSilver = System.Text.Json.JsonEncodedText.Encode("silver");
	private static readonly System.Text.Json.JsonEncodedText MemberPlatinum = System.Text.Json.JsonEncodedText.Encode("platinum");
	private static readonly System.Text.Json.JsonEncodedText MemberMissing = System.Text.Json.JsonEncodedText.Encode("missing");
	private static readonly System.Text.Json.JsonEncodedText MemberGold = System.Text.Json.JsonEncodedText.Encode("gold");
	private static readonly System.Text.Json.JsonEncodedText MemberEnterprise = System.Text.Json.JsonEncodedText.Encode("enterprise");
	private static readonly System.Text.Json.JsonEncodedText MemberDev = System.Text.Json.JsonEncodedText.Encode("dev");
	private static readonly System.Text.Json.JsonEncodedText MemberBasic = System.Text.Json.JsonEncodedText.Encode("basic");

	public override LicenseType Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.String);
		if (reader.ValueTextEquals(MemberTrial))
		{
			return LicenseType.Trial;
		}

		if (reader.ValueTextEquals(MemberStandard))
		{
			return LicenseType.Standard;
		}

		if (reader.ValueTextEquals(MemberSilver))
		{
			return LicenseType.Silver;
		}

		if (reader.ValueTextEquals(MemberPlatinum))
		{
			return LicenseType.Platinum;
		}

		if (reader.ValueTextEquals(MemberMissing))
		{
			return LicenseType.Missing;
		}

		if (reader.ValueTextEquals(MemberGold))
		{
			return LicenseType.Gold;
		}

		if (reader.ValueTextEquals(MemberEnterprise))
		{
			return LicenseType.Enterprise;
		}

		if (reader.ValueTextEquals(MemberDev))
		{
			return LicenseType.Dev;
		}

		if (reader.ValueTextEquals(MemberBasic))
		{
			return LicenseType.Basic;
		}

		throw new System.Text.Json.JsonException($"Unknown value '{reader.GetString()}' for enum '{nameof(LicenseType)}'.");
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, LicenseType value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value)
		{
			case LicenseType.Trial:
				writer.WriteStringValue(MemberTrial);
				break;
			case LicenseType.Standard:
				writer.WriteStringValue(MemberStandard);
				break;
			case LicenseType.Silver:
				writer.WriteStringValue(MemberSilver);
				break;
			case LicenseType.Platinum:
				writer.WriteStringValue(MemberPlatinum);
				break;
			case LicenseType.Missing:
				writer.WriteStringValue(MemberMissing);
				break;
			case LicenseType.Gold:
				writer.WriteStringValue(MemberGold);
				break;
			case LicenseType.Enterprise:
				writer.WriteStringValue(MemberEnterprise);
				break;
			case LicenseType.Dev:
				writer.WriteStringValue(MemberDev);
				break;
			case LicenseType.Basic:
				writer.WriteStringValue(MemberBasic);
				break;
			default:
				throw new System.Text.Json.JsonException($"Invalid value '{value}' for enum '{nameof(LicenseType)}'.");
		}
	}
}