// Application/Services/ArticleService.cs
using ContentOrderSystem.Domain;
using ContentOrderSystem.Application.Interfaces;
using System;
using System.Collections.Generic;

namespace ContentOrderSystem.Application.Services
{
    public class ArticleService
    {
        private readonly IRepository<Article> _articleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ArticleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _articleRepository = unitOfWork.Articles;
        }

        public IEnumerable<Article> GetAllArticles()
        {
            return _articleRepository.GetAll();
        }

        public void AddArticle(Article article)
        {
            if (string.IsNullOrEmpty(article.Name))
                throw new ArgumentException("Article name is required.");

            _articleRepository.Add(article);
            _unitOfWork.Commit();
        }

        public void UpdateArticle(Article article)
        {
            _articleRepository.Update(article);
            _unitOfWork.Commit();
        }

        public void DeleteArticle(Article article)
        {
            // Soft-delete
            article.IsArchived = true;
            _articleRepository.Update(article);
            _unitOfWork.Commit();
        }
    }
}
