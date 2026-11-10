using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shhop
{

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
}
