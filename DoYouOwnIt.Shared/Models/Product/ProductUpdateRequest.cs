using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.Product
{
    public record struct ProductUpdateRequest(
        string Name,
        string? CoverImageURL,
        DateOnly ProductLaunchDate,
        string? Slug,
        string? Description,
        int CategoryId,
        bool IsLocked,
        string? Creators,
        string? CreditsURL,
        string? ContentRating,
        bool IsAIAssisted,
        string? AIAssistsWith,
        bool ForMatureAudiences,
        string? MatureAudienceReason
    );
}
