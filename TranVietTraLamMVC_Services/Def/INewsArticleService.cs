using TranVietTraLam_Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranVietTraLamMVC_Services.DTO.Request;
using TranVietTraLamMVC_Services.DTO.Response;

namespace TranVietTraLamMVC_Services.Def
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
