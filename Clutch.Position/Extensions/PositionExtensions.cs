using clutch_position.Data;
using clutch_position.Entities;
using clutch_position.Requests;
using clutch_position.Resources;
using Org.BouncyCastle.Asn1.Ocsp;

namespace clutch_position.Extensions
{
    public static class PositionExtensions
    {
        public static Position ToAddPositionRequest(this PostPositionRequest request)
        {
            return new Position
            {
                PositionName = request.PositionName,
                PositionDescription = request.PositionDescription,
                Salary = request.Salary,
                PositionStatus = PositionStatus.Empty
            };
        }

        public static Position FromAddPositionRequest(this PostPositionRequest request)
        {
            return new Position
            {
                PositionName = request.PositionName,
                PositionDescription = request.PositionDescription,
                Salary = request.Salary,
                PositionStatus = (PositionStatus)Enum.Parse(typeof(PositionStatus), request.PositionStatus)
            };
        }

        public static Position ToAmendPositionRequest(this PutPositionRequest request, Guid id)
        {
            return new Position
            {
                PositionName = request.PositionName,
                PositionDescription = request.PositionDescription,
                Salary = request.Salary,
                PositionStatus = (PositionStatus)Enum.Parse(typeof(PositionStatus), request.PositionStatus)
            };
        }

        public static List<PositionResource> ToPositionResources(this List<Position> positions)
        {
            return positions.Select(position => new PositionResource
            {
                Id = position.Id,
                PositionName = position.PositionName,
                PositionDescription = position.PositionDescription,
                Salary = position.Salary,
                PositionStatus = position.PositionStatus.ToString()
            }
            ).ToList();
        }

        public static PositionResource ToPositionResource(this Position position)
        {
            return new PositionResource
            {
                Id = position.Id,
                PositionName = position.PositionName,
                PositionDescription = position.PositionDescription,
                Salary = position.Salary,
                PositionStatus = position.PositionStatus.ToString()
            };
        }
    }
}
