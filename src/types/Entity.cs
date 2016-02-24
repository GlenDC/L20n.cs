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
using System.IO;
using System.Collections.Generic;

namespace L20n
{
	namespace Types
	{
		public class Entity : Entry
		{	
			private string m_Identifier;
			private Types.Value m_Value;

			public Entity(string id, Types.Value value)
			{
				m_Identifier = id;
				m_Value = value;
			}
			
			public override List<Entity> Evaluate()
			{
				var entities = new List<Entity>();
				entities.Add(this);
				return entities;
			}
			
			public override string ToString()
			{
				return String.Format("<{0} {1}>",
				                     m_Identifier, m_Value.ToString());
			}
		}
	}
}

