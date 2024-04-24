﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CodeWalker.GameFiles
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class YfdFile : GameFile, PackedFile
    {
        public FrameFilterDictionary FrameFilterDictionary { get; set; }

        public string LoadException { get; set; }


        public YfdFile() : base(null, GameFileType.Yfd)
        {
        }
        public YfdFile(RpfFileEntry entry) : base(entry, GameFileType.Yfd)
        {
        }

        public void Load(byte[] data, RpfFileEntry entry)
        {
            Name = entry.Name;
            RpfFileEntry = entry;
            //Hash = entry.ShortNameHash;


            if (entry is not RpfResourceFileEntry resentry)
            {
                ThrowFileIsNotAResourceException();
                return;
            }


            try
            {
                using var rd = new ResourceDataReader(resentry, data);

                FrameFilterDictionary = rd.ReadBlock<FrameFilterDictionary>();
            }
            catch (Exception ex)
            {
                LoadException = ex.ToString();
            }
        }

        public byte[] Save()
        {
            byte[] data = ResourceBuilder.Build(FrameFilterDictionary, 4); //yfd is version 4...

            return data;
        }
    }


    public class YfdXml : MetaXmlBase
    {
        public static string GetXml(YfdFile yfd)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(XmlHeader);

            if (yfd?.FrameFilterDictionary != null)
            {
                FrameFilterDictionary.WriteXmlNode(yfd.FrameFilterDictionary, sb, 0);
            }

            return sb.ToString();
        }

    }

    public class XmlYfd
    {
        public static YfdFile GetYfd(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return GetYfd(doc);
        }

        public static YfdFile GetYfd(XmlDocument doc)
        {
            YfdFile yfd = new YfdFile();
            yfd.FrameFilterDictionary = new FrameFilterDictionary();
            yfd.FrameFilterDictionary.ReadXml(doc.DocumentElement);
            return yfd;
        }
    }
}
