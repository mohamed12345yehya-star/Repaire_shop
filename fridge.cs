using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shhop
{
    public class Fridge : Appliance
    {
        // ==========================================
        // 4. Subclasses for Appliances (Polymorphism)
        // ==========================================
        private double freon_cost;

        public Fridge(string serial_number, string brand, double freon_cost)
            : base(serial_number, brand)
        {
            this.freon_cost = freon_cost;
        }

        public override double CalculateRepairCost()
        {
            double fixed_fee = 20000;
            return fixed_fee + freon_cost;
        }
    }
}
