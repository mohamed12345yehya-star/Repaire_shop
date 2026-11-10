using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shhop
{
    public class Person
    {
        // ==========================================
       // 1. Base Class for all people (Inheritance)
       // ==========================================

        private string first_name;
        private string last_name;
        private string phone_number;

        public string FirstName
        {
            get { return first_name; }
            set { first_name = value; }
        }

        public string LastName
        {
            get { return last_name; }
            set { last_name = value; }
        }

        public string PhoneNumber
        {
            get { return phone_number; }
            set { phone_number = value; }
        }

        public Person(string first_name, string last_name, string phone_number)
        {
            this.first_name = first_name;
            this.last_name = last_name;
            this.phone_number = phone_number;
        }
    }

    // ==========================================
    // 2. Subclasses for Persons
    // ==========================================
    public class Client : Person
    {
        private int client_id;
        private string comment;

        public int ClientId { get { return client_id; } }
        public string Comment { get { return comment; } set { comment = value; } }

        public Client(int client_id, string first_name, string last_name, string phone_number)
            : base(first_name, last_name, phone_number)
        {
            this.client_id = client_id;
            this.comment = "No comments";
        }
    }   
}
