namespace ProductServices.Models.Dtos
{
    public class ResultDto<T>
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public T Data { get; set; }
    }

    public class ResultDto
    {
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
