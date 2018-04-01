using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


    class BinSerializerUtility
    {
        /// <summary>
        /// THe code in this class is created by 
        /// Farid Naisan 2010
        /// Both the bin and Array is in the same.
        /// </summary>
        

        /// <summary>
        /// Function serializing any type of object 
        /// </summary>
        /// <param name="obj"> Object to be serialized. This object 
        /// must be Serializable.</param>
        /// <param name="filePath">File path including the name of the file to be serialized.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static void Serialize(object obj, string filePath)
        {
            FileStream fileObj = null;
            try
            {
                //Steps in serializing an object
                fileObj = new FileStream(filePath, FileMode.Create);
                BinaryFormatter binFormatter = new BinaryFormatter();
                binFormatter.Serialize(fileObj, obj);
            }
            finally
            {
                if (fileObj != null)
                    fileObj.Close();

            }
        }

        /// <summary>
        /// Generic method for deserializing any type of object 
        /// </summary>
        /// <typeparam name="T"> Any object</typeparam>
        /// <param name="filepath">File path including the name of the file to be deserialized</param>
        /// <returns></returns>
        /// <remarks>Object must not have changed its structure since it was serilized calling
        /// the above method.</remarks>
        public static T Deserialize<T>(string filepath)
        {
            FileStream fileObj = null;
            object obj = null;

            try
            {
                if (!File.Exists(filepath))
                {
                    throw new FileNotFoundException("The file is not found. ", filepath);
                }

                fileObj = new FileStream(filepath, FileMode.Open);

                BinaryFormatter binFormatter = new BinaryFormatter();
                obj = (T)binFormatter.Deserialize(fileObj);
            }
            finally
            {
                if (fileObj != null)
                {
                    fileObj.Close();
                }
            }

            return (T)obj;
        }
        /// <summary>
        /// A general function serializing an object to an array. The array can then be
        /// send to other programs, over the network, etc.
        /// </summary>
        /// <param name="obj">object to be serialized. This object must be Serializable.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static byte[] Serialize(object obj)
        {
            byte[] serializedObject = null;
            MemoryStream memStream = null;

            try
            {
                memStream = new MemoryStream();
                BinaryFormatter binFormatter = new BinaryFormatter();

                binFormatter.Serialize(memStream, obj);
                memStream.Seek(0, 0);			    //set position at 0,0
                serializedObject = memStream.ToArray();
            }
            finally
            {
                if (memStream != null)
                    memStream.Close();
            }

            return serializedObject;    // return the array.
        }

        /// <summary>
        /// Deserialize any type of array object.  The type (T) is defined when
        /// calling this function.
        /// </summary>
        /// <typeparam name="T">Any array type</typeparam>
        /// <param name="serializedObject">Array object containing data.</param>
        /// <returns>The array object containing the data read from the serializedObject.</returns>
        /// <remarks>Object must not have changed its structure since it was serilized calling
        /// the above method.</remarks>
        public static T Deserialize<T>(byte[] serializedObject)
        {
            object obj = null;    //object to return
            MemoryStream memStream = null;

            try
            {
                memStream = new MemoryStream();
                memStream.Write(serializedObject, 0, serializedObject.Length);
                memStream.Seek(0, 0);  //set position at 0,0

                BinaryFormatter binFormatter = new BinaryFormatter();
                obj = binFormatter.Deserialize(memStream);
            }
            finally
            {
                if (memStream != null)
                    memStream.Close();
            }

            return (T)obj;		// convert obj to correct type!
        }
    }