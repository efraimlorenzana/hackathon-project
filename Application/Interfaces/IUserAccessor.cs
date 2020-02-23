namespace Application.Interfaces
{
    public interface IUserAccessor
    {
         string GetCurrentUsername();
         int GetTotalPoints();
    }
}