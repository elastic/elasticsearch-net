// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("security.put_user.json")]
	public partial interface IPutUserRequest
	{
		/// <summary>
		/// The email of the user.
		/// </summary>
		[DataMember(Name = "email")]
		string Email { get; set; }

		/// <summary>
		/// The full name of the user.
		/// </summary>
		[DataMember(Name = "full_name")]
		string FullName { get; set; }

		/// <summary>
		/// Arbitrary metadata that you want to associate with the user.
		/// </summary>
		[DataMember(Name = "metadata")]
		IDictionary<string, object> Metadata { get; set; }

		/// <summary>
		/// The user’s password. Passwords must be at least 6 characters long.
		/// </summary>
		/// <remarks>
		/// When adding a user, one of <see cref="Password" /> or <see cref="PasswordHash" /> is required. When updating an
		/// existing user,
		/// the password is optional, so that other fields on the user (such as their roles) may be updated without modifying the
		/// user’s password.
		/// </remarks>
		[DataMember(Name = "password")]
		string Password { get; set; }

		/// <summary>
		/// A hash of the user’s password. This must be produced using the same hashing algorithm as has been configured for
		/// password storage.
		/// Using this parameter allows the client to pre-hash the password for performance and/or confidentiality reasons.
		/// The <see cref="Password" /> parameter and the <see cref="PasswordHash" /> parameter cannot be used in the same request.
		/// </summary>
		[DataMember(Name = "password_hash")]
		string PasswordHash { get; set; }

		/// <summary>
		/// A set of roles the user has. The roles determine the user’s access permissions. To create a user without any roles,
		/// specify an empty list.
		/// </summary>
		[DataMember(Name = "roles")]
		IEnumerable<string> Roles { get; set; }
	}

	public partial class PutUserRequest
	{
		/// <inheritdoc />
		public string Email { get; set; }

		/// <inheritdoc />
		public string FullName { get; set; }

		/// <inheritdoc />
		public IDictionary<string, object> Metadata { get; set; }

		/// <inheritdoc />
		public string Password { get; set; }

		/// <inheritdoc />
		public string PasswordHash { get; set; }

		/// <inheritdoc />
		public IEnumerable<string> Roles { get; set; }
	}

	public partial class PutUserDescriptor
	{
		/// <inheritdoc />
		string IPutUserRequest.Email { get; set; }

		/// <inheritdoc />
		string IPutUserRequest.FullName { get; set; }

		/// <inheritdoc />
		IDictionary<string, object> IPutUserRequest.Metadata { get; set; }

		/// <inheritdoc />
		string IPutUserRequest.Password { get; set; }

		/// <inheritdoc />
		string IPutUserRequest.PasswordHash { get; set; }

		/// <inheritdoc />
		IEnumerable<string> IPutUserRequest.Roles { get; set; }

		/// <inheritdoc cref="IPutUserRequest.Password" />
		public PutUserDescriptor Password(string password) => Assign(password, (a, v) => a.Password = v);

		/// <inheritdoc cref="IPutUserRequest.PasswordHash" />
		public PutUserDescriptor PasswordHash(string passwordHash) => Assign(passwordHash, (a, v) => a.PasswordHash = v);

		/// <inheritdoc cref="IPutUserRequest.Roles" />
		public PutUserDescriptor Roles(IEnumerable<string> roles) => Assign(roles, (a, v) => a.Roles = v);

		/// <inheritdoc cref="IPutUserRequest.Roles" />
		public PutUserDescriptor Roles(params string[] roles) => Assign(roles, (a, v) => a.Roles = v);

		/// <inheritdoc cref="IPutUserRequest.FullName" />
		public PutUserDescriptor FullName(string fullName) => Assign(fullName, (a, v) => a.FullName = v);

		/// <inheritdoc cref="IPutUserRequest.Email" />
		public PutUserDescriptor Email(string email) => Assign(email, (a, v) => a.Email = v);

		/// <inheritdoc cref="IPutUserRequest.Metadata" />
		public PutUserDescriptor Metadata(IDictionary<string, object> metadata) => Assign(metadata, (a, v) => a.Metadata = v);

		/// <inheritdoc cref="IPutUserRequest.Metadata" />
		public PutUserDescriptor Metadata(Func<FluentDictionary<string, object>, IDictionary<string, object>> selector) =>
			Assign(selector, (a, v) => a.Metadata = v?.Invoke(new FluentDictionary<string, object>()));
	}
}
