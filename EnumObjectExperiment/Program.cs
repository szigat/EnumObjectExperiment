using EnumObjectExperiment.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EnumObjectExperiment
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (ExperimentContext db = new ExperimentContext())
            {
                //db.Database.EnsureDeleted();
                //db.Database.EnsureCreated();

                AddOrUpdateEnumeration(db, TaskState.Created);
                AddOrUpdateEnumeration(db, TaskState.Done);
                AddOrUpdateEnumeration(db, TaskState.InProgress);

                AddOrUpdateEnumeration(db, TaskPriority.Normal);
                AddOrUpdateEnumeration(db, TaskPriority.Low);
                AddOrUpdateEnumeration(db, TaskPriority.High);


                db.SaveChanges(SaveChangesMode.WithEnumeration);
            }
            using (ExperimentContext db = new ExperimentContext())
            {
                db.Tasks.Add(new Model.TaskItem()
                {
                    Title = "teszt4",
                    State = TaskState.Done,
                    Priority = TaskPriority.Normal
                });
                db.SaveChanges();


            }

            using (ExperimentContext db = new ExperimentContext())
            {
                var taskItems = db.Tasks.Take(100).ToList();
                FillEnumerations(taskItems);

                taskItems = db.Tasks.Take(100).ToList();
                FillEnumerations(taskItems);
            }

            Console.WriteLine("Hello World!");

            Console.ReadLine();
        }

        private static void FillEnumerations<T>(List<T> collection) where T : IEntity
        {
            foreach (T item in collection)
            {
                FillEnumerations(item);
            }
        }


        private static void FillEnumerations<T>(T entity) where T : IEntity
        {


            var sw = System.Diagnostics.Stopwatch.StartNew();



            var enumerations = typeof(T).GetProperties().Where(p => p.PropertyType.BaseType == typeof(Enumeration));


            foreach (var oneEnum in enumerations)
            {
                var idProperty = typeof(T).GetProperties().FirstOrDefault(p => p.Name == $"{oneEnum.Name}Id");
                double? id = null;
                if (Nullable.GetUnderlyingType(idProperty.PropertyType) != null)
                {
                    id = (double?)idProperty.GetValue(entity);
                }
                else
                {
                    id = (double)idProperty.GetValue(entity);
                }

                if (id != null)
                {
                    object value = null;

                    var props = oneEnum.PropertyType.GetProperties();
                    var fields = oneEnum.PropertyType.GetFields(BindingFlags.Public |
                                                                BindingFlags.Static |
                                                                BindingFlags.DeclaredOnly);

                    value = fields.Select(f => f.GetValue(entity)).Cast<Enumeration>()
                        .FirstOrDefault(x => x.Id == id);
                    oneEnum.SetValue(entity, value);
                }

            }


            sw.Stop();
            Console.WriteLine($"{sw.ElapsedMilliseconds} ms");
        }

        private static void AddOrUpdateEnumeration<T>(ExperimentContext db, T taskState) where T : Enumeration
        {
            if (db.Set<T>().Any(s => s.Id == taskState.Id))
            {
                db.Set<T>().Update(taskState);
            }
            else
            {
                db.Set<T>().Add(taskState);
            }
        }
    }
}