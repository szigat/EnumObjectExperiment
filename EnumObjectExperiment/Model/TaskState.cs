namespace EnumObjectExperiment.Model
{
    public class TaskState : Enumeration
    {
        public override EnumerationType TypeId { get => EnumerationType.TaskState; protected set => throw new System.NotImplementedException(); }

        public static readonly TaskState Created
            = new TaskState(1.1, "Created", "Létrehozva");

        public static readonly TaskState InProgress
            = new TaskState(1.2, "InProgress", "Folyamatban");

        public static readonly TaskState Done
            = new TaskState(1.3, "Done", "Kész");

        protected TaskState()
        {
            
        }

        private TaskState(double value, string systemName, string displayName)
            : base(value, systemName, displayName)
        {
           
        }

    }
}