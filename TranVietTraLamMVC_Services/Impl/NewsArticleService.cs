using TranVietTraLam_Repositories.Def;
using TranVietTraLam_Repositories.Models;
using TranVietTraLamMVC_Services.Def;
using TranVietTraLamMVC_Services.DTO.Request;
using TranVietTraLamMVC_Services.DTO.Response;

namespace TranVietTraLamMVC_Services.Impl
{
    public class NewsArticleService : INewsArticleService
    {
        private readonly INewsArticleRepo _newsArticleRepo;
        public NewsArticleService(INewsArticleRepo newsArticleRepo)
        {
            _newsArticleRepo = newsArticleRepo;
        }
        public async Task<IEnumerable<GetAllNewsArticle>> GetAllAsync()
        {
            var newsArticles = await _newsArticleRepo.GetAllAsync();
            return newsArticles.Select(article => new GetAllNewsArticle
            {
                NewsArticleId = article.NewsArticleId,
                NewsTitle = article.NewsTitle,
                Headline = article.Headline,
                CreatedDate = article.CreatedDate,
                NewsContent = article.NewsContent,
                NewsSource = article.NewsSource,
                CategoryId = article.CategoryId,
                NewsStatus = article.NewsStatus,
                CreatedById = article.CreatedById,
                UpdatedById = article.UpdatedById,
                ModifiedDate = article.ModifiedDate,
                Tags =  article.Tags.Select(tag => new Tag
                    {
                        TagId = tag.TagId,
                        TagName = tag.TagName
                    }).ToList()
            }).ToList();
        }

        public async Task<IEnumerable<GetAllNewsArticle>> GetAllAsyncStaff()
        {
            var newsArticles = await _newsArticleRepo.GetAllAsyncStaff();
            return newsArticles.Select(article => new GetAllNewsArticle
            {
                NewsArticleId = article.NewsArticleId,
                NewsTitle = article.NewsTitle,
                Headline = article.Headline,
                CreatedDate = article.CreatedDate,
                NewsContent = article.NewsContent,
                NewsSource = article.NewsSource,
                CategoryId = article.CategoryId,
                NewsStatus = article.NewsStatus,
                CreatedById = article.CreatedById,
                UpdatedById = article.UpdatedById,
                ModifiedDate = article.ModifiedDate,
                Tags =  article.Tags.Select(tag => new Tag
                {
                    TagId = tag.TagId,
                    TagName = tag.TagName
                }).ToList()
            }).ToList();
        }

        public async Task<IEnumerable<GetAllNewsArticle>> GetByIdAsync(string id)
        {
           var newsArticle = await _newsArticleRepo.GetByIdAsync(id);
           return new List<GetAllNewsArticle>
            {
                new()
                {
                    NewsArticleId = newsArticle.NewsArticleId,
                    NewsTitle = newsArticle.NewsTitle,
                    Headline = newsArticle.Headline,
                    CreatedDate = newsArticle.CreatedDate,
                    NewsContent = newsArticle.NewsContent,
                    NewsSource = newsArticle.NewsSource,
                    CategoryId = newsArticle.CategoryId,
                    NewsStatus = newsArticle.NewsStatus,
                    CreatedById = newsArticle.CreatedById,
                    UpdatedById = newsArticle.UpdatedById,
                    ModifiedDate = newsArticle.ModifiedDate,
                    Tags = newsArticle.Tags.Select(tag => new Tag
                    {
                        TagId = tag.TagId,
                        TagName = tag.TagName
                    }).ToList()
                }
            };
        }
        public async Task<List<Tag>> GetTagsByIdsAsync(List<int> tagIds)
        {
            return await _newsArticleRepo.GetTagsByIdsAsync(tagIds);
        }

