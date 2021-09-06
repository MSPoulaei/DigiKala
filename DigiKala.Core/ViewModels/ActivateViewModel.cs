using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DigiKala.Core.ViewModels
{
    public class ActivateViewModel
    {
        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [Phone(ErrorMessage ="فقط عدد میتوانید وارد کنید")]
        [MaxLength(11, ErrorMessage = "مقدار {0} نباید بیشتر از {1} کاراکتر باشد")]
        public string Mobile { get; set; }
        [Display(Name = "کد فعال سازی")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [Phone(ErrorMessage ="فقط عدد میتوانید وارد کنید")]
        [MaxLength(6, ErrorMessage = "مقدار {0} نباید بیشتر از {1} کاراکتر باشد")]
        public string ActivationCode { get; set; }
    }
}
