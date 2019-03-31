using System;
using System.Linq;
using System.Collections.Generic;
using TasksBoard.Models;

namespace TasksBoard.Heplers
{
    public class TasksTreeItem<TaskModel>
    {
        public TaskModel Item { get; set; }
        public IEnumerable<TasksTreeItem<TaskModel>> Children { get; set; }
    }

    internal static class GenericHelpers
    {
        public static IEnumerable<TasksTreeItem<TaskModel>> GenerateTree<TaskModel, K>(
            this IEnumerable<TaskModel> collection,
            Func<TaskModel, K> id_selector,
            Func<TaskModel, K> parent_id_selector,
            K root_id = default(K))
            {
                foreach (var c in collection.Where(c => parent_id_selector(c).Equals(root_id)))
                {
                    yield return new TasksTreeItem<TaskModel>
                    {
                        Item = c,
                        Children = collection.GenerateTree(id_selector, parent_id_selector, id_selector(c))
                    };
                }
            }
    }
}