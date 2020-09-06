namespace Tete.Models.Authentication
{
  [System.Serializable]
  public class NotLoggedInException : System.Exception
  {
    public NotLoggedInException() { }
    public NotLoggedInException(string message) : base(message) { }
    public NotLoggedInException(string message, System.Exception inner) : base(message, inner) { }
    protected NotLoggedInException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
  }

  [System.Serializable]
  public class InsufficientPriviledgesException : System.Exception
  {
    public InsufficientPriviledgesException() { }
    public InsufficientPriviledgesException(string message) : base(message) { }
    public InsufficientPriviledgesException(string message, System.Exception inner) : base(message, inner) { }
    protected InsufficientPriviledgesException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
  }
}