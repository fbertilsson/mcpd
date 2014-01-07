using System;
using System.Collections.Generic;

namespace HistoricEntitiesCodeFirst
{
    public class HistoricEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Tag> Tags { get; set; }
        public TimeRef TimeReference { get; set; }
    }

    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Tag Parent { get; set; }
        public List<Tag> Children { get; set; }
        public List<HistoricEvent> HistoricEvents { get; set; } 
        public override string ToString()
        {
            return Name;
        }
    }

    public abstract class TimeRef
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
    }

    //public class PointInTime : TimeRef
    //{
    //}

    public class TimeRefSpan : TimeRef
    {
        public DateTime End { get; set; }
    }
}
