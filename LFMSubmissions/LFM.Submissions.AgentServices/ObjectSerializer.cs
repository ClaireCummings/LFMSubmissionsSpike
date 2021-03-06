﻿using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace LFM.Submissions.AgentServices
{
    /// <summary>
    /// Serialization and Deserialization tasks for an ojbect.
    /// </summary>
    public class ObjectSerializer : IObjectSerializer
    {
        /// <summary>
        /// Serializes specified object to an XML string.
        /// </summary>
        /// <param name="objectInstance">Object to serialize.</param>
        /// <returns>XML string.</returns>
        public string XmlSerializeToString<T>(T objectInstance)
        {
            var serializer = new XmlSerializer(objectInstance.GetType());
            var sb = new StringBuilder();

            using (TextWriter writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, objectInstance);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Deserializes an XML string to an object.
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize.</typeparam>
        /// <param name="objectData">XML string to deserialize.</param>
        /// <returns>Object.</returns>
        public T XmlDeserializeFromString<T>(string objectData)
        {
            return (T)XmlDeserializeFromString(objectData, typeof(T));
        }

        /// <summary>
        /// Deserializes an XML string to an object of the specified type.
        /// </summary>
        /// <param name="objectData">XML string to deserialize.</param>
        /// <param name="type">Type of object to deserialize.</param>
        /// <returns>Object.</returns>
        public object XmlDeserializeFromString(string objectData, Type type)
        {
            var serializer = new XmlSerializer(type);
            object result;
            
            try
            {
                using (TextReader reader = new StringReader(objectData))
                {
                    result = serializer.Deserialize(reader);
                }
            }

            catch (Exception ex)
            {
                throw new DeserializationException("Failed to deserialize object data", ex);
            }

            return result;
        }
    }
}
