﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _02_LampShadeQuery.Contracts.Article;
using _02_LampShadeQuery.Contracts.Query;

namespace _02_LampShadeQuery.Contracts.ArticleCategory
{
    public class ArticleCategoryQueryModel
    {
        public string Name { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Description { get; set; }
        public int ShowOrder { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public List<string> KeywordList { get; set; }
        public string MetaDecription { get; set; }
        public string CanonicalAddress { get; set; }
        public long ArticleCount { get; set; }
        public List<ArticleQueryModel> ArticleQueries { get; set; }


    }
}
