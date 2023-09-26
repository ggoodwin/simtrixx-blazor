namespace Application.Interfaces.Services.Clock
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
        bool BeAValidDate(DateTime date);
    }
}
