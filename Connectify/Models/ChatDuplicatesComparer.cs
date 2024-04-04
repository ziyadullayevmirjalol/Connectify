namespace Connectify.Models;

public class ChatDuplicatesComparer : IEqualityComparer<Chat>
{
    public bool Equals(Chat x, Chat y)
    {
        if (x.Id == y.Id)
        {
            return true;
        }
        else { return false; }
    }

    public int GetHashCode(Chat chat)
    {
        return chat.Id.GetHashCode();
    }
}
