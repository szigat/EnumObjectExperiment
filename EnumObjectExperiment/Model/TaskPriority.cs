using System;
using System.Collections.Generic;
using System.Text;

namespace EnumObjectExperiment.Model
{
    public class TaskPriority : Enumeration
    {
        public override EnumerationType TypeId { get => EnumerationType.TaskPriority; protected set => throw new NotImplementedException(); }
        //public const EnumerationType TypeId = EnumerationType.TaskPriority;

        public static readonly TaskPriority Normal
            = new TaskPriority(2.1, "Normal", "Normális");

        public static readonly TaskPriority Low
            = new TaskPriority(2.2, "Low", "Alacsony");

        public static readonly TaskPriority High
            = new TaskPriority(2.3, "High", "Magas");

        protected TaskPriority()
        {
            
        }

        private TaskPriority(double value, string systemName, string displayName)
            : base(value, systemName, displayName)
        {
           
        }

    }
}
