﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Download_MThread.Core
{
    public static class XmlLoaderTest
    {
        public static List<ImageFile> GetUrl()
        {
            var cardsList = new List<ImageFile>();
            var xDoc = new XmlDocument();
            xDoc.Load("HS cardlist.xml");

            var cards = xDoc.GetElementsByTagName("Card");
            for (int i = 0; i < cards.Count; i++)
            {
                var xmlElement = cards[i]["image"];
                if (xmlElement != null)
                {
                    cardsList.Add(MakeFile(cards[i]));
                }
            }
            return cardsList; 
        }
        private static ImageFile MakeFile(XmlNode xmlNode)
        {
            var File = new ImageFile();
            if (xmlNode["image"] != null)
            {
                File.Url = xmlNode["image"].InnerText;
            }
            if (xmlNode["name"] != null)
            {
                File.Name = xmlNode["name"].InnerText;
            }
            return File;
        }
    }
}