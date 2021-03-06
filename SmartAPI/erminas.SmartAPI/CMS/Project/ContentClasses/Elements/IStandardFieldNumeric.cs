﻿// SmartAPI - .Net programmatic access to RedDot servers
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

using System.Xml;

namespace erminas.SmartAPI.CMS.Project.ContentClasses.Elements
{
    public interface IStandardFieldNumeric : IStandardFieldNonDate
    {
        int? MaxCharacterCount { get; set; }
    }

    internal class StandardFieldNumeric : StandardFieldNonDate, IStandardFieldNumeric
    {
        internal StandardFieldNumeric(IContentClass contentClass, XmlElement xmlElement)
            : base(contentClass, xmlElement)
        {
        }

        [RedDot("eltmaxsize")]
        public int? MaxCharacterCount
        {
            get { return GetAttributeValue<int?>(); }
            set { SetAttributeValue(value); }
        }
    }
}