using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

/*
 * Taken from SolrNet https://github.com/mausch/SolrNet/blob/master/SolrNet/Location.cs
 */

namespace Nest
{

	/// <summary>
	/// Represents a Latitude/Longitude as a 2 dimensional point. 
	/// </summary>
	public class GeoLocation : IEquatable<GeoLocation>, IFormattable
	{
		/// <summary>
		/// Latitude
		/// </summary>
		public double Latitude { get { return _latitude; } }
		private readonly double _latitude;

		/// <summary>
		/// Longitude
		/// </summary>
		public double Longitude { get { return _longitude; } }
		private readonly double _longitude;

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
			_latitude = latitude;
			_longitude = longitude;
		}

		/// <summary>
		/// True if <paramref name="latitude"/> is a valid latitude. Otherwise false.
		/// </summary>
		/// <param name="latitude"></param>
		/// <returns></returns>
		public static bool IsValidLatitude(double latitude)
		{
			return latitude >= -90 && latitude <= 90;
		}

		/// <summary>
		/// True if <paramref name="longitude"/> is a valid longitude. Otherwise false.
		/// </summary>
		/// <param name="longitude"></param>
		/// <returns></returns>
		public static bool IsValidLongitude(double longitude)
		{
			return longitude >= -180 && longitude <= 180;
		}

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

		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0},{1}", _latitude, _longitude);
		}

		public bool Equals(GeoLocation other)
		{
			if (ReferenceEquals(null, other))
				return false;
			if (ReferenceEquals(this, other))
				return true;
			return _latitude.Equals(other._latitude) && _longitude.Equals(other._longitude);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;
			if (ReferenceEquals(this, obj))
				return true;
			if (obj.GetType() != this.GetType())
				return false;
			return Equals((GeoLocation) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (_latitude.GetHashCode()*397) ^ _longitude.GetHashCode();
			}
		}

		public string ToString(string format, IFormatProvider formatProvider)
		{
			return ToString();
		}
	}
}
