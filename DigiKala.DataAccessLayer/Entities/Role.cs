using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DigiKala.DataAccessLayer.Entities
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="نام نقش")]
        [Required(ErrorMessage ="نباید بدون مقدار باشد")]
        [MaxLength(20,ErrorMessage ="مقدار {0} نباید بیشتر از {1} کاراکتر باشد")]
        public string  Name { get; set; }
        public virtual ICollection<RolePermissions> RolePermissions{ get; set; }
        public virtual ICollection<User> Users{ get; set; }
    }
}
