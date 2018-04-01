using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;

    /// <summary>
    /// THe code in this class is created by 
    /// Farid Naisan 2010
    /// </summary>

    class XMLSerializerUtility
    {

        /// <summary>
        /// A generic method that can be used to serialize any type of object.
        /// The type of object is defined at method call by the client object
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="filePath">File to which data is to be srialized</param>
        /// <param name="obj">Object containing data to be serialized. This object 
        /// must be Serializable.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static void SerializeToFile<T>(string filePath, T obj)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            
            TextWriter writer = new StreamWriter(filePath);

            try
            {
                serializer.Serialize(writer, obj);
            }
            finally
            {
                if (writer != null)

                    writer.Close();
            }
        }

        /// <summary>
        /// Deserialize any xml file serialized  using the above method.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="filePath">XML file to be deserialized.</param>
        /// <returns>The object containing data read from the xml file.</returns>
        /// <remarks>Object must not have changed its structure since it was serilized calling
        /// the above method.</remarks>
        public static T DeserializeFromFile<T>(string filePath)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            object obj = null;
            //to be returned with data

            TextReader reader = null;

            try
            {
                reader = new StreamReader(filePath);
                obj = (T)serializer.Deserialize(reader);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return (T)obj;
        }
    }

    /// <summary>
    /// Googled code that creates a good way of creating customized serializer
    /// NotUsed
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class XmlAnything<T> : IXmlSerializable
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public XmlAnything() 
        {
 
        }

        /// <summary>
        /// Constructor with one paramter
        /// </summary>
        /// <param name="t"></param>
        public XmlAnything(T t) { this.Value = t; }

        /// <summary>
        /// Property to serialize/deserialize.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Implemet the WriteXml demanded by the interface.
        /// Used to get the real type and serialize it
        /// </summary>
        /// <param name="writer"></param>
        public void WriteXml(XmlWriter writer)
        {
            if (Value == null)
            {
                writer.WriteAttributeString("type", "null");
                return;
            }
            Type type = this.Value.GetType();
            XmlSerializer serializer = new XmlSerializer(type);
            writer.WriteAttributeString("type", type.AssemblyQualifiedName);
            serializer.Serialize(writer, this.Value);
        }

        /// <summary>
        /// Implement the ReadXml demanded by the interface
        /// Used to deserialize and get the real type.
        /// </summary>
        /// <param name="reader"></param>
        public void ReadXml(XmlReader reader)
        {
            if (!reader.HasAttributes)
                throw new FormatException("expected a type attribute!");
            string type = reader.GetAttribute("type");
            reader.Read(); // consume the value
            if (type == "null")
                return;// leave T at default value
            XmlSerializer serializer = new XmlSerializer(Type.GetType(type));
            this.Value = (T)serializer.Deserialize(reader);
            reader.ReadEndElement();
        }

        public XmlSchema GetSchema() { return (null); }
    }
