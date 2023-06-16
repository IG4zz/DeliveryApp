using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryApp.UI;
using DeliveryApp.DBEntities;

namespace DeliveryApp.Libs
{
    /// <summary>
    /// Класс SaveEntryInfo
    /// Отвечает за сохранение данных о входе в систему
    /// </summary>
    public class SaveEntryInfo
    {
        /// <summary>
        /// Метод SaveLoginHistory
        /// Сохраняет данные о входе в систему
        /// </summary>
        /// <param name="userId">Идентификатор Пользователя</param>
        /// <param name="loginTime">Дата и время входа</param>
        internal static void SaveLoginHistory(int userId, DateTime loginTime)
        {
            using (var context = new Delivery_DBEntities())
            {
                var loginHistory = new EntryHistory
                {
                    User_ID = userId,
                    DateTime = loginTime,

                };

                context.EntryHistories.Add(loginHistory);
                context.SaveChanges();
            }
        }
    }
}
