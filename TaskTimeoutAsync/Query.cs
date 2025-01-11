namespace TaskTimeoutAsync
{
    class Query
    {
        public string Value { get; set; }
        public QueryParams Params { get; set; }

        public Query(string value, QueryParams queryParams)
        {
            Value = value;
            Params = queryParams;
        }
    }
}