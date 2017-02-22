using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registryry
{
   public class User
    {
        /// <summary>
        ///Field values for first name, last name, password, email, and id 
        /// </summary>
            private string fname;
            protected string password;
            private string email;
            private int id;
            private string lname;

            /// <summary>
            /// default constructor
            /// </summary>
            public User()
            {


            }

            /// <summary>
            /// When the values are given Constructor
            /// </summary>
            /// <param name="name">person's name</param>
            /// <param name="password">user's password</param>
            /// <param name="email">user's email</param>
            /// <param name="id">user id</param>
            public User(string fname, string lname, string password, string email, int id)
            {
                this.fname = fname;
                this.lname = lname;
                this.password = password;
                this.email = email;
                this.id = id;
            }

            public string wholeName
            {
                get { return $"{fname} {lname}"; }
            }
            public virtual string Password
            {
                get { return password; }

            }

            public string Email
            {
                get { return email; }
                set { email = value; }
            }

            public int Id
            {
                get { return id; }
            }

            public override string ToString()
            {
                return $" {wholeName} \n {Email}";
            }


        }


  }

