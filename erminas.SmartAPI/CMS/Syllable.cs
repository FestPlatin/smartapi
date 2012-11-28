/*
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

using System;
using System.Xml;

namespace erminas.SmartAPI.CMS
{
    /// <summary>
    ///   Represents a prefix or a postfix.
    /// </summary>
    public class Syllable : PartialRedDotObject
    {
        protected readonly Project Project;
        private string _name;

        public Syllable(Project project, XmlNode node)
            : base(node)
        {
            Project = project;
            LoadXml(node);
        }

        public Syllable(Project project, Guid guid)
            : base(guid)
        {
            Project = project;
        }

        /// <summary>
        ///   Name of the syllable
        /// </summary>
        public override string Name
        {
            get { return LazyLoad(ref _name); }
        }

        protected override void LoadXml(XmlNode node)
        {
            InitIfPresent(ref _name, "name", x => x);
        }

        protected override XmlNode RetrieveWholeObject()
        {
            return Project.Syllables.GetByGuid(Guid).XmlNode;
        }
    }
}