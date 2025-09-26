using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoYouOwnIt.Shared.Models.ProductCategory;

namespace DoYouOwnIt.Shared.Models.Product
{
    public record struct ProductResponseNoCategory(
        int Id,
        string Name,
        string CoverImageURL,
        DateOnly ProductLaunchDate,
        string? Description,
        int CategoryId,
        bool IsLocked,
        string? Creators,
        string? CreditsURL,
        string? ContentRating,
        bool IsAIAssisted,
        bool ForMatureAudiences,
        string? MatureAudienceReason,
        string? CreatorId,
        string? ModifierId,
        DateTime LastModified
    );
}
