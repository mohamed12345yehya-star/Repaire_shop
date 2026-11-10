using System;
using System.Collections.Generic;

namespace shhop
{
    // ==========================================
    // 1. Base Class for all people (Inheritance)
    // ==========================================
    public class Person
    {
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

    // ==========================================
    // 4. Subclasses for Appliances (Polymorphism)
    // ==========================================
    public class Fridge : Appliance
    {
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

    public class WashingMachine : Appliance
    {
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

    public class AirConditioner : Appliance
    {
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

    // ==========================================
    // 5. Static Class for Validation
    // ==========================================
    public static class MaintenanceValidator
    {
        public static bool IsValidSerialNumber(string serial_number)
        {
            if (!string.IsNullOrEmpty(serial_number) && serial_number.Length >= 5)
            {
                return true;
            }
            return false;
        }
    }

    // ==========================================
    // 6. Main Program and Menu Interface
    // ==========================================
    class Program
    {
        static void ShowNotification(string notification_message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(notification_message);
            Console.ResetColor();
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            List<Appliance> center_inventory = new List<Appliance>();

            Fridge f1 = new Fridge("REF-100", "L.G", 15000);
            f1.OnStatusChanged += ShowNotification;

            WashingMachine w1 = new WashingMachine("WM-500", "Samsung", 35000);
            w1.OnStatusChanged += ShowNotification;

            AirConditioner a1 = new AirConditioner("AC-900", "Gree", 20000);
            a1.OnStatusChanged += ShowNotification;

            center_inventory.Add(f1);
            center_inventory.Add(w1);
            center_inventory.Add(a1);

            bool keep_running = true;

            while (keep_running)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("==================================================");
                Console.WriteLine("        Maintenance Center Management System      ");
                Console.WriteLine("==================================================");
                Console.ResetColor();
                Console.WriteLine("Choose your role to login:");
                Console.WriteLine("1. Manager");
                Console.WriteLine("2. Technician");
                Console.WriteLine("3. Client");
                Console.WriteLine("4. Exit");
                Console.Write("Enter choice (1-4): ");

                string role_choice = Console.ReadLine();
                Console.WriteLine();

                switch (role_choice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("=== Manager Dashboard ===");
                        Console.WriteLine($"Total Appliances Registered: {Appliance.TotalAppliancesCount}");
                        Console.WriteLine("--------------------------------------------------");
                        double total_income = 0;
                        foreach (var app in center_inventory)
                        {
                            double cost = app.CalculateRepairCost();
                            total_income += cost;
                            Console.WriteLine($"Device: {app.Brand} ({app.GetType().Name}) | Serial: {app.SerialNumber} | State: {app.RepairState} | Cost: {cost} SP");
                        }
                        Console.WriteLine("--------------------------------------------------");
                        Console.WriteLine($"Total Expected Income: {total_income} SP");
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("=== Technician Dashboard ===");
                        Console.WriteLine("Available Devices:");
                        for (int i = 0; i < center_inventory.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. Device: {center_inventory[i].Brand} | Serial: {center_inventory[i].SerialNumber} | State: {center_inventory[i].RepairState}");
                        }
                        Console.Write("\nEnter device number to mark as Finished: ");
                        if (int.TryParse(Console.ReadLine(), out int app_index) && app_index > 0 && app_index <= center_inventory.Count)
                        {
                            Console.WriteLine("\n[Updating status...]");
                            center_inventory[app_index - 1].RepairState = "Finished";
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice!");
                        }
                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("=== Client Tracking ===");
                        Console.Write("Enter your device serial number: ");
                        string search_serial = Console.ReadLine();

                        if (!MaintenanceValidator.IsValidSerialNumber(search_serial))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Error: Invalid Serial Number format (Too short)!");
                            Console.ResetColor();
                        }
                        else
                        {
                            Appliance found_app = center_inventory.Find(a => a.SerialNumber.Equals(search_serial, StringComparison.OrdinalIgnoreCase));
                            if (found_app != null)
                            {
                                Console.WriteLine($"\nDevice Found: {found_app.Brand}");
                                Console.WriteLine($"Current Repair State: {found_app.RepairState}");
                                Console.WriteLine($"Estimated Cost: {found_app.CalculateRepairCost()} SP");
                            }
                            else
                            {
                                Console.WriteLine("\nDevice not found in our center.");
                            }
                        }
                        break;

                    case "4":
                        keep_running = false;
                        Console.WriteLine("Exiting System. Good luck with your project!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice, try again.");
                        break;
                }

                if (role_choice != "4")
                {
                    Console.WriteLine("\nPress Enter to return to main menu...");
                    Console.ReadLine();
                }
            }
        }
    }
}
