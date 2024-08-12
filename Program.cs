using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;


namespace ConsoleAppDZ11
{
    public class Program
    {
        static void Main(string[] args)
        {
            
            TaskManager taskManager = new TaskManager();
            taskManager.AddTask(new Task
            {Id = 00001,Title = "дом",Description = "Сходить за покупками",DueDate = new DateTime (2024,09,07),
                Priority = "высокий"});
            taskManager.AddTask(new Task
            {Id = 00002, Title = "дом",Description = "Выбросить мусор", DueDate = new DateTime(2024, 08, 07),
                Priority = "средний"});
            taskManager.AddTask(new Task
            {Id = 00003, Title = "работа", Description = "Написать заявление", DueDate = new DateTime(2024, 09, 15),
                Priority = "средний"});
            taskManager.AddTask(new Task
            { Id = 00004, Title = "банк", Description = "Закрыть кредит", DueDate = new DateTime(2024, 08, 25),
                Priority = "высокий"
            });
            taskManager.AddTask(new Task
            { Id = 00005, Title = "дом", Description = "уборка",  DueDate = new DateTime(2024, 08, 15),
                Priority = "средний"
            });
            taskManager.AddTask(new Task
            { Id = 00006,Title = "работа", Description = "Подписать документ", DueDate = new DateTime(2024, 09, 28),
                Priority = "низкий"
            });
            taskManager.AddTask(new Task
            {  Id = 00007,Title = "дом", Description = "Сходить в домоуправление", DueDate = new DateTime(2024, 09, 21),
                Priority = "низкий"
            });

            taskManager.SaveToCsv("Task.csv"); // / Сохранение в csv

            taskManager.SaveToJson("Task.json"); // Сохранение в json
            Console.WriteLine("Загрузка задач из json файла");
            taskManager.LoadFromJson("Task.json"); // Загрузка задач из json файла
            foreach (var emp in taskManager.list)
            {
                Console.WriteLine($"ID: {emp.Id}, Title: {emp.Title}, Description: {emp.Description}, DueDate: {emp.DueDate.ToString("D")}, Priority: {emp.Priority}");
            }
            taskManager.UpdateTask(new Task {Id = 00007, Title = "дом", // Изменение задачи
                Description = "стирка",
                DueDate = new DateTime(2024, 08, 19),
                Priority = "высокий"
            });

            taskManager.SaveToJson("Task.json"); // Сохранение в json c измененной задачей
            Console.WriteLine("Загрузка задач из json файла с измененной задачей");
            taskManager.LoadFromJson("Task.json"); // Загрузка задач из json файла
            foreach (var emp in taskManager.list)
            {
                Console.WriteLine($"ID: {emp.Id}, Title: {emp.Title}, Description: {emp.Description}, DueDate: {emp.DueDate.ToString("D")}, Priority: {emp.Priority}");
            }


            taskManager.SaveToXML("Task.xml"); // Сохранение в xml
            Console.WriteLine("Загрузка задач из xml файла");
            taskManager.LoadFromXML("Task.xml"); // Загрузка задач из xml файла
            foreach (var emp in taskManager.list)
            {
                Console.WriteLine($"ID: {emp.Id}, Title: {emp.Title}, Description: {emp.Description}, DueDate: {emp.DueDate.ToString("D")}, Priority: {emp.Priority}");
            }

           
            Console.WriteLine($"Сортировка по дате");
            IEnumerable <Task> sortedTasks = taskManager.FiltrSortTaskByDate();
            foreach (var emp in sortedTasks)
            {
                Console.WriteLine($"Дата: {emp.DueDate.ToString("D")}, ID:{emp.Id}, Title: {emp.Title}, Description: {emp.Description}, Priority: {emp.Priority}");
            }

            Console.WriteLine($"Фильтрация по приоритету");
            IEnumerable<Task> filtrTask = taskManager.FiltrTasksByPriority("высокий");
            foreach (var emp in filtrTask)
            {
                Console.WriteLine($"Priority: {emp.Priority}, Дата: {emp.DueDate.ToString("D")}, ID:{emp.Id}, Title: {emp.Title}, Description: {emp.Description}");
            }

            Console.WriteLine($"Группировка по приоритету");
            taskManager.GruppTasksByPriority(); // Группировка по приоритету
          
            Console.WriteLine($"Фильтрация по срокам");
            Console.WriteLine($"Введите начало периода (в формате год,месяц,день)->");
            var dt1 = DateTime.Parse(Console.ReadLine());
            Console.WriteLine($"Введите конец периода (в формате год,месяц,день)->");
            var dt2 = DateTime.Parse(Console.ReadLine());
            IEnumerable<Task> filtrTasks = taskManager.FiltrTasksByTime(dt1,dt2);
            foreach (var emp in filtrTasks)
            {
                Console.WriteLine($"Дата: {emp.DueDate.ToString("D")}, ID:{emp.Id}, Title: {emp.Title}, Description: {emp.Description}, Priority: {emp.Priority}");
            }
        }
    }
}
