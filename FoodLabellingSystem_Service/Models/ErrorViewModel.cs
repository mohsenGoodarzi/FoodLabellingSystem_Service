using FoodLabellingSystem_Service.Other;

namespace FoodLabellingSystem_Service.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    public QueryResult QureyResult { get; set; }

}

