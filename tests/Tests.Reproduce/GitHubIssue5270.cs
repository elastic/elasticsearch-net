/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
