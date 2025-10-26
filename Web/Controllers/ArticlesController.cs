// Web/Controllers/ArticlesController.cs
using System;
using System.Collections.Generic;
using ContentOrderSystem.Domain;
using ContentOrderSystem.Application.Services;

namespace ContentOrderSystem.Web.Controllers
{
    public class ArticlesController
    {
        private readonly ArticleService _articleService;

        public ArticlesController(ArticleService articleService)
        {
            _articleService = articleService;
        }

        // Display list of articles
        public IEnumerable<Article> Index()
        {
            return _articleService.GetAllArticles();
        }

        // Add new article
        public void Add(Article article)
        {
            try
            {
                _articleService.AddArticle(article);
                Console.WriteLine("Article added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Edit article
        public void Edit(Article article)
        {
            try
            {
                _articleService.UpdateArticle(article);
                Console.WriteLine("Article updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Delete article
        public void Delete(Guid id)
        {
            try
            {
                var article = _articleService.GetAllArticles().FirstOrDefault(a => a.Id == id);
                if (article == null)
                    Console.WriteLine("Article not found.");
                else
                    _articleService.DeleteArticle(article);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
