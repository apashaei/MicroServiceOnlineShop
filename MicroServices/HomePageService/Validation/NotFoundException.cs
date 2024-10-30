namespace HomPageServices.Validation
{
    public class NotFoundException : Exception 
    {
        public NotFoundException(string name, string key):base($"موجودیت {name}با کلید {key} پیدا نشد.") { }
    }
}
