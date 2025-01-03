using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4Docker
{
    
   
        public class Task
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string Status { get; private set; }
            public TeamMember Assignee { get; private set; }

            public Task(string title, string description)
            {
                Title = title ?? throw new ArgumentNullException(nameof(title));
                Description = description ?? throw new ArgumentNullException(nameof(description));
                Status = "Очікує";
            }

            public void UpdateStatus(string newStatus)
            {
                var validStatuses = new[] { "Очікує", "Виконується", "Завершено" };
                if (!validStatuses.Contains(newStatus))
                    throw new ArgumentException("Invalid status.");
                Status = newStatus;
            }

            public void AssignTo(TeamMember member)
            {
                Assignee = member ?? throw new ArgumentNullException(nameof(member));
            }
        
    }
}
