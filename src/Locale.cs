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
using System.IO;

namespace L20n
{
	public class Locale
	{
		List<Types.Entity> m_Entities;

		public Locale()
		{
			m_Entities = new List<Types.Entity>();
		}

		public void Import(String file_name)
		{
			try {
				if (!IO.LocalizbleObjectsList.Parse (file_name, m_Entities)) {
					throw new IOException("no entities got imported");
				}
			}
			catch(Exception e) {
				string msg = String.Format(
					"something went wrong importing locale file: {0}",
					file_name);
				throw new IOException(msg, e);
			}
		}
	}
}
