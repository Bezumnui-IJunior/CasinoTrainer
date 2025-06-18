using System;
using System.Reflection;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;

namespace Progress
{
    public class PersistantSerializer
    {
        private readonly string _prefsKey;

        public PersistantSerializer(string prefsKey)
        {
            _prefsKey = prefsKey;
        }

        public void Save(object obj)
        {
            string data = JsonConvert.SerializeObject(obj,
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                }
            );
            PlayerPrefs.SetString(_prefsKey, data);
            PlayerPrefs.Save();
        }

        public bool Load<T>(object destination)
        {
            if (destination.GetType() != typeof(T))
                throw new TypeAccessException("Invalid type for deserialization");

            if (TryLoad(out T loaded) == false)
                return false;

            CopyProperties(destination, loaded);

            return true;
        }

        public bool TryLoad<T>(out T result)
        {
            result = default;

            string rawData = PlayerPrefs.GetString(_prefsKey);

            if (rawData == null)
                return false;

            try
            {
                result = JsonConvert.DeserializeObject<T>(rawData,
                    new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented,
                    }
                );

                return result != null;
            }
            catch (JsonSerializationException)
            {
                return false;
            }
        }

        private void CopyProperties<T>(T destination, T reference)
        {
            Type type = destination.GetType();

            foreach (PropertyInfo property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (property.HasAttribute(typeof(JsonPropertyAttribute)))
                    property.SetValue(destination, property.GetValue(reference));
            }
        }
    }
}