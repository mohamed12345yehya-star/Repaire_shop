using System;
using System.Collections.Generic;

namespace shhop
{
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
