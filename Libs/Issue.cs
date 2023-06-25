using DeliveryApp.DBEntities;
using System;
using System.Linq;

namespace DeliveryApp.Libs
{
    /// <summary>
    /// Класс Issue
    /// Отвечает за выдачу посылок
    /// </summary>
    public class Issue
    {
        /// <summary>
        /// Метод IssuePackage
        /// Создает запись с типом "Выдана получателю"
        /// и устанавливает значение isActive у Посылки в 0
        /// </summary>
        /// <param name="package_ID"> Идентификатор посылки </param>
        public static void IssuePackage(int package_ID)
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
