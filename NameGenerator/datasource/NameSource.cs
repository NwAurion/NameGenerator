using System.Collections.Generic;
using NameGenerator.model;

namespace NameGenerator.datasource
{
    interface NameSource
    {
        Dictionary<string, string> LoadLanguages();

        void LoadFirstNames();
        void LoadLastNames();

        HashSet<NameObject> GetFirstNames();
        HashSet<NameObject> GetLastNames();
    }
}
