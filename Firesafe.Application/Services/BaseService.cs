namespace Application.Services;

public abstract class BaseService : IDisposable
{
    public class ServiceException(string message) : Exception
    {
        public override string Message { get; } = message;
    }
    
    protected void NotifyError(string message)
    {
        throw new ServiceException(message);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}