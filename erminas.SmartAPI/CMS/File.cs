﻿/*
 * Smart API - .Net programatical access to RedDot servers
 * Copyright (C) 2012  erminas GbR 
 *
 * This program is free software: you can redistribute it and/or modify it 
 * under the terms of the GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details. 
 *
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>. 
 */

using System.Xml;
using erminas.SmartAPI.Utils;

namespace erminas.SmartAPI.CMS
{
    public class File
    {
        /// <summary>
        ///   Date/Time of file creation as CMS locale specific string
        /// </summary>
        public readonly string CreationDate;

        /// <summary>
        ///   Folder the file is stored in
        /// </summary>
        public readonly Folder Folder;

        /// <summary>
        ///   Name of the file
        /// </summary>
        public readonly string Name;

        public readonly Project Project;

        public File(Project project, XmlNode xmlNode)
        {
            Project = project;

            Name = xmlNode.GetAttributeValue("name");
            CreationDate = xmlNode.GetAttributeValue("data");
            Folder = new Folder(project, xmlNode.GetGuid("folderguid"));
        }

        public int ReferenceCount()
        {
            const string GET_REFERENCES =
                "<PROJECT><TRANSFER action=\"checkimage\" getreferences=\"1\" sync=\"1\" folderguid=\"{0}\" filename=\"{1}\" /></PROJECT>";

            XmlDocument xmlDoc = Project.ExecuteRQL(string.Format(GET_REFERENCES, Folder.Guid.ToRQLString(), Name));
            return xmlDoc.GetElementsByTagName("REFERENCE").Count;
        }

        public void Delete()
        {
            //TODO ist ENG okay, oder sollte man z.b. die standardsprache waehlen
            const string DELETE_FILE =
                "<MEDIA><FOLDER guid=\"{0}\" ><FILES action=\"deletefiles\"><FILE sourcename=\"{1}\" languagevariantid=\"ENG\" checkfolder=\"1\"/></FILES></FOLDER></MEDIA>";

            Project.ExecuteRQL(string.Format(DELETE_FILE, Folder.Guid.ToRQLString(), Name));
        }
    }
}