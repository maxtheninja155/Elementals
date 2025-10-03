using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public static class ConfigParser 
{
   public static HashSet<KeyValuePair<string, string>> GetSectionData(string filePath, string sectionName)
    {
        HashSet<KeyValuePair<string, string>> data = new HashSet<KeyValuePair<string, string>>();

        FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        StreamReader reader = new StreamReader(stream);

        string line;
        while ((line = reader.ReadLine()) != null)
        {
            if (line.Equals('[' + sectionName + ']'))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("="))
                    {
                        string keyWithWhiteSpace = line.Substring(0, line.IndexOf('='));
                        string valueWithWhiteSpace = line.Substring(line.IndexOf('=') + 1);

                        string key = string.Concat(keyWithWhiteSpace.Where(c => !char.IsWhiteSpace(c)));
                        string value = string.Concat(valueWithWhiteSpace.Where(c => !char.IsWhiteSpace(c)));
                        Debug.Log(key + " " + value);
                        data.Add(new KeyValuePair<string, string>(key, value));
                    }
                    else if (line.ToCharArray().Length > 0 && line.ToCharArray()[0] == '[')
                        break;
                }
            }
        }

        reader.Close();
        stream.Close();
        return data; 
    }
}
