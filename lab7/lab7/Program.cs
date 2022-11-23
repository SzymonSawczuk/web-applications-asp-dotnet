using System;
using LinqExamples;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;

//Szymon Sawczuk 260287

namespace lab7
{

    //Zad 3
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Topic(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class Student
    {
        //public static List<Topic> TopicStatic = new List<Topic>()
        //{
        //    new Topic(1, "C#"),
        //    new Topic(2, "Fuzzy logic"),
        //    new Topic(3, "C++"),
        //    new Topic(4, "PHP"),
        //    new Topic(5, "Neural networks"),
        //    new Topic(6, "JavaScript"),
        //    new Topic(7, "Basic"),
        //    new Topic(8, "Java"),
        //    new Topic(9, "Algorithms"),
        //    new Topic(10, "Web programming"),

        //};

        public static List<Topic> TopicStatic = (from stud in Generator.GenerateStudentsWithTopicsEasy()
                                                 from topic in stud.Topics
                                                 group topic by topic into topicGroup
                                                 select topicGroup.Key).Select((topic, index) => new Topic(index + 1, topic)).ToList();

        public int Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public bool Active { get; set; }
        public int DepartmentId { get; set; }

        public List<int> Topics { get; set; }
        public Student(int id, int index, string name, Gender gender, bool active,
            int departmentId, List<int> topics)
        {
            this.Id = id;
            this.Index = index;
            this.Name = name;
            this.Gender = gender;
            this.Active = active;
            this.DepartmentId = departmentId;
            this.Topics = topics;
        }

        //public Student()
        //{
        //    this.Id = 99;
        //    this.Index = 260287;
        //    this.Name = "Sawczuk";
        //    this.Gender = Gender.Male;
        //    this.Active = true;
        //    this.DepartmentId = 99;
        //    this.Topics = new List<int> { 4, 2 };
        //}

        public (string, int) AddSecondNameToNameAndChangeIndexByAge(string secondName, int age)
        {
            return (this.Name + " " + secondName, this.Index + age);
        }

        public override string ToString()
        {
            var result = $"{Id,2}) {Index,5}, {Name,11}, {Gender,6},{(Active ? "active" : "no active"),9},{DepartmentId,2}, topics: ";
            foreach (var str in Topics)
                result += str + "(" + TopicStatic.Find(topic => topic.Id == str).Name + "), ";
            return result;
        }
    }

    public class Student2
    {
        
        public int Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public bool Active { get; set; }
        public int DepartmentId { get; set; }

        public Student2(int id, int index, string name, Gender gender, bool active,
            int departmentId)
        {
            this.Id = id;
            this.Index = index;
            this.Name = name;
            this.Gender = gender;
            this.Active = active;
            this.DepartmentId = departmentId;
        }

        public override string ToString()
        {
            var result = $"{Id,2}) {Index,5}, {Name,11}, {Gender,6},{(Active ? "active" : "no active"),9},{DepartmentId,2}";
            return result;
        }
    }

    public class StudentToTopic
    {
        public static List<Topic> TopicStatic = (from stud in Generator.GenerateStudentsWithTopicsEasy()
                                                 from topic in stud.Topics
                                                 group topic by topic into topicGroup
                                                 select topicGroup.Key).Select((topic, index) => new Topic(index + 1, topic)).ToList();

        public int IdS { get; set; }
        public int IdT { get; set; }

        public StudentToTopic(int idS, int idT)
        {
            this.IdS = idS;
            this.IdT = idT;
        }
    }

    class Program
    {

        //Zad 1
        public static IEnumerable<IGrouping<int, StudentWithTopics>> GroupByNameAndIndexN(int n)
        {
            var index = 0;
            return from stud in Generator.GenerateStudentsWithTopicsEasy()
                   orderby stud.Name, stud.Index
                   group stud by index++ / n into studGroup
                   select studGroup;

        }

        public static void ShowGroupByNameAndIndexN(int n)
        {
            var students = GroupByNameAndIndexN(n);

            foreach(var student in students)
            {
                Console.WriteLine(student.Key);

                student.ToList().ForEach(stud => Console.WriteLine(" " + stud));
            }
        }

        //Zad 2a
        public static IEnumerable<dynamic> SortTopicsByFreq()
        {
            return (from topic in Generator.GenerateStudentsWithTopicsEasy()
                   .SelectMany(stud => stud.Topics)
                    group topic by topic into topicGroup
                    select new
                    {
                        Topic = topicGroup.Key,
                        Counter = topicGroup.Count()
                    }).OrderByDescending(result => result.Counter).ThenBy(result => result.Topic);
        }

        //zad 2b
        public static IEnumerable<dynamic> SortTopicsByFreqGender()
        {
            return from student in Generator.GenerateStudentsWithTopicsEasy()
                   group student by student.Gender into genderGroup
                   select new
                   {
                       GenderTopic = genderGroup.Key,
                       Topics = (from topic in genderGroup.SelectMany(stud => stud.Topics)
                                group topic by topic into topicGroup
                                select new
                                {
                                    Topic = topicGroup.Key,
                                    Counter = topicGroup.Count()
                                }).OrderByDescending(result => result.Counter).ThenBy(result => result.Topic)
                                
                    };
        }

        public static void ShowSortTopicsByFreq()
        {
            var topics = SortTopicsByFreq();

            foreach (var topic in topics)
            {
                Console.WriteLine(topic.Counter + " " + topic.Topic);
            }
        }

        public static void ShowSortTopicsByFreqGender()
        {
            var topics = SortTopicsByFreqGender();

            foreach (var topic in topics)
            {
                Console.WriteLine(topic.GenderTopic);
                foreach(var topic_inner in topic.Topics)
                {
                    Console.WriteLine(" " + topic_inner.Counter + " " + topic_inner.Topic);
                }
            }
        }

        //zad3
        public static List<Student> GenerateStudents()
        {
            return (from stud in Generator.GenerateStudentsWithTopicsEasy()
                    select new Student(stud.Id,
                                        stud.Index,
                                        stud.Name,
                                        stud.Gender,
                                        stud.Active,
                                        stud.DepartmentId,
                                        (from topic in Student.TopicStatic
                                         from topic_old in stud.Topics
                                         where topic.Name == topic_old
                                         select topic.Id).ToList()
                                       )).ToList();  
        }

        public static List<StudentToTopic> GenerateStudentsToTopic()
        { 
            return (from stud in Generator.GenerateStudentsWithTopicsEasy()
                    from topic in stud.Topics
                    from topic_new in StudentToTopic.TopicStatic
                    where topic_new.Name == topic
                    select new StudentToTopic(stud.Id, topic_new.Id)).ToList();
        }

        public static void zad3_3()
        {
            var students = (from stud in Generator.GenerateStudentsWithTopicsEasy()
                            select new Student2(stud.Id,
                                                stud.Index,
                                                stud.Name,
                                                stud.Gender,
                                                stud.Active,
                                                stud.DepartmentId
                                               )).ToList();

            var topics = GenerateStudentsToTopic();

            var topicsToWrite = from stud in students
                                from topic in topics
                                where stud.Id == topic.IdS
                                group topic by new { topic.IdS, stud } into topicGroup
                                select new { StudentElem = topicGroup.Key.stud, TopicElem = topicGroup };

            topicsToWrite.ToList().ForEach(elem => {
                Console.Write(elem.StudentElem + ", topics: ");
                elem.TopicElem.ToList().ForEach(
                    elem_inner => Console.Write(elem_inner.IdT + "(" +
                    StudentToTopic.TopicStatic.Find(elem_topic => elem_topic.Id == elem_inner.IdT)
                    + ")"+ ", "));
                Console.WriteLine(); });
        }

        static void Main(string[] args)
        {

            //ShowGroupByNameAndIndexN(3);
            //ShowGroupByNameAndIndexN(5);
            //ShowSortTopicsByFreq();
            //ShowSortTopicsByFreqGender();

            //var students = GenerateStudents();

            //foreach (var student in students)
            //{
            //    Console.WriteLine(student);
            //}

            //zad3_3();

            //zad4


            //Student student = (Student)Activator.CreateInstance(typeof(Student), new object[] { 99, 260287, "Sawczuk", Gender.Male, true, 12, new List<int> { 1, 4 } });

            //Student student = new Student(99, 260287, "Sawczuk", Gender.Male, true, 12, new List<int> { 1, 4 });

            Student student = (Student)typeof(Student).Assembly.CreateInstance("lab7.Student", false, BindingFlags.CreateInstance,
                                null, new object[] { 99, 260287, "Sawczuk", Gender.Male, true, 12, new List<int> { 1, 4 } }, null, null);

            MethodInfo methodInfo = student.GetType().GetMethod("AddSecondNameToNameAndChangeIndexByAge",
                new Type[] { typeof(string), typeof(int) });

   
            (string, int) result = ((string, int))methodInfo.Invoke(student, new object[] { "Szymon", 20 });
            Console.WriteLine($"Result = {result}");

        }
    }
    
}

