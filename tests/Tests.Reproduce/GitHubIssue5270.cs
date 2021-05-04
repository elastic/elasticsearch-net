// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;

namespace Tests.Reproduce
{
	public class GitHubIssue5270
	{
		private static readonly byte[] ResponseBytes = Encoding.UTF8.GetBytes(@"{
  ""test admin role mapping"" : {
    ""enabled"" : true,
    ""roles"" : [
      ""apm_user""
    ],
    ""rules"" : {
      ""any"" : [
        {
          ""field"" : {
            ""dn"" : [
              ""CN=Bloggs Joe abcdef01,OU=Users,OU=_Central,OU=S1000,OU=SG001,DC=ad001,DC=example,DC=net"",
              ""cn=bloggs joe abcdef02,ou=usersfunctional,ou=_central,ou=accadm,OU=SG001,DC=ad001,DC=example,DC=net""
            ]
          }
        }
      ]
    },
    ""metadata"" : { }
  }
}");

		[U]
		public async Task GetRoleMappings()
		{
			var pool = new SingleNodeConnectionPool(new Uri($"http://localhost:9200"));
			var settings = new ConnectionSettings(pool, new InMemoryConnection(ResponseBytes));
			var client = new ElasticClient(settings);

			// ReSharper disable once MethodHasAsyncOverload
			var roleResponse = client.Security.GetRoleMapping();
			roleResponse.RoleMappings.Count.Should().Be(1);
			Assert(roleResponse);

			roleResponse = await client.Security.GetRoleMappingAsync();
			roleResponse.RoleMappings.Count.Should().Be(1);
			Assert(roleResponse);
		}

		private static void Assert(GetRoleMappingResponse roleResponse) =>
			roleResponse.RoleMappings["test admin role mapping"]
				.Rules.Should()
				.BeAssignableTo<AnyRoleMappingRule>()
				.Subject.Any.First()
				.Should()
				.BeAssignableTo<FieldRoleMappingRule>()
				.Subject.Field.Should()
				.BeAssignableTo<DistinguishedNameRule>()
				.Subject["dn"].Should().BeAssignableTo<IEnumerable<string>>()
				.Subject.Count().Should().Be(2);
	}
}
