namespace VueDotNetCoreMySql.Common.Utils
{
    public class CustomResponse<T>
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public T Result { get; set; }
    }
}