﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace DatabaseObjectSearcher
{
    public class Serializator
    {
        public static T Load<T>(string fileName)
        {
            // return empty list if file is not exist
            if (!File.Exists(fileName))
            {
                return Activator.CreateInstance<T>();
            }

            using (TextReader reader = new StreamReader(fileName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                T data = (T)serializer.Deserialize(reader);
                return data;
            }
        }

        public static void Save(string fullName, object obj)
        {
            if (!Directory.Exists(Path.GetDirectoryName(fullName)))
                Directory.CreateDirectory(Path.GetDirectoryName(fullName));

            string varTempFile = Path.Combine(Path.GetDirectoryName(fullName), "temporary");

            using (TextWriter textWriter = new StreamWriter(varTempFile))
            {
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(textWriter, obj);
                textWriter.Flush();
                textWriter.Close();
            }

             string errFile = Path.Combine(Path.GetDirectoryName(fullName), "tmpReplaceError.xml");
             File.Replace(varTempFile, fullName, errFile);
        }
    }
}