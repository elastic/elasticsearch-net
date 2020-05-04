// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;

namespace Nest
{
	public class AllRoleMappingRule : RoleMappingRuleBase
	{
		public AllRoleMappingRule(params RoleMappingRuleBase[] rules) => AllRules = rules;

		public AllRoleMappingRule(IEnumerable<RoleMappingRuleBase> rules) => AllRules = rules;

		public IEnumerable<RoleMappingRuleBase> All => AllRules;
	}
}
