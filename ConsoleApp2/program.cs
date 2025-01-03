using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab4Docker;
using Task = lab4Docker.Task;

namespace ConsoleApp2
{
    public class Program
    {
        private static List<TeamMember> teamMembers = new();
        private static List<Task> tasks = new();

        public static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n=== Team Task Manager ===");
                Console.WriteLine("1. Add Team Member");
                Console.WriteLine("2. Remove Team Member");
                Console.WriteLine("3. View All Team Members");
                Console.WriteLine("4. Add Task");
                Console.WriteLine("5. Assign Task to Member");
                Console.WriteLine("6. Update Task Status");
                Console.WriteLine("7. View All Tasks");
                Console.WriteLine("8. View Tasks by Status");
                Console.WriteLine("9. Exit");
                Console.Write("Choose an option: ");
            
                switch (Console.ReadLine())
                {
                    case "1": AddTeamMember(); break;
                    case "2": RemoveTeamMember(); break;
                    case "3": ViewAllTeamMembers(); break;
                    case "4": AddTask(); break;
                    case "5": AssignTask(); break;
                    case "6": UpdateTaskStatus(); break;
                    case "7": ViewAllTasks(); break;
                    case "8": ViewTasksByStatus(); break;
                    case "9": return;
                    default: Console.WriteLine("Invalid option."); break;
                }
            }
           
        }

        private static void AddTeamMember()
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            int id = teamMembers.Count + 1;
            teamMembers.Add(new TeamMember(name, id));
            Console.WriteLine($"Team Member '{name}' added with ID {id}.");
        }

        private static void RemoveTeamMember()
        {
            Console.Write("Enter Member ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var member = teamMembers.FirstOrDefault(m => m.Id == id);
                if (member != null)
                {
                    teamMembers.Remove(member);
                    Console.WriteLine($"Member {member.Name} removed.");
                }
                else
                {
                    Console.WriteLine("Member not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }

        private static void ViewAllTeamMembers()
        {
            Console.WriteLine("\n=== Team Members ===");
            foreach (var member in teamMembers)
            {
                Console.WriteLine($"- {member.Id}: {member.Name}");
            }
        }

        private static void AddTask()
        {
            Console.Write("Enter Task Title: ");
            string title = Console.ReadLine();
            Console.Write("Enter Task Description: ");
            string description = Console.ReadLine();
            tasks.Add(new Task(title, description));
            Console.WriteLine($"Task '{title}' added.");
        }

        private static void AssignTask()
        {
            Console.Write("Enter Task Title: ");
            string title = Console.ReadLine();
            var task = tasks.FirstOrDefault(t => t.Title == title);
            if (task == null)
            {
                Console.WriteLine("Task not found.");
                return;
            }

            Console.Write("Enter Member ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var member = teamMembers.FirstOrDefault(m => m.Id == id);
                if (member != null)
                {
                    task.AssignTo(member);
                    Console.WriteLine($"Task '{task.Title}' assigned to {member.Name}.");
                }
                else
                {
                    Console.WriteLine("Member not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }

        private static void UpdateTaskStatus()
        {
            Console.Write("Enter Task Title: ");
            string title = Console.ReadLine();
            var task = tasks.FirstOrDefault(t => t.Title == title);
            if (task == null)
            {
                Console.WriteLine("Task not found.");
                return;
            }

            Console.Write("Enter New Status (Очікує, Виконується, Завершено): ");
            string status = Console.ReadLine();
            try
            {
                task.UpdateStatus(status);
                Console.WriteLine($"Task '{task.Title}' updated to '{status}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void ViewAllTasks()
        {
            Console.WriteLine("\n=== All Tasks ===");
            foreach (var task in tasks)
            {
                Console.WriteLine($"- {task.Title}: {task.Description} | Status: {task.Status} | Assignee: {task.Assignee?.Name ?? "None"}");
            }
        }

        private static void ViewTasksByStatus()
        {
            Console.Write("Enter Status (Очікує, Виконується, Завершено): ");
            string status = Console.ReadLine();
            var filteredTasks = tasks.Where(t => t.Status == status).ToList();

            if (filteredTasks.Count == 0)
            {
                Console.WriteLine("No tasks found with this status.");
                return;
            }

            Console.WriteLine($"\n=== Tasks with Status '{status}' ===");
            foreach (var task in filteredTasks)
            {
                Console.WriteLine($"- {task.Title}: {task.Description} | Assignee: {task.Assignee?.Name ?? "None"}");
            }
        }
    }
}
