namespace ScanWord.Core.Entity.Common
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}