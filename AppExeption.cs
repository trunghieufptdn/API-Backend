using System.Globalization;

namespace WebApiLearn;

public class AppExeption: Exception
{
    public AppExeption() : base() { }
        
    public AppExeption(string message) : base(message) { }

    public AppExeption(string message, params object[] args)
        : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}