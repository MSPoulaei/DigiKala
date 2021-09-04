﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiKala.DataAccessLayer.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [MaxLength(11, ErrorMessage = "مقدار {0} نباید بیشتر از {1} کاراکتر باشد")]
        public string Mobile { get; set; }
        [Display(Name ="کلمه عبور")]
        [Required(ErrorMessage ="نباید بدون مقدار باشد")]
        [MaxLength(50,ErrorMessage ="مقدار {0} نباید بیشتر از {1} کاراکتر باشد")]
        public string Password { get; set; }
        [Display(Name ="کد ملی")]
        [Required(ErrorMessage ="نباید بدون مقدار باشد")]
        [MaxLength(10,ErrorMessage ="مقدار {0} نباید بیشتر از {1} کاراکتر باشد")]
        public string Code { get; set; }
        [Display(Name ="نام و نام خانوادگی")]
        [Required(ErrorMessage ="نباید بدون مقدار باشد")]
        [MaxLength(100,ErrorMessage ="مقدار {0} نباید بیشتر از {1} کاراکتر باشد")]
        public string FullName { get; set; }
        [Display(Name ="کد فعال سازی")]
        [Required(ErrorMessage ="نباید بدون مقدار باشد")]
        [MaxLength(6,ErrorMessage ="مقدار {0} نباید بیشتر از {1} کاراکتر باشد")]
        public string ActivationCode { get; set; }
        [Display(Name = "فعال")]
        public bool IsActive { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

    }
}