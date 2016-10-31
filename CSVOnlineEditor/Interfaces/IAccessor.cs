namespace CSVOnlineEditor.Interfaces
{
    public interface IAccessor<T>
    {
        string[] GetObjectData(T obj);
    }
}
