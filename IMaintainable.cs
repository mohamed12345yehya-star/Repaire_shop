using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shhop
{

    // ==========================================
    // 3. Interface and Abstract Base Class
    // ==========================================
    public interface IMaintainable
    {
        void PerformMaintenance();
    }

    public abstract class Appliance : IMaintainable
    {
        public delegate void StatusChangedHandler(string message);
        public event StatusChangedHandler OnStatusChanged;

        private static int total_appliances_count = 0;
        public static int TotalAppliancesCount { get { return total_appliances_count; } }

        private string serial_number;
        private string brand;
        private string repair_state;

        public string SerialNumber { get { return serial_number; } }
        public string Brand { get { return brand; } }

        public string RepairState
        {
            get { return repair_state; }
            set
            {
                string old_state = repair_state;
                repair_state = value;

                if (old_state != "Finished" && repair_state == "Finished" && OnStatusChanged != null)
                {
                    OnStatusChanged($"[Notification] Device Serial: ({serial_number}) Brand: ({brand}) is Finished and ready!");
                }
            }
        }

        public Appliance(string serial_number, string brand)
        {
            this.serial_number = serial_number;
            this.brand = brand;
            this.repair_state = "Started";
            total_appliances_count++;
        }

        public abstract double CalculateRepairCost();

        public void PerformMaintenance()
        {
            Console.WriteLine($"[Testing] Checking Device Serial: {serial_number} | Brand: {brand}");
        }
    }
}