        public async Task<IEnumerable<ViewNewsArticleHistory>> ViewNewsArticleByStaffId(short id)
        {
            var newsArticles = await _newsArticleRepo.ViewNewsArticleByStaffId(id);
            return newsArticles.Select(article => new ViewNewsArticleHistory
            {
                NewsArticleId = article.NewsArticleId,
                NewsTitle = article.NewsTitle,
                Headline = article.Headline,
                CreatedDate = article.CreatedDate,
                NewsContent = article.NewsContent,
                NewsSource = article.NewsSource,
                CategoryId = article.CategoryId,
                NewsStatus = article.NewsStatus,
                CreatedById = article.CreatedById,
                CreatedBy = article.CreatedBy == null ? null : new CreatedByInfoDto
                {
                    AccountId = article.CreatedBy.AccountId,
                    AccountEmail = article.CreatedBy.AccountEmail,
                },
                UpdatedById = article.UpdatedById,
                ModifiedDate = article.ModifiedDate,
                Tags = article.Tags.Select(tag => new Tag
                    {
                        TagId = tag.TagId,
                        TagName = tag.TagName
                    }).ToList()
            }).ToList();
        }

        public async Task<NewsArticle> AddAsync(AddNewsArticleResponse request)
        {
            var tags = await GetTagsByIdsAsync(request.SelectedTagIds);

            var newsArticle = new NewsArticle
            {
                NewsArticleId = Guid.NewGuid().ToString(),
                NewsTitle = request.NewsTitle,
                Headline = request.Headline,
                CreatedDate = request.CreatedDate ?? DateTime.Now,
                NewsContent = request.NewsContent,
                NewsSource = request.NewsSource,
                CategoryId = request.CategoryId,
                NewsStatus = request.NewsStatus,
                CreatedById = request.CreatedById,
                UpdatedById = request.UpdatedById,
                Tags = tags
            };
            return await _newsArticleRepo.AddAsync(newsArticle);
        }
        public async Task<NewsArticle> UpdateAsync(UpdateNewsArticleResponse newsArticle)
        {
            var tags = await GetTagsByIdsAsync(newsArticle.SelectedTagIds);
            var updateNewsArticle = new NewsArticle
            {
                NewsArticleId = newsArticle.NewsArticleId,
                NewsTitle = newsArticle.NewsTitle,
                Headline = newsArticle.Headline,
                NewsContent = newsArticle.NewsContent,
                NewsSource = newsArticle.NewsSource,
                CategoryId = newsArticle.CategoryId,
                NewsStatus = newsArticle.NewsStatus,
                UpdatedById = newsArticle.UpdatedById,
                ModifiedDate = newsArticle.ModifiedDate,
                Tags = tags
            };
            return await _newsArticleRepo.UpdateAsync(updateNewsArticle);
        }

        public Task DeleteAsync(string id)
        {
            var newsArticle = _newsArticleRepo.GetByIdAsync(id);
            if (newsArticle == null)
            {
                throw new KeyNotFoundException("News article not found.");
            }
            return _newsArticleRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<GetAllNewsArticle>> FilterByDateAsync(DateTime startDate, DateTime endDate)
        {
            var filteredArticles = await _newsArticleRepo.FilterByDateAsync(startDate, endDate);
            return filteredArticles.Select(article => new GetAllNewsArticle
            {
                NewsArticleId = article.NewsArticleId,
                NewsTitle = article.NewsTitle,
                Headline = article.Headline,
                CreatedDate = article.CreatedDate,
                NewsContent = article.NewsContent,
                NewsSource = article.NewsSource,
                CategoryId = article.CategoryId,
                NewsStatus = article.NewsStatus,
                CreatedById = article.CreatedById,
                CreatedBy = article.CreatedBy == null ? null : new CreatedByInfoDto
                {
                    AccountId = article.CreatedBy.AccountId,
                    AccountEmail = article.CreatedBy.AccountEmail,
                },
                UpdatedById = article.UpdatedById,
                ModifiedDate = article.ModifiedDate,
                Tags = article.Tags.Select(tag => new Tag
                    {
                        TagId = tag.TagId,
                        TagName = tag.TagName
                    }).ToList()
            }).ToList();
        }
    }
}
