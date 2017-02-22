using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registryry
{
   public class Student : User
    {
        public string major;
        public bool isFulltime;
        // Dictionary<string, Course> classes = new Dictionary<string, Course>();
        //List<Course> Classes = new List<Course>();
        // array of students' courses

        /// <summary>
        /// </summary>
        /// <param name="fname">Students First Name</param>
        /// <param name="lname">Students Last Name</param>
        /// <param name="password">Students Password</param>
        /// <param name="email">Students Email</param>
        /// <param name="id">Students Special ID</param>
        /// <param name="major">Students Major</param>

        public Student(string fname, string lname, string password, 
            string email, int id, string major = "undecided") : base(fname, lname, password, email, id)
        {
            this.major = major;
            isFulltime = false;
        }
        
        public void defineTimeStatus(Student student)
        {
            
        }
        /*public override string getInfo()
        {
            StringBuilder info = new StringBuilder(base.ToString());
            info.Append($"\n{major}");
            info.Append($"\nfulltime: {isFulltime}");

            if (classes.Count == 0)
            {
                info.Append($"Not registered for classes");
            }
            else
            {
                foreach (KeyValuePair<string, Course> x in classes)
                {
                    info.Append("\n");
                    info.Append(x.Value.name);
                }
            }
            return info.ToString();
        }*/
    }
}

