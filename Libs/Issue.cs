using DeliveryApp.DBEntities;
using System;
using System.Linq;

namespace DeliveryApp.Libs
{
    class Issue
    {
        internal static void IssuePackage(int package_ID)
        {
            using (var context = new Delivery_DBEntities())
            {
                var issuePackage = new Package_Movings
                {
                    Package_ID = package_ID,
                    MoveType_ID = 2,
                    MoveDate = DateTime.Now,

                };
                context.Package_Movings.Add(issuePackage);
                Package package = Delivery_DBEntities.GetContext().Packages.FirstOrDefault(p => p.Package_ID == package_ID);
                package.isActive = false;
                Delivery_DBEntities.GetContext().SaveChanges();
                context.SaveChanges();
            }
        }
    }
}
