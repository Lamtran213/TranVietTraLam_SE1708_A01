using ASM_01_SE1708_SE182432_Repositories.Models;
using ASM_01_SE1708_SE182432_Services.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASM_01_SE1708_SE182432_Services.DTO.Response;

namespace ASM_01_SE1708_SE182432_Services.Def
{
    public interface INewsArticleService
    {
        public Task<IEnumerable<GetAllNewsArticle>> GetAllAsync();
        public Task<IEnumerable<GetAllNewsArticle>> GetAllAsyncStaff();
        public Task<IEnumerable<GetAllNewsArticle>> GetByIdAsync(string id);
        public Task<NewsArticle> AddAsync(AddNewsArticleResponse newsArticle);
        public Task<NewsArticle> UpdateAsync(UpdateNewsArticleResponse newsArticle);
        public Task DeleteAsync(string id);
        public Task<IEnumerable<GetAllNewsArticle>> FilterByDateAsync(DateTime startDate, DateTime endDate);
        protected Task<List<Tag>> GetTagsByIdsAsync(List<int> tagIds);
        public Task<IEnumerable<ViewNewsArticleHistory>> ViewNewsArticleByStaffId(short id);
    }
}
