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

using SimpleJSON;

using L20n.Exceptions;

namespace L20n
{
	namespace Internal
	{
		public sealed class Manifest
		{
			private const string LOCALE_STRING_ID = "{{locale}}";
			
			private List<string> m_Locales;
			private string m_DefaultLocale;
			private List<string> m_Resources;
			
			public List<string> Locales
			{
				get { return m_Locales; }
			}
			
			public string DefaultLocale
			{
				get { return m_DefaultLocale; }
				private set
				{
					if(!m_Locales.Contains(value))
					{
						string msg = string.Format("{0} is not a valid default locale as " +
						                           "it couldn't be found in the list of locales.", value);
						throw new ImportException(msg);
					}
					
					m_DefaultLocale = value;
				}
			}
			
			public Manifest()
			{
				m_Locales = new List<string>();
				m_DefaultLocale = null;
				m_Resources = new List<string>();
			}

			public void Import(string manifest_path)
			{
				Flush();

				using(var sr = new StreamReader(manifest_path))
				{
					var json = sr.ReadToEnd();
					var root = JSON.Parse(json);
					
					var locales = root["locales"];
					if(locales == null || locales.Count == 0)
					{
						string msg = string.Format("No locales were provided in: {0}", manifest_path);
						throw new ImportException(msg);
					}
					foreach(var locale in locales.Children)
					{
						m_Locales.Add(locale.Value);
					}
					
					var defaultLocale = root["default_locale"].Value;
					if(defaultLocale == "")
					{
						string msg = string.Format("No default locale was provided in: {0}", manifest_path);
						throw new ImportException(msg);
					}

					DefaultLocale = defaultLocale;
					
					var resources = root["resources"];
					if(resources == null || resources.Count == 0)
					{
						string msg = string.Format("No resources were provided in: {0}", manifest_path);
						throw new ImportException(msg);
					}
					foreach(var resource in resources.Children)
					{
						AddResoure(resource.Value, manifest_path);
					}
				}
			}
			
			public List<String> GetLocaleFiles(string locale)
			{
				var files = new List<String>();
				foreach(var resource in m_Resources) {
					var file = resource.Replace(LOCALE_STRING_ID, locale);
					if(File.Exists(file))
					{
						files.Add(file);
					}
				}
				
				return files;
			}

			private void Flush()
			{
				m_Locales.Clear();
				m_DefaultLocale = null;
				m_Resources.Clear ();
			}
			
			private void AddResoure(string resource, string manifest)
			{
				if (!resource.Contains(LOCALE_STRING_ID))
				{
					string msg = string.Format("Resource '{0}' does not contain local string-id ({1}).",
					                           resource, LOCALE_STRING_ID);
					throw new ImportException(msg);
				}
				
				resource = Path.Combine(new string[] {
					Path.GetDirectoryName(manifest),
					resource});
				
				m_Resources.Add(resource);
			}
		}
	}
}