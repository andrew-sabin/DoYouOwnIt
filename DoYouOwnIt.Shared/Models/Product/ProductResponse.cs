using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoYouOwnIt.Shared.Models.ProductCategory;

namespace DoYouOwnIt.Shared.Models.Product
{
    public record struct ProductResponse(
        int Id,
        string Name,
        string Slug,
        string? CoverImageURL,
        DateOnly ProductLaunchDate,
        string? Description,
        ProductCategoryResponse Category,
        bool IsLocked,
        string? Creators,
        string? CreditsURL,
        string? ContentRating,
        bool IsAIAssisted,
        string? AIAssistsWith,
        bool ForMatureAudiences,
        string? MatureAudienceReason,
        string? CreatorId,
        string? ModifierId,
        DateTime LastModified
    );
}
