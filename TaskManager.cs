using CsvHelper;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;



namespace ConsoleAppDZ11
{
    public class TaskManager
    {
        public List<Task> list = new List<Task> { };


        public void AddTask(Task task) { list.Add(task); }
        public void RemoveTask(long id)
        {
            list.RemoveAll(p => p.Id == id); // Удаляем задачу с данным Id
        }

        public void UpdateTask(Task task)
        {
            var existing = list.FirstOrDefault(p => p.Id == task.Id);
            if (existing != null)
            {
                existing.Title = task.Title;
                existing.Description = task.Description;
                existing.DueDate = task.DueDate;
                existing.Priority = task.Priority;
            }
        }

        public void SaveToCsv(string path)// Сохранение в csv
        {
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ru-RU");
            using var writer = new StreamWriter(path); 
            using var csv = new CsvWriter(writer, ci);
            csv.WriteHeader  <Task>();
            csv.NextRecord();
            foreach (var employee in list)
            {
                csv.WriteRecord(employee);
                csv.NextRecord();
            }
        }
        public void SaveToJson(string path) // Метод сохранения в файл json
        {
            string json = JsonConvert.SerializeObject(list);
            File.WriteAllText(path, json);
        }
        public void LoadFromJson(string path) // Возвращаем список, которые выпишем из нашего файла json
        {
            string json = File.ReadAllText(path);
            list = JsonConvert.DeserializeObject<List<Task>>(json);
        }
        public void SaveToXML(string path) // метод для сохранения в xml
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(list.GetType());
                using (StreamWriter sw = new StreamWriter(path))
                {
                    serializer.Serialize(sw, list);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
   
           
        public void LoadFromXML(string path)
        {
            var culture = System.Globalization.CultureInfo.InvariantCulture;
            XDocument xml = XDocument.Load(path);
            list = xml.Descendants("Task")
                .Select(p => new Task
                {
                    Id = long.Parse(p.Element("Id").Value),
                    Title = p.Element("Title").Value,
                    Description = p.Element("Description").Value,
                    DueDate = DateTime.Parse(p.Element("DueDate").Value, culture),
                    Priority = p.Element("Priority").Value
                }).ToList();
        }

        public IEnumerable<Task> FiltrTasksByPriority(string priority) 
        {
            return list.Where(p => p.Priority.ToLower() == priority.ToLower());
        }
        public IEnumerable<Task> FiltrTasksByTime(DateTime time_one, DateTime time_two) // Фильтрация по сроку
        {
            return list.Where(p => p.DueDate > time_one && p.DueDate < time_two);
        }
        public IEnumerable<Task> FiltrSortTaskByDate() // Сортировка по дате
        {
            return list.OrderBy(p => p.DueDate);
        }
        public void GruppTasksByPriority() // Группировка по приоритету
        {
            var task = list.GroupBy(p => p.Priority);
            foreach (var emp in task)
            {
                Console.WriteLine ($"Приоритет: {emp.Key}");

                foreach (var task_one in emp)
                {
                    Console.WriteLine($"Дата: {task_one.DueDate.ToString("D")}, ID:{task_one.Id}, Title: {task_one.Title}, Description: {task_one.Description}");
                }
                Console.WriteLine(); 
            }
        }

    }  
}
   

    

