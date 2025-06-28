using Microsoft.EntityFrameworkCore;
using TranVietTraLam_Repositories.Def;
using TranVietTraLam_Repositories.Models;

namespace TranVietTraLam_Repositories.Impl
{
    public class NewsArticleRepo : INewsArticleRepo
    {
        private readonly FunewsManagementContext _context;
        public NewsArticleRepo(FunewsManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NewsArticle>> GetAllAsync()
        {
            return await _context.NewsArticles.Include(tags => tags.Tags).
                Where(a => a.NewsStatus == true).ToListAsync();
        }

        public async Task<IEnumerable<NewsArticle>> GetAllAsyncStaff()
        {
            return await _context.NewsArticles.Include(tags => tags.Tags)
                .OrderByDescending(a => a.CreatedDate)
                .ToListAsync();
        }

        public async Task<NewsArticle> GetByIdAsync(string id)
        {
            return await _context.NewsArticles.Include(tags => tags.Tags)
                       .Where(a => a.NewsArticleId == id && a.NewsStatus == true)
                      .FirstOrDefaultAsync() 
                      ?? throw new KeyNotFoundException("News article not found.");
        }

        public async Task<NewsArticle> AddAsync(NewsArticle newsArticle)
        {
            var newId = _context.NewsArticles.Count() + 1;
             newsArticle.NewsArticleId = newId.ToString();
            _context.NewsArticles.Add(newsArticle);
            await _context.SaveChangesAsync();
            return await Task.FromResult(newsArticle);
        }
        
        public async Task<List<Tag>> GetTagsByIdsAsync(List<int> tagIds)
        {
            return await _context.Tags
                .Where(t => tagIds.Contains(t.TagId))
                .ToListAsync();
        }

        public async Task<IEnumerable<NewsArticle>> ViewNewsArticleByStaffId(short id)
        {
            var articles = await _context.NewsArticles
                .Where(a => a.CreatedById == id)
                .Include(tags => tags.Tags)
                .Include(a => a.Category)
                .Include(a => a.CreatedBy)
                .OrderByDescending(a => a.CreatedDate)
                .ToListAsync();
            return articles;
        }

        public async Task<int> GetTotalNewsArticlesAsync()
        {
            var totalArticles = await _context.NewsArticles.CountAsync(a=> a.NewsArticleId != null);
            return totalArticles;
        }

        public async Task<NewsArticle> UpdateAsync(NewsArticle newsArticle)
        {
            var existingArticle = await _context.NewsArticles
                .Include(tags => tags.Tags)
                .FirstOrDefaultAsync(a => a.NewsArticleId == newsArticle.NewsArticleId);
            if (existingArticle == null)
            {
                throw new KeyNotFoundException("News article not found.");
            }

            existingArticle.UpdatedById = newsArticle.UpdatedById;
            existingArticle.NewsTitle = newsArticle.NewsTitle;
            existingArticle.Headline = newsArticle.Headline;
            existingArticle.NewsContent = newsArticle.NewsContent;
            existingArticle.NewsSource = newsArticle.NewsSource;
            existingArticle.NewsStatus = newsArticle.NewsStatus;
            existingArticle.ModifiedDate = DateTime.UtcNow;
            existingArticle.CategoryId = newsArticle.CategoryId;
            existingArticle.Tags = newsArticle.Tags;

            await _context.SaveChangesAsync();
            return existingArticle;
        }

        public async Task DeleteAsync(string id)
        {
            var newsArticle = await _context.NewsArticles
                .Include(n => n.Tags) 
                .FirstOrDefaultAsync(a => a.NewsArticleId == id);

            if (newsArticle == null)
            {
                throw new KeyNotFoundException("News article not found."); 
            }

            _context.NewsArticles.Remove(newsArticle);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<NewsArticle>> FilterByDateAsync(DateTime startDate, DateTime endDate)
        {
            var filteredArticles = await _context.NewsArticles
                .Where(a => a.CreatedDate != null &&
                            a.CreatedDate >= startDate &&
                            a.CreatedDate <= endDate)
                .Include(a => a.Tags)
                .Include(a => a.Category)
                .Include(a => a.CreatedBy)
                .OrderByDescending(a => a.CreatedDate)
                .ToListAsync();

            return filteredArticles;
        }

        public async Task<IEnumerable<NewsArticle>> DescendingByDateAsync()
        {
            var descendingArticles = await _context.NewsArticles
                .Where(a => a.NewsStatus == true)
                .OrderByDescending(a => a.CreatedDate)
                .Include(tags => tags.Tags)
                .ToListAsync();
            return descendingArticles;
        }
    }
}
