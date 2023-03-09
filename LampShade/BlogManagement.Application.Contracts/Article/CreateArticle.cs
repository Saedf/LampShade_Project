using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Http;

namespace BlogManagement.Application.Contracts.Article
{
    public class CreateArticle
    {
        [MaxLength(500,ErrorMessage = ValidationMessages.MaxLenght)]
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Title { get;  set; }

        [MaxLength(500, ErrorMessage = ValidationMessages.MaxLenght)]
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string ShortDescription { get;  set; }
        public string Description { get; set; }
        public IFormFile Picture { get;  set; }

        [MaxLength(500, ErrorMessage = ValidationMessages.MaxLenght)]
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureAlt { get;  set; }

        [MaxLength(500, ErrorMessage = ValidationMessages.MaxLenght)]
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureTitle { get;  set; }

      
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PublishDate { get;  set; }
        public string Slug { get;  set; }

        [MaxLength(100, ErrorMessage = ValidationMessages.MaxLenght)]
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string KeyWords { get;  set; }
        public string MetaDescription { get;  set; }
        public string CanonicalAddress { get;  set; }

       
        [Range(1,long.MaxValue,ErrorMessage = ValidationMessages.IsRequired)]
        public long CategoryId { get;  set; }
    }
}
