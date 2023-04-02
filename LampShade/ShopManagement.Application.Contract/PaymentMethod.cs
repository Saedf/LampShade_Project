using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contract
{
    public class PaymentMethod
    {
        public long Id { get;  set; }
        public string Name { get;  set; }
        public string Description { get;  set; }

         PaymentMethod(long id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public static List<PaymentMethod> GetList()
        {
            return new List<PaymentMethod>
            {
                new PaymentMethod(1, "پرداخت اینترنتی",
                    "در این مدل شما به درگاه پرداخت اینترنتی هدایت شده و در لحظه پرداخت وجه را انجام خواهید"),
                new PaymentMethod(2, "پرداخت نقدی",
                    "در این مدل ابتدا سفارش ثبت شده و سپس وجه به صورت نقدی در زمان تحویل کالا دریافت خواهد شد"),

            };
        }

        public static PaymentMethod GetBy(long id)
        {
            return GetList().FirstOrDefault(x => x.Id == id);
        }
    }
}
