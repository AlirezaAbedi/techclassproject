using FluentValidation.Results;

namespace Model
{
    public class ServiceDto<T>
    {
        public byte Status { get; set; } = 1;
        public T Data { get; set; }
        public string Message { get; set; }

        public List<string> Errors { get; set; }

        //public static explicit operator Microsoft.AspNetCore.Mvc.OkObjectResult(ServiceDto<int> v)
        //{
        //    throw new NotImplementedException();
        //}
        //public IList<ValidationFailure> Errors { get; set; }

    }

    public class PagedResultDto<T>
    {
        public int PageNumber { get; set; }
        public int TotalCount { get; set; }

        public List<T> Items { get; set; }

        
    }
}
