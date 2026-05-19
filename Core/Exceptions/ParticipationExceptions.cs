namespace sk.Core.Exceptions
{
    ///<summary>
    ///Ошибка при попытке записаться на событие
    ///</summary>
    public class ParticipationException : Exception
    {
        public int UserID;
        public ParticipationException(): base() { }
        public ParticipationException(string message) : base(message) { }
        public ParticipationException(string message, Exception exception) : base(message, exception) { }
        public ParticipationException(int userID, string message): base (message)
        {
            UserID = userID;
        }
    }
}
