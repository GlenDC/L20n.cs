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

namespace L20n
{
	namespace Types
	{
		namespace Internal
		{
			namespace Expressions
			{
				public class HashValue : Value
				{
					private readonly Dictionary<string, Primary> m_Table;
					private readonly Primary m_DefaultValue;

					public HashValue(Dictionary<string, Primary> table, Primary default_value)
					{
						m_Table = table;
						m_DefaultValue = default_value;
					}

					public Primary Get(string identifier) {
						Primary value = null;

						if (!m_Table.TryGetValue (identifier, out value)) {
							value = m_DefaultValue;
						}

						if(value == null) {
							string msg = String.Format(
								"<hash_item> could not be found for <identifier> {0}, and no default was been set for this <hash_value>",
								identifier);
							// TODO: We might want to wrap around all the different exceptions
							// so that people can always use the same L20n module for any exception type
							// rather than all kind of different .NET ones
							throw new KeyNotFoundException(msg);
						}

						return value;
					}
					
					public override string ToString()
					{
						throw new Exception("TODO");
					}
				}
			}
		}
	}
}