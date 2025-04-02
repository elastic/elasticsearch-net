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

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch;

/// <summary>
/// <para>
/// A latitude/longitude as a 2 dimensional point. It can be represented in various ways:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// as a <c>{lat, long}</c> object
/// </para>
/// </item>
/// <item>
/// <para>
/// as a geo hash value
/// </para>
/// </item>
/// <item>
/// <para>
/// as a <c>[lon, lat]</c> array
/// </para>
/// </item>
/// <item>
/// <para>
/// as a string in <c>"&lt;lat>, &lt;lon>"</c> or WKT point formats
/// </para>
/// </item>
/// </list>
/// </summary>
public sealed partial class GeoLocation : Elastic.Clients.Elasticsearch.Core.IComplexUnion<Elastic.Clients.Elasticsearch.GeoLocation.Kind>
{
	public enum Kind
	{
		LatitudeLongitude,
		GeoHash,
		Coordinates,
		Text
	}

	private readonly Kind _kind;
	private readonly object _value;

	Kind Elastic.Clients.Elasticsearch.Core.IComplexUnion<Kind>.ValueKind => _kind;

	object Elastic.Clients.Elasticsearch.Core.IComplexUnion<Kind>.Value => _value;

	private GeoLocation(Kind kind, object value)
	{
		_kind = kind;
		_value = value;
	}

	internal GeoLocation(Elastic.Clients.Elasticsearch.LatLonGeoLocation variant) : this(Kind.LatitudeLongitude, variant)
	{
	}

	public static Elastic.Clients.Elasticsearch.GeoLocation LatitudeLongitude(Elastic.Clients.Elasticsearch.LatLonGeoLocation latitudeLongitude) => new(Kind.LatitudeLongitude, latitudeLongitude);

	public bool IsLatitudeLongitude => _kind is Kind.LatitudeLongitude;

	public bool TryGetLatitudeLongitude([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out Elastic.Clients.Elasticsearch.LatLonGeoLocation? latitudeLongitude)
	{
		latitudeLongitude = null;
		if (_kind == Kind.LatitudeLongitude)
		{
			latitudeLongitude = (Elastic.Clients.Elasticsearch.LatLonGeoLocation)_value;
			return true;
		}

		return false;
	}

	public static implicit operator Elastic.Clients.Elasticsearch.GeoLocation(Elastic.Clients.Elasticsearch.LatLonGeoLocation latitudeLongitude) => GeoLocation.LatitudeLongitude(latitudeLongitude);

	internal GeoLocation(Elastic.Clients.Elasticsearch.GeoHashLocation variant) : this(Kind.GeoHash, variant)
	{
	}

	public static Elastic.Clients.Elasticsearch.GeoLocation GeoHash(Elastic.Clients.Elasticsearch.GeoHashLocation geoHash) => new(Kind.GeoHash, geoHash);

	public bool IsGeoHash => _kind is Kind.GeoHash;

	public bool TryGetGeoHash([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out Elastic.Clients.Elasticsearch.GeoHashLocation? geoHash)
	{
		geoHash = null;
		if (_kind == Kind.GeoHash)
		{
			geoHash = (Elastic.Clients.Elasticsearch.GeoHashLocation)_value;
			return true;
		}

		return false;
	}

	public static implicit operator Elastic.Clients.Elasticsearch.GeoLocation(Elastic.Clients.Elasticsearch.GeoHashLocation geoHash) => GeoLocation.GeoHash(geoHash);

	internal GeoLocation(System.Collections.Generic.ICollection<double> variant) : this(Kind.Coordinates, variant)
	{
	}

	public static Elastic.Clients.Elasticsearch.GeoLocation Coordinates(System.Collections.Generic.ICollection<double> coordinates) => new(Kind.Coordinates, coordinates);

	public bool IsCoordinates => _kind is Kind.Coordinates;

	public bool TryGetCoordinates([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out System.Collections.Generic.ICollection<double>? coordinates)
	{
		coordinates = null;
		if (_kind == Kind.Coordinates)
		{
			coordinates = (System.Collections.Generic.ICollection<double>)_value;
			return true;
		}

		return false;
	}

	public static implicit operator Elastic.Clients.Elasticsearch.GeoLocation(double[] coordinates) => GeoLocation.Coordinates(coordinates);

	internal GeoLocation(string variant) : this(Kind.Text, variant)
	{
	}

	public static Elastic.Clients.Elasticsearch.GeoLocation Text(string text) => new(Kind.Text, text);

	public bool IsText => _kind is Kind.Text;

	public bool TryGetText([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out string? text)
	{
		text = null;
		if (_kind == Kind.Text)
		{
			text = (string)_value;
			return true;
		}

		return false;
	}

	public static implicit operator Elastic.Clients.Elasticsearch.GeoLocation(string text) => GeoLocation.Text(text);
}

public readonly partial struct GeoLocationBuilder
{
	public Elastic.Clients.Elasticsearch.GeoLocation LatitudeLongitude(Elastic.Clients.Elasticsearch.LatLonGeoLocation value)
	{
		return new Elastic.Clients.Elasticsearch.GeoLocation(value);
	}

	public Elastic.Clients.Elasticsearch.GeoLocation LatitudeLongitude(System.Action<Elastic.Clients.Elasticsearch.LatLonGeoLocationDescriptor> action)
	{
		return new Elastic.Clients.Elasticsearch.GeoLocation(Elastic.Clients.Elasticsearch.LatLonGeoLocationDescriptor.Build(action));
	}

	public Elastic.Clients.Elasticsearch.GeoLocation GeoHash(Elastic.Clients.Elasticsearch.GeoHashLocation value)
	{
		return new Elastic.Clients.Elasticsearch.GeoLocation(value);
	}

	public Elastic.Clients.Elasticsearch.GeoLocation GeoHash(System.Action<Elastic.Clients.Elasticsearch.GeoHashLocationDescriptor> action)
	{
		return new Elastic.Clients.Elasticsearch.GeoLocation(Elastic.Clients.Elasticsearch.GeoHashLocationDescriptor.Build(action));
	}

	public Elastic.Clients.Elasticsearch.GeoLocation Coordinates(System.Collections.Generic.ICollection<double> value)
	{
		return new Elastic.Clients.Elasticsearch.GeoLocation(value);
	}

	public Elastic.Clients.Elasticsearch.GeoLocation Coordinates()
	{
		return new Elastic.Clients.Elasticsearch.GeoLocation(Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfDouble.Build(null));
	}

	public Elastic.Clients.Elasticsearch.GeoLocation Coordinates(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfDouble>? action)
	{
		return new Elastic.Clients.Elasticsearch.GeoLocation(Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfDouble.Build(action));
	}

	public Elastic.Clients.Elasticsearch.GeoLocation Coordinates(params double[] values)
	{
		return new Elastic.Clients.Elasticsearch.GeoLocation([.. values]);
	}

	public Elastic.Clients.Elasticsearch.GeoLocation Text(string value)
	{
		return new Elastic.Clients.Elasticsearch.GeoLocation(value);
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.GeoLocation Build(System.Func<Elastic.Clients.Elasticsearch.GeoLocationBuilder, Elastic.Clients.Elasticsearch.GeoLocation> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.GeoLocationBuilder();
		return action.Invoke(builder);
	}
}