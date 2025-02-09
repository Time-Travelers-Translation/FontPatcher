using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CrossCutting.Core.Contract.Configuration.DataClasses
{
    public class ConfigCategory
    {
        public string Name { get; set; }
        [JsonInclude]
        public List<ConfigEntry> Entries { get; private set; }

        public ConfigCategory()
        {
            Entries = new List<ConfigEntry>();
        }

        public ConfigEntry AddEntry(string key, object value)
        {
            return AddEntry(key, value, false);
        }

        public ConfigEntry AddEntry(string key, object value, bool persist)
        {
            ConfigEntry result = new ConfigEntry(this)
            {
                Key = key,
                Value = value,
                Persist = persist
            };

            Entries.Add(result);

            return result;
        }
    }
}
