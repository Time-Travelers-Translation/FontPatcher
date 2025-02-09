using CrossCutting.Core.Contract.Configuration;
using CrossCutting.Core.Contract.Configuration.DataClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;


namespace CrossCutting.Core.Configuration.File
{
    public class FileConfigurationRepository : IConfigurationRepository
    {
        public IEnumerable<ConfigCategory> Load()
        {
            string cfgPath = GetConfigPath();
            if (!System.IO.File.Exists(cfgPath))
                yield break;

            string json = System.IO.File.ReadAllText(cfgPath);
            if (string.IsNullOrEmpty(json))
                yield break;

            var result = JsonSerializer.Deserialize<List<ConfigCategory>>(json);

            foreach (ConfigCategory category in result)
            {
                foreach (ConfigEntry entry in category.Entries)
                {
                    entry.Category = category;
                    entry.Value = ((JsonElement)entry.Value).ValueKind switch
                    {
                        JsonValueKind.String => ((JsonElement)entry.Value).GetString(),
                        JsonValueKind.Number => ((JsonElement)entry.Value).GetInt32(),
                        _ => null
                    };
                }

                yield return category;
            }
        }

        private string GetConfigPath()
        {
            return Path.Combine(Path.GetDirectoryName(Environment.ProcessPath)!, "config.json");
        }
    }
}
