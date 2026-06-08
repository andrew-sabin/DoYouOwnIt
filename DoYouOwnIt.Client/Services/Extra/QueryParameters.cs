namespace DoYouOwnIt.Client.Services.Extra
{
    public class QueryParameters
    {
        public string? SearchText { get; set; } = string.Empty;
        public int? CategoryID { get; set; } = 0;
        public bool DescendingSearch { get; set; } = false;
        public int TotalCount { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 15;

    }
}
