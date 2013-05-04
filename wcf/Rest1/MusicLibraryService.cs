using System.Collections.Generic;
using System.ServiceModel;

namespace Rest1
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    class MusicLibraryService : IMusicLibraryService
    {
        /// <summary>
        /// Map from artist, album and track to composer list
        /// </summary>
        private readonly Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> m_Db;

        public MusicLibraryService()
        {
            m_Db = new Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>>();
            var miles = new Dictionary<string, Dictionary<string, List<string>>>();
            m_Db.Add("MilesDavis", miles);

            var tutu = new Dictionary<string, List<string>>();
            miles.Add("Tutu", tutu);

            tutu.Add("Tutu", new List<string>{ "Marcus Miller" });
            tutu.Add("Tomaas", new List<string> { "Miles Davis", "Marcus Miller" });

        }

        public string Version()
        {
            return "1.0";
        }

        public string[] GetComposers(string artist, string album, string track)
        {
            if (! m_Db.ContainsKey(artist)) return null;

            var ar = m_Db[artist];

            if (!ar.ContainsKey(album)) return null;

            var al = ar[album];

            if (!al.ContainsKey(track)) return null;

            return al[track].ToArray();
        }
    }
}
