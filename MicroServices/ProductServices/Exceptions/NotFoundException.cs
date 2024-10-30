namespace ProductServices.Exceptions
{
    public class NotFoundException:Exception
    {
        public NotFoundException(Guid EntityId, string EntityName) : base($"موجودیت با ایدی {EntityId}و نام {EntityName} یافت نشد.") { }
    }
}
