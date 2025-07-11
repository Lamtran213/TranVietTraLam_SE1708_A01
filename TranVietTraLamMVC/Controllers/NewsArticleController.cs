﻿using TranVietTraLam_Repositories.Models;
using TranVietTraLamMVC_Services.Def;
using TranVietTraLamMVC_Services.DTO.Request;
using TranVietTraLamMVC_Services.DTO.Response;
using Microsoft.AspNetCore.Mvc;

namespace TranVietTraLamMVC.Controllers
{
    public class NewsArticleController : Controller
    {
        private readonly INewsArticleService _newsArticleService;
        private readonly ITagsService _tagsService;
        public NewsArticleController(INewsArticleService newsArticleService, ITagsService tagsService)
        {
            _newsArticleService = newsArticleService;
            _tagsService = tagsService;
        }
        [HttpGet]
        public async Task<IActionResult> NewsArticle()
        {
            var articles = await _newsArticleService.GetAllAsync();
            if (!articles.Any())
            {
                return NotFound("No news articles found.");
            }
            return View("NewsArticle", articles);
        }

        [HttpGet]
        public async Task<IActionResult> NewsArticleForStaff()
        {
            var articles = await _newsArticleService.GetAllAsyncStaff();
            var tags = await _tagsService.GetAllTags();
            ViewBag.TagList = tags;
            if (!articles.Any())
            {
                return NotFound("No news articles found.");
            }
            return View("NewsArticleForStaff", articles);
        }

        [HttpGet]
        public async Task<IActionResult> GetNewsArticleById(string id)
        {
            var articles = await _newsArticleService.GetByIdAsync(id);
    
            if (!articles.Any())
            {
                ViewBag.Error = $"No article found with ID: {id}";
            }

            return View("NewsArticle", articles);
        }
        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            var tags = await _tagsService.GetAllTags();
            if (tags == null || !tags.Any())
            {
                ViewBag.Error = "No tags found.";
                return View("NewsArticleForStaff");
            }
            var allNewsArticle = await _newsArticleService.GetAllAsyncStaff();
            ViewBag.TagList = await _tagsService.GetAllTags(); 
            return View("NewsArticleForStaff", allNewsArticle);

        }

        [HttpPost]
        public async Task<IActionResult> AddNewsArticle(AddNewsArticleResponse newsArticle)
        {
            var currentStaff = HttpContext.Session.GetObject<SystemAccount>("StaffAccount");
            if (currentStaff == null)
            {
                ViewBag.Error = "You must be logged in as a staff member to add a news article.";
                var fallbackArticles = await _newsArticleService.GetAllAsyncStaff();
                ViewBag.TagList = await _tagsService.GetAllTags();
                return View("NewsArticleForStaff", fallbackArticles);
            }

            newsArticle.CreatedById = currentStaff.AccountId;
            await _newsArticleService.AddAsync(newsArticle);

            return RedirectToAction(nameof(NewsArticleForStaff));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateNewsArticle(UpdateNewsArticleResponse newsArticle)
        {
            var currentStaff = HttpContext.Session.GetObject<SystemAccount>("StaffAccount");
            if (currentStaff == null)
            {
                ViewBag.Error = "You must be logged in as a staff member to update a news article.";
                var fallbackArticles = await _newsArticleService.GetAllAsyncStaff();
                ViewBag.TagList = await _tagsService.GetAllTags(); 
                return View("NewsArticleForStaff", fallbackArticles);
            }

            newsArticle.UpdatedById = currentStaff.AccountId;
            await _newsArticleService.UpdateAsync(newsArticle);

            var allNewsArticle = await _newsArticleService.GetAllAsyncStaff();
            ViewBag.TagList = await _tagsService.GetAllTags(); 

            return View("NewsArticleForStaff", allNewsArticle);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteNewsArticle(string id)
        {
            try
            {
                await _newsArticleService.DeleteAsync(id);
                var allNewsArticle = await _newsArticleService.GetAllAsyncStaff();
                return View("NewsArticleForStaff", allNewsArticle);
            }
            catch (KeyNotFoundException ex)
            {
                ViewBag.Error = ex.Message;
                return View("NewsArticleForStaff");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllFilter()
        {
            var allNewsArticle = await _newsArticleService.GetAllAsyncStaff();

            var totalArticles = await _newsArticleService.GetTotalNewsArticlesAsync();
            ViewBag.TotalArticles = totalArticles;

            if (!allNewsArticle.Any())
            {
                ViewBag.Error = "No news articles found.";
                return View("ReportStatistic");
            }

            return View("ReportStatistic", allNewsArticle);
        }


        public async Task<IActionResult> FilterByDate(DateTime? startDate, DateTime? endDate)
        {
            IEnumerable<GetAllNewsArticle> filteredArticles;

            if (!Request.Query.ContainsKey("startDate") || !Request.Query.ContainsKey("endDate"))
            {
                filteredArticles = await _newsArticleService.GetAllAsyncStaff();
            }
            else
            {
                if (startDate == null || endDate == null)
                {
                    ViewBag.Error = "Please select both start and end dates.";
                    filteredArticles = new List<GetAllNewsArticle>();
                }
                else if (startDate > endDate)
                {
                    ViewBag.Error = "Start date must be before or equal to end date.";
                    filteredArticles = new List<GetAllNewsArticle>();
                }
                else
                {
                    filteredArticles = await _newsArticleService.FilterByDateAsync(startDate.Value, endDate.Value);
                    if (!filteredArticles.Any())
                    {
                        ViewBag.Error = "No news articles found for the selected date range.";
                    }
                }
            }

            ViewBag.TotalArticles = filteredArticles.Count();

            return View("ReportStatistic", filteredArticles);
        }


        [HttpGet]
        public async Task<IActionResult> ViewNewsArticleByStaffId(short id)
        {
            var articles = await _newsArticleService.ViewNewsArticleByStaffId(id);
            var currentStaff = HttpContext.Session.GetObject<SystemAccount>("StaffAccount");
            if (!articles.Any())
            {
                ViewBag.Error = $"No news articles found for staff ID: {id}";
            }

            var tags = await _tagsService.GetAllTags();
            ViewBag.Articles = articles;
            ViewBag.TagList = tags;

            return View("ViewHistory", articles);
        }

        [HttpGet]
        public async Task<IActionResult> GetTotalNewsArticles()
        {
            var totalArticles = await _newsArticleService.GetTotalNewsArticlesAsync();
            if (totalArticles == 0)
            {
                ViewBag.Error = "No news articles found.";
                return View("ReportStatistic");
            }

            ViewBag.TotalArticles = totalArticles;
            return View("ReportStatistic");
        }
    }
}
