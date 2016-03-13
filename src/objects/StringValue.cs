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

using L20n.Utils;
using L20n.Internal;
using L20n.Exceptions;

namespace L20n
{
	namespace Objects
	{
		public sealed class StringValue : Primitive
		{
			/*public string Value
			{
				get
				{
					if(m_Expressions.Length != 0) {
						throw new EvaluateException(
							String.Format(
								"can't return `{0}` as <string_value> has {1} expression(s)",
								m_Value, m_Expressions.Length));
					}

					return m_Value;
				}
			}*/

			private readonly string m_Value;
			private readonly L20nObject[] m_Expressions;

			public StringValue(string value, L20nObject[] expressions)
			{
				m_Value = value;
				m_Expressions = expressions;
			}

			public override Option<L20nObject> Eval(LocaleContext ctx, params L20nObject[] argv)
			{
				if (m_Expressions.Length == 0)
					return new Option<L20nObject>(new StringOutput(m_Value));

				string[] expressions = new string[m_Expressions.Length];
				for (int i = 0; i < expressions.Length; ++i) {
					var e = m_Expressions[i].Eval(ctx);

					if(!e.IsSet) {
						return e;
					}

					Identifier identifier;
					while(e.Unwrap().As<Identifier>(out identifier)) {
						e = ctx.GetEntity(identifier.Value)
							.Map((entity) => entity.Eval(ctx));
						
						if(!e.IsSet) {
							return e;
						}
					}

					var stringOutput = e.Unwrap().As<Primitive>().ToString(ctx);
					if(!stringOutput.IsSet) {
						return L20nObject.None;
					}

					expressions[i] = stringOutput.Unwrap();
				}

				var output = String.Format(m_Value, expressions);
				return new Option<L20nObject>(new StringOutput(output));
			}

			public override Option<string> ToString(LocaleContext ctx, params L20nObject[] argv)
			{
				return Eval(ctx)
					.Map((primitive) =>
					     primitive.As<Primitive>().ToString(ctx));
			}
		}
	}
}
