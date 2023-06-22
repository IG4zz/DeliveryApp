using DeliveryApp.DBEntities;
using System;

namespace DeliveryApp.Libs
{
    class Issue
    {
        internal static void IssuePackage(int packageID)
        {
            using (var context = new Delivery_DBEntities())
            {
                var issuePackage = new Package_Movings
                {
                    Package_ID = packageID,
                    MoveType_ID = 2,
                    MoveDate = DateTime.Now,

                };

               context.Package_Movings.Add(issuePackage);
            }
        }
    }
}
