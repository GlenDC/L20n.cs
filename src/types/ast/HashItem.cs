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

namespace L20n
{
	namespace Types
	{
		namespace AST
		{
			public class HashItem
			{
				public bool IsDefault
				{
					get { return m_IsDefault; }
				}

				public string Identifier
				{
					get { return m_Identifier; }
				}

				public Internal.Expressions.Primary Value
				{
					get { return m_Value; }
				}

				private readonly bool m_IsDefault;
				private readonly string m_Identifier;
				private readonly Internal.Expressions.Primary m_Value;

				public HashItem(string id, Internal.Expressions.Primary value, bool is_default)
				{
					m_Identifier = id;
					m_Value = value;
					m_IsDefault = is_default;
				}
			}
		}
	}
}
