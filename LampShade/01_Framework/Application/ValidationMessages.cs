using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Framework.Application
{
    public class ValidationMessages
    {
        public const string IsRequired = "این مقدار نمی تواند خالی باشد!";
        public const string IsBetweenValue = " این مقدار می  بایست کمتر از مقدار تعیین شده وارد شود ";
        public const string MaxFileSize = " حجم فایل بیشتر از 3 مگابایت می باشد  ";
        public const string MaxLenght = "حداکثر طول رشته بیش از حد مجاز می باشد !";
    }
}
