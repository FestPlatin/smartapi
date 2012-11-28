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

using System;
using System.Xml;
using erminas.SmartAPI.CMS.CCElements.Attributes;

namespace erminas.SmartAPI.CMS.CCElements
{

    #region Scrolling

    public enum Scrolling
    {
        NotSet = 0,
        Yes,
        No,
        Auto
    }

    public static class ScrollingUtils
    {
        public static string ToRQLString(this Scrolling value)
        {
            switch (value)
            {
                case Scrolling.NotSet:
                    return "";
                case Scrolling.Auto:
                    return "auto";
                case Scrolling.Yes:
                    return "yes";
                case Scrolling.No:
                    return "no";
                default:
                    throw new ArgumentException(string.Format("Unknown {0} value: {1}",
                                                              typeof (Scrolling).Name, value));
            }
        }

        public static Scrolling ToScrolling(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Scrolling.NotSet;
            }
            switch (value.ToLowerInvariant())
            {
                case "auto":
                    return Scrolling.Auto;
                case "yes":
                    return Scrolling.Yes;
                case "no":
                    return Scrolling.No;
                default:
                    throw new ArgumentException(string.Format("Cannot convert string value {1} to {0}",
                                                              typeof (Scrolling).Name, value));
            }
        }
    }

    #endregion

    #region Frameborder

    public enum Frameborder
    {
        NotSet = 0,
        Yes,
        No
    }

    public static class FrameborderUtils
    {
        public static string ToRQLString(this Frameborder value)
        {
            switch (value)
            {
                case Frameborder.NotSet:
                    return string.Empty;
                case Frameborder.Yes:
                    return "yes";
                case Frameborder.No:
                    return "no";
                default:
                    throw new ArgumentException(string.Format("Unknown {0} value: {1}",
                                                              typeof (Frameborder).Name, value));
            }
        }

        public static Frameborder ToFrameborder(string value)
        {
            switch (value.ToLowerInvariant())
            {
                case "yes":
                    return Frameborder.Yes;
                case "no":
                    return Frameborder.No;
                case "":
                case null:
                    return Frameborder.NotSet;
                default:
                    throw new ArgumentException(string.Format("Cannot convert string value {1} to {0}",
                                                              typeof (Frameborder).Name, value));
            }
        }
    }

    #endregion

    public class Frame : CCElement
    {
        public Frame(ContentClass contentClass, XmlNode xmlNode)
            : base(contentClass, xmlNode)
        {
            CreateAttributes("eltxhtmlcompliant", "eltframename", "eltmarginwidth",
                             "eltmarginheight", "eltscrolling", "eltsrc",
                             "eltsupplement", "eltframeborder", "eltnoresize"
                );
        }

        public override ContentClassCategory Category
        {
            get { return ContentClassCategory.Structural; }
        }

        public bool IsSyntaxConformingToXHtml
        {
            get { return ((BoolXmlNodeAttribute) GetAttribute("eltxhtmlcompliant")).Value; }
            set { ((BoolXmlNodeAttribute) GetAttribute("eltxhtmlcompliant")).Value = value; }
        }

        public string FrameName
        {
            get { return ((StringXmlNodeAttribute) GetAttribute("eltframename")).Value; }
            set { ((StringXmlNodeAttribute) GetAttribute("eltframename")).Value = value; }
        }

        public string MarginWidth
        {
            get { return ((StringXmlNodeAttribute) GetAttribute("eltmarginwidth")).Value; }
            set { ((StringXmlNodeAttribute) GetAttribute("eltmarginwidth")).Value = value; }
        }

        public string MarginHeight
        {
            get { return ((StringXmlNodeAttribute) GetAttribute("eltmarginheight")).Value; }
            set { ((StringXmlNodeAttribute) GetAttribute("eltmarginheight")).Value = value; }
        }

        public Scrolling Scrolling
        {
            get { return ((StringEnumXmlNodeAttribute<Scrolling>) GetAttribute("eltscrolling")).Value; }
            set { ((StringEnumXmlNodeAttribute<Scrolling>) GetAttribute("eltscrolling")).Value = value; }
        }

        public string Src
        {
            get { return ((StringXmlNodeAttribute) GetAttribute("eltsrc")).Value; }
            set { ((StringXmlNodeAttribute) GetAttribute("eltsrc")).Value = value; }
        }

        public string Supplement
        {
            get { return ((StringXmlNodeAttribute) GetAttribute("eltsupplement")).Value; }
            set { ((StringXmlNodeAttribute) GetAttribute("eltsupplement")).Value = value; }
        }

        public Frameborder Frameborder
        {
            get { return ((StringEnumXmlNodeAttribute<Frameborder>) GetAttribute("eltframeborder")).Value; }
            set { ((StringEnumXmlNodeAttribute<Frameborder>) GetAttribute("eltframeborder")).Value = value; }
        }

        public bool IsNotResizing
        {
            get { return ((BoolXmlNodeAttribute) GetAttribute("eltnoresize")).Value; }
            set { ((BoolXmlNodeAttribute) GetAttribute("eltnoresize")).Value = value; }
        }
    }
}