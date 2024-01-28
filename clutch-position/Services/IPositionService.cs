using clutch_position.Models;
using clutch_position.Requests;
using clutch_position.Resources;

namespace clutch_position.Services
{
    public interface IPositionService
    {
        Task<Position> CreatePosition(PostPositionRequest postPositionRequest);
        Task<List<PositionResource>> GetPositionsAync();
        Task<PositionResource> GetPositionAsync(string id);
        Task<Position> AmendPosition(PutPositionRequest postPositionRequest, string id);

    }
}
