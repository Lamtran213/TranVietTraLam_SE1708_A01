using ASM_01_SE1708_SE182432_Repositories.Models;

namespace ASM_01_SE1708_SE182432_Repositories.Def
{
    public interface INewsArticleRepo
    {
        public Task<IEnumerable<NewsArticle>> GetAllAsync();
        public Task<IEnumerable<NewsArticle>> GetAllAsyncStaff();
        public Task<NewsArticle> GetByIdAsync(string id);
        public Task<NewsArticle> AddAsync(NewsArticle newsArticle);
        public Task<NewsArticle> UpdateAsync(NewsArticle newsArticle);
        public Task DeleteAsync(string id);
        public Task<IEnumerable<NewsArticle>> FilterByDateAsync(DateTime startDate, DateTime endDate);
        public Task<IEnumerable<NewsArticle>> DescendingByDateAsync();  
        public Task<List<Tag>> GetTagsByIdsAsync(List<int> tagIds);
        public Task<IEnumerable<NewsArticle>> ViewNewsArticleByStaffId(short id);
    }
}
