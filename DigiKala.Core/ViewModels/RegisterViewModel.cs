﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DigiKala.Core.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [MaxLength(11, ErrorMessage = "مقدار {0} نباید بیشتر از {1} کاراکتر باشد")]
        [MinLength(11, ErrorMessage = "مقدار {0} نباید کمتر از {1} کاراکتر باشد")]
        public string Mobile { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیشتر از {1} کاراکتر باشد")]
        [MinLength(4, ErrorMessage = "مقدار {0} نباید کمتر از {1} کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "تایید کلمه عبور")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="کلمه های عبور همخوانی ندارد")]
        public string ConfirmPassword { get; set; }
    }
}