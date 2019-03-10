using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using doremi.Services;

namespace doremi.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context,
           IFunctional functional)
        {
            context.Database.EnsureCreated();

            //check for users
            if (context.ApplicationUser.Any())
            {
                return; //if user is not empty, DB has been seed
            }

            //init app with super admin user
            await functional.CreateDefaultSuperAdmin();

            List<string> warehouseRoles = new List<string>{ "Dashboard Main", "Warehouse","Sales Order","Event Log","Product","Shipment","Vendor Type", "Vendor", "Purchase Type","Purchase Order", "Goods Received Note", "Bill", "Payment Voucher", "Product", "Product Type", "Unit Of Measure", "Change Password"};
            List<string> salesRoles = new List<string> { "Dashboard Main", "Sales Order","Sales Type", "Event Log", "Customer", "Customer Type", "Change Password" };
            List<string> operationRoles = new List<string> { "Dashboard Main", "Sales Order", "Event Log", "Change Password" };
            List<string> accountRoles = new List<string> { "Dashboard Main", "Sales Order", "Event Log", "Change Password" };
            //Warehouse
            await functional.CreateUsers("rogan@doremi.com", "test1234", "Rogan", "Beasley", 1, warehouseRoles);
            //Sales
            await functional.CreateUsers("javier@doremi.com", "test1234", "Javier", "Lu", 2, salesRoles);
            //Operatioin
            await functional.CreateUsers("jiya@doremi.com", "test1234", "Jiya", "Lane", 3, operationRoles);
            //Account
            await functional.CreateUsers("abel@doremi.com", "test1234", "Abel", "Williams", 4, accountRoles);

            //init app data
            await functional.InitAppData();

        }
    }
}
