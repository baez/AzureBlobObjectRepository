using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace AzureBlobsCore.Shared
{
    public static class Serializer
    {
        public static string WriteFromObject(Type type, object obj)
        {
            var ms = new MemoryStream();

            var ser = new DataContractJsonSerializer(type);
            ser.WriteObject(ms, obj);
            byte[] json = ms.ToArray();

            ms.Close();

            return Encoding.UTF8.GetString(json, 0, json.Length);
        }

        public static T ReadToTrackObject<T>(string json) where T : class
        {
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            var ser = new DataContractJsonSerializer(typeof(T));

            var deserializedObject = ser.ReadObject(ms) as T;

            ms.Close();

            return deserializedObject;
        }

    }
}
