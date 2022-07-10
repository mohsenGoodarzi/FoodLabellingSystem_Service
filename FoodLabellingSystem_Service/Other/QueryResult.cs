namespace FoodLabellingSystem_Service.Other
{
    public struct QueryResult
    {
        public QueryResultType Result { get; set; }
        public string Message { get; set; }
        public int ErrorCode { get; set; }
    }

}

