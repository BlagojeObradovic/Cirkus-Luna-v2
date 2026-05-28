// Ansvarlig: Onur
namespace CirkusLuna.ClassLibrary.Models
{
    public enum EmployeeRole
    {
        Manager,
        Cashier,
        Technician,
        Stagehand,
        Security,
        Other
    }

    public class Employee : Person
    {
        public EmployeeRole Role { get; set; }

        public string RoleDescription
        {
            get
            {
                switch (Role)
                {
                    case EmployeeRole.Manager: return "Leder";
                    case EmployeeRole.Cashier: return "Kasserer";
                    case EmployeeRole.Technician: return "Tekniker";
                    case EmployeeRole.Stagehand: return "Sceneteknikker";
                    case EmployeeRole.Security: return "Sikkerhed";
                    default: return "Andet";
                }
            }
        }

        public DateTime HireDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
    }
}
