using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shhop
{
    public class Technician : Person
    {
        private double salary;

        public double Salary { get { return salary; } set { salary = value; } }

        public Technician(string first_name, string last_name, string phone_number, double salary)
            : base(first_name, last_name, phone_number)
        {
            this.salary = salary;
        }
    }
}
