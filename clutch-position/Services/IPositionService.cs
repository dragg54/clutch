using clutch_position.Entities;
using clutch_position.Requests;
using clutch_position.Resources;

namespace clutch_position.Services
{
    public interface IPositionService
    {
        Task CreatePosition(PostPositionRequest postPositionRequest);
        Task<List<PositionResource>> GetPositionsAync();
        Task<PositionResource> GetPositionAsync(string id);
        Task AmendPosition(PutPositionRequest postPositionRequest, string id);

    }
}
