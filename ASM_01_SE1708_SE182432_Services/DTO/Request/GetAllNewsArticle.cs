using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASM_01_SE1708_SE182432_Repositories.Models;

namespace ASM_01_SE1708_SE182432_Services.DTO.Request
{
    public class GetAllNewsArticle
    {
        public string NewsArticleId { get; set; } = null!;

        public string? NewsTitle { get; set; }

        public string Headline { get; set; } = null!;

        public DateTime? CreatedDate { get; set; }

        public string? NewsContent { get; set; }

        public string? NewsSource { get; set; }

        public short? CategoryId { get; set; }

        public bool? NewsStatus { get; set; }

        public short? CreatedById { get; set; }
        public virtual CreatedByInfoDto? CreatedBy { get; set; }

        public short? UpdatedById { get; set; }

        public DateTime? ModifiedDate { get; set; }
        
        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
    public class CreatedByInfoDto
    {
        public short AccountId { get; set; }
        public string? AccountEmail { get; set; }
    }
}
