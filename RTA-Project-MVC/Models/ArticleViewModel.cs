using RTA_Project_DAL.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RTA_Project_MVC.Models
{
    public class ArticleViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле является обязательным для заполнения")]
        [Display(Name = "Заголовок")]
        [StringLength(40, ErrorMessage = "{0} должен содержать от {2} до {1} символов", MinimumLength = 5)]
        public string Title { get; set; }

        public Lang LangId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} не должно быть пустым")]
        [MaxLength(6000)]
        public string Content { get; set; }

        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public DateTime Date { get; set; }
    }
}