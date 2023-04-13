namespace Application.Core
{
    // Creating mediator response result class which tell whether handler return success or failure
    // or return data or return null
    public class MediatorHandlerResult<T>
    {
        public bool IsSuccess { get; set; }
        public T Value { get; set; }
        public string Error { get; set; }

        public static MediatorHandlerResult<T> Success(T value) 
            => new MediatorHandlerResult<T> { IsSuccess = true, Value = value };

        public static MediatorHandlerResult<T> Failure(string error)
            => new MediatorHandlerResult<T> { IsSuccess = false , Error = error};
    }
}