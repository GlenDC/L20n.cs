/**
 * This source file is part of the Commercial L20n Unity Plugin.
 * 
 * Copyright (c) 2016 - 2017 Glen De Cauwsemaecker (contact@glendc.com)
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0

 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;

using L20n.Internal;
using L20n.Utils;

namespace L20n
{
	namespace Objects
	{
		public sealed class CallExpression : L20nObject
		{
			private readonly L20nObject[] m_Variables;
			private readonly string m_Identifier;
			
			public CallExpression(string identifier, L20nObject[] variables)
			{
				m_Identifier = identifier;
				m_Variables = variables;
			}

			public override L20nObject Optimize()
			{
				return this;
			}
			
			public override Option<L20nObject> Eval(LocaleContext ctx, params L20nObject[] argv)
			{
				return ctx.GetMacro(m_Identifier)
					.MapOrWarning(
						(macro) => macro.Eval(ctx, m_Variables),
						"couldn't find macro with name {0}", m_Identifier);
			}
		}
	}
}
