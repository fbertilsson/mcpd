using System.Runtime.Serialization;

namespace GenericMessageHandling
{
    [DataContract]
    class Comedian
    {
        [DataMember]
        private readonly string m_Name;

        [DataMember]
        private readonly string m_HumorCategory;

        public Comedian(string name, string humorCategory)
        {
            m_Name = name;
            m_HumorCategory = humorCategory;
        }

        public override string ToString()
        {
            return string.Format("[Comedian, name: {0} humorCategory: {1}", m_Name, m_HumorCategory);
        }
    }
}
