using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shhop
{
    public class AirConditioner : Appliance
    {
        // ==========================================
        // 4. Subclasses for Appliances (Polymorphism)
        // ==========================================

        private double cleaning_labor_cost;

        public AirConditioner(string serial_number, string brand, double cleaning_labor_cost)
            : base(serial_number, brand)
        {
            this.cleaning_labor_cost = cleaning_labor_cost;
        }

        public override double CalculateRepairCost()
        {
            double fixed_fee = 30000;
            return fixed_fee + cleaning_labor_cost;
        }
    }
}
