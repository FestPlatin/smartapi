﻿// Smart API - .Net programmatic access to RedDot servers
//  
// Copyright (C) 2013 erminas GbR
// 
// This program is free software: you can redistribute it and/or modify it 
// under the terms of the GNU General Public License as published by the Free Software Foundation,
// either version 3 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License along with this program.
// If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Web;
using System.Xml;
using erminas.SmartAPI.CMS.Project.Filesystem;
using erminas.SmartAPI.Utils;

namespace erminas.SmartAPI.CMS.Project.Pages.Elements
{
    public class AbstractMediaElement : PageElement
    {
        private File _file;

        protected AbstractMediaElement(Project project, Guid guid, LanguageVariant languageVariant)
            : base(project, guid, languageVariant)
        {
        }

        protected AbstractMediaElement(Project project, XmlElement xmlElement) : base(project, xmlElement)
        {
            LoadWholePageElement();
        }

        public void Commit()
        {
            const string COMMIT =
                @"<ELT action=""save"" reddotcacheguid="""" guid=""{0}"" value=""{1}"" subdirguid=""{2}"" extendedinfo=""""></ELT>";

            string rqlStr = Value == null
                                ? string.Format(COMMIT, Guid.ToRQLString(), Session.SESSIONKEY_PLACEHOLDER,
                                                Session.SESSIONKEY_PLACEHOLDER)
                                : string.Format(COMMIT, Guid.ToRQLString(), HttpUtility.HtmlEncode(Value.Name),
                                                Value.Folder.Guid.ToRQLString());

            Project.ExecuteRQL(rqlStr);
        }

        public File Value
        {
            get { return LazyLoad(ref _file); }
            set { _file = value; }
        }

        protected override sealed void LoadWholePageElement()
        {
            var folder = GetFolder();
            if (folder == null)
            {
                return;
            }
            InitFileValue(folder);
        }

        private void InitFileValue(Folder folder)
        {
            var fileName = XmlElement.GetAttributeValue("value");

            var files = folder.GetFilesByNamePattern(fileName);
            _file = files.Find(file => file.Name == fileName);
        }

        private Folder GetFolder()
        {
            Guid folderGuid;
            if (!XmlElement.TryGetGuid("subdirguid", out folderGuid))
            {
                _file = null;
                return null;
            }
            return Project.Folders.GetByGuid(folderGuid);
        }
    }
}