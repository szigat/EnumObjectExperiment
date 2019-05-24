using System;
using System.Collections.Generic;
using System.Text;

namespace EnumObjectExperiment.Model
{
    public interface IEntity { }

    public class TaskItem : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public double StateId { get; set; }
        public TaskState State { get; set; }
        public double PriorityId { get; set; }
        public TaskPriority Priority { get; set; }
    }
}
