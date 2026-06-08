using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Models.Product
{
    public record struct ProductCreateRequest(
        string Name,
        string? CoverImageURL,
        DateOnly ProductLaunchDate,
        string? Description,
        string? DescriptionSource,
        int CategoryId,
        string? CategoryName,
        bool IsLocked,
        string? Creators,
        int ContentRating,
        bool IsAIAssisted,
        string? AIAssistsWith,
        bool ForMatureAudiences,
        string? MatureAudienceReason,
        bool HasIssue,
        string? Issue,
        string? IssueURL
    );
}
