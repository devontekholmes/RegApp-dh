using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registryry
{
    public interface ICourse
    {
        
        int rosterCount { get; }
        bool addStudent(Student student);
        bool AddStudents(List<Student> roster);
        bool removeStudent(int id);
        bool removeStudent(Student student);
        //bool removeStudent(string fname, string lname);
        bool isFull { get; }
    }
}
