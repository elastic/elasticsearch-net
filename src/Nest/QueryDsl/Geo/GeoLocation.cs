using System;
using System.Globalization;
using Newtonsoft.Json;

/*
 * Taken from SolrNet https://github.com/mausch/SolrNet/blob/master/SolrNet/Location.cs
 */

namespace Nest
{
	/// <summary>
	/// Represents a Latitude/Longitude as a 2 dimensional point that gets serialized as { lat, lon }
	/// </summary>
	public class GeoLocation : IEquatable<GeoLocation>, IFormattable
	{
		/// <summary>
		/// Latitude
		/// </summary>
		[JsonProperty("lat")]
		public double Latitude { get; }

		/// <summary>
		/// Longitude
		/// </summary>
		[JsonProperty("lon")]
		public double Longitude { get; }

		/// <summary>
		/// Represents a Latitude/Longitude as a 2 dimensional point.
		/// </summary>
		/// <param name="latitude">Value between -90 and 90</param>
		/// <param name="longitude">Value between -180 and 180</param>
		/// <exception cref="ArgumentOutOfRangeException">If <paramref name="latitude"/> or <paramref name="longitude"/> are invalid</exception>
		public GeoLocation(double latitude, double longitude)
		{
			if (!IsValidLatitude(latitude))
				throw new ArgumentOutOfRangeException(string.Format(CultureInfo.InvariantCulture,
					"Invalid latitude '{0}'. Valid values are between -90 and 90", latitude));
			if (!IsValidLongitude(longitude))
				throw new ArgumentOutOfRangeException(string.Format(CultureInfo.InvariantCulture,
					"Invalid longitude '{0}'. Valid values are between -180 and 180", longitude));
			Latitude = latitude;
			Longitude = longitude;
		}

		/// <summary>
		/// True if <paramref name="latitude"/> is a valid latitude. Otherwise false.
		/// </summary>
		/// <param name="latitude"></param>
		/// <returns></returns>
		public static bool IsValidLatitude(double latitude) => latitude >= -90 && latitude <= 90;

		/// <summary>
		/// True if <paramref name="longitude"/> is a valid longitude. Otherwise false.
		/// </summary>
		/// <param name="longitude"></param>
		/// <returns></returns>
		public static bool IsValidLongitude(double longitude) => longitude >= -180 && longitude <= 180;

		/// <summary>
		/// Try to create a <see cref="GeoLocation"/>.
		/// Return <value>null</value> if either <paramref name="latitude"/> or <paramref name="longitude"/> are invalid.
		/// </summary>
		/// <param name="latitude">Value between -90 and 90</param>
		/// <param name="longitude">Value between -180 and 180</param>
		/// <returns></returns>
		public static GeoLocation TryCreate(double latitude, double longitude)
		{
			if (IsValidLatitude(latitude) && IsValidLongitude(longitude))
				return new GeoLocation(latitude, longitude);
			return null;
		}

		public override string ToString() =>
			Latitude.ToString("#0.0#######", CultureInfo.InvariantCulture) + "," +
			Longitude.ToString("#0.0#######", CultureInfo.InvariantCulture);

		public bool Equals(GeoLocation other)
		{
			if (ReferenceEquals(null, other))
				return false;
			if (ReferenceEquals(this, other))
				return true;
			return Latitude.Equals(other.Latitude) && Longitude.Equals(other.Longitude);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;
			if (ReferenceEquals(this, obj))
				return true;
			if (obj.GetType() != this.GetType())
				return false;
			return Equals((GeoLocation)obj);
		}

		public override int GetHashCode() =>
			unchecked((Latitude.GetHashCode() * 397) ^ Longitude.GetHashCode());

		public string ToString(string format, IFormatProvider formatProvider) => ToString();

		public static implicit operator GeoLocation(string latLon)
		{
            if (string.IsNullOrEmpty(latLon))
                throw new ArgumentNullException(nameof(latLon));

			var parts = latLon.Split(',');
			if (parts.Length != 2) throw new ArgumentException("Invalid format: string must be in the form of lat,lon");
			if (!double.TryParse(parts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out var lat))
				throw new ArgumentException("Invalid latitude value");
			if (!double.TryParse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var lon))
				throw new ArgumentException("Invalid longitude value");
			return new GeoLocation(lat, lon);
		}

		public static implicit operator GeoLocation(double[] lonLat)
		{
			return lonLat.Length != 2
				? null
				: new GeoLocation(lonLat[1], lonLat[0]);
		}
	}

	/// <summary>
	/// Represents a Latitude/Longitude as a 2 dimensional point that gets serialized as new [] { lon, lat }
	/// </summary>
	[JsonConverter(typeof(GeoCoordinateJsonConverter))]
	public class GeoCoordinate : GeoLocation
	{
		/// <summary>
		/// Creates a new instance of <see cref="GeoCoordinate"/>
		/// </summary>
		public GeoCoordinate(double latitude, double longitude) : base(latitude, longitude) { }

		/// <summary>
		/// Creates a new instance of <see cref="GeoCoordinate"/> from a pair of coordinates
		/// in the order Latitude then Longitude.
		/// </summary>
		public static implicit operator GeoCoordinate(double[] coordinates)
		{
			if (coordinates == null || coordinates.Length != 2)
				throw new ArgumentOutOfRangeException(
					nameof(coordinates),
					$"Can not create a {nameof(GeoCoordinate)} from an array that does not have two doubles");

			return new GeoCoordinate(coordinates[0], coordinates[1]);
		}
	}
}
