// Infrastructure/Repositories/EfArticleRepository.cs
using System;
using System.Collections.Generic;
using ContentOrderSystem.Domain;
using ContentOrderSystem.Application.Interfaces;

namespace ContentOrderSystem.Infrastructure.Repositories
{
    public class EfArticleRepository : IRepository<Article>
    {
        private readonly List<Article> _articles = new(); // Simulated DB

        public void Add(Article entity) => _articles.Add(entity);

        public void Delete(Article entity) => _articles.Remove(entity);

        public IEnumerable<Article> GetAll() => _articles;

        public Article GetById(Guid id) => _articles.Find(a => a.Id == id);

        public void Update(Article entity)
        {
            var index = _articles.FindIndex(a => a.Id == entity.Id);
            if (index >= 0) _articles[index] = entity;
        }
    }
}
