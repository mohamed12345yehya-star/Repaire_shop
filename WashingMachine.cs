using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shhop
{
    public class WashingMachine : Appliance
    {
        // ==========================================
        // 4. Subclasses for Appliances (Polymorphism)
        // ==========================================
        private double spare_parts_cost;

        public WashingMachine(string serial_number, string brand, double spare_parts_cost)
            : base(serial_number, brand)
        {
            this.spare_parts_cost = spare_parts_cost;
        }

        public override double CalculateRepairCost()
        {
            double fixed_fee = 25000;
            return fixed_fee + spare_parts_cost;
        }
    }
}
