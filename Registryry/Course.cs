using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Registryry
{
    public class Course
    {

        /// <summary>
        /// field values for Courses
        /// 
        /// </summary>
        public int CourseId;
        public string name;
        private List<Student> studentRoster = new List<Student>();
        
        public DateTime startTime;
        public DateTime creditHour;
        public delegate bool CloseRegistration(Course thisCourseToClose);

        public CloseRegistration closeReg = null;
        public bool isClosed = false;

        
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Course()
        {

        }

        public Course(string name, DateTime timeOfDay, DateTime creditHour)
        {
            this.name = name;
            this.creditHour = creditHour;
            this.startTime = timeOfDay;
        }

        public bool isFull
        {
            get
            {
                return studentRoster.Count == Global.maxStudents;
            }

        }

        public int rosterCount
        {
            get
            {
                return studentRoster.Count;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                //name = value;
            }
        }

        public bool addStudent(Student student)
        {
            SpaceCheck(studentRoster.Count + 1);
            studentRoster.Add(student);
            if (closeReg != null && isFull)
            {
                closeReg(this);
            }
            return true;
        }

        public bool AddStudents(List<Student> roster)
        {
            SpaceCheck(roster.Count + studentRoster.Count);

            foreach (Student item in roster)
            {
                addStudent(item);
            }
            return true;
        }

        public IEnumerable<Student> GetIenumerableStudentList()
        {
            return studentRoster;
        }
        public Task<List<Student>> FetchRoster()
        {
            return Task.Run(() => { return studentRoster; });
        }

        public Student GetStudentById(int id)
        {

            var student = studentRoster.Where(x => x.Id == id).FirstOrDefault();
            return student;
        }

        /*[Obsolete("method is for LINQ demo")] // Example of a directive
        public List<Student> GetStudentByFirstName(string fn)
        {
            var student = studentRoster.Where(person => person.fname == fn && person.lname == "a").ToList();
            return student;
        }
        */
        public Student GetStudentByWholeName(string name)
        {
            var result = studentRoster.Where(p => p.wholeName == name).FirstOrDefault();
            return result;
        }

        public Student GetStudentByWholeName(string firstname, string lastname)
        {
            return GetStudentByWholeName($"{firstname} {lastname}");
        }

        public async Task<List<Student>> GetStudentRoster()
        {
            Console.WriteLine("Start Async");
            var results = await FetchRoster();
            Console.WriteLine("End Async");
            return studentRoster;
        }

        public void printRosterCount()
        {
            Thread.Sleep(1000);
            Console.WriteLine("num of students " + studentRoster.Count);
        }

        public bool removeStudent(int id)
        {
            return studentRoster.Remove(GetStudentById(id));
        }

        public bool removeStudent(Student student)
        {
            bool success = studentRoster.Remove(student);
            if (success && (closeReg != null) && (studentRoster.Count == 19))
            {
                closeReg(this);
            }
            return success;
        }

        private bool SpaceCheck(int newCount)
        {
            if (newCount > Global.maxStudents)
            {
                throw new Exception(Errors.notEnoughSpace);
            }
            return true;
        }
    }
}
