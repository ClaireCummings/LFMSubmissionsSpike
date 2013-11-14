using System;

namespace LFM.Submissions.AgentServices
{
    public interface IObjectSerializer
    {
        /// <summary>
        /// Serializes specified object to an XML string.
        /// </summary>
        /// <param name="objectInstance">Object to serialize.</param>
        /// <returns>XML string.</returns>
        string XmlSerializeToString<T>(T objectInstance);

        /// <summary>
        /// Deserializes an XML string to an object.
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize.</typeparam>
        /// <param name="objectData">XML string to deserialize.</param>
        /// <returns>Object.</returns>
        T XmlDeserializeFromString<T>(string objectData);

        /// <summary>
        /// Deserializes an XML string to an object of the specified type.
        /// </summary>
        /// <param name="objectData">XML string to deserialize.</param>
        /// <param name="type">Type of object to deserialize.</param>
        /// <returns>Object.</returns>
        object XmlDeserializeFromString(string objectData, Type type);
    }
}