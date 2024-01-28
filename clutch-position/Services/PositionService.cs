using clutch_position.Data;
using clutch_position.Data.Contexts;
using clutch_position.Exceptions;
using clutch_position.Extensions;
using clutch_position.Models;
using clutch_position.Requests;
using clutch_position.Resources;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace clutch_position.Services
{
    public class PositionService : IPositionService
    {
        private readonly PositionDbContext positionDbContext;
        public PositionService(PositionDbContext positionDbContext)
        {
            this.positionDbContext = positionDbContext ?? throw new ArgumentException();
        }

        public async Task<Position> CreatePosition(PostPositionRequest request)
        {
            var existingPosition = positionDbContext.Positions.SingleOrDefault(emp => emp.Id.ToString() == request.Id.ToString());

            if (existingPosition != null)
            {
                throw new DuplicateException($"Position with id {existingPosition.Id} already exists");
            }
            try
            {
                var newPosition = request.ToAddPositionRequest();
                await positionDbContext.Positions.AddAsync(request.ToAddPositionRequest());
                await positionDbContext.SaveChangesAsync();
                return newPosition;
            }
            catch (Exception ex)
            {
                var errMsg = "Unable to create Position";
                Log.Error(errMsg, ex);
                throw new Exception(errMsg, ex);
            }
        }

        public async Task<Position> AmendPosition(PutPositionRequest request, string id)
        {
            var existingPosition = await positionDbContext.Positions.FirstOrDefaultAsync(x => x.Id.ToString() == id);
            if (existingPosition == null)
            {
                var errMsg = $"Position with id {id} does not exist";
                throw new NotFoundException(errMsg);
            }
            try
            {
                existingPosition.PositionName = request.PositionName;
                existingPosition.PositionDescription = request.PositionDescription;
                existingPosition.PositionStatus = (PositionStatus)Enum.Parse(typeof(PositionStatus), request.PositionStatus);
                existingPosition.Salary = request.Salary;
                positionDbContext.SaveChanges();
                return existingPosition;
            }
            catch (Exception ex)
            {
                var errMsg = "Unable to amend position";
                Log.Error(errMsg, ex);
                throw new Exception(errMsg, ex);
            }

        }

        public async Task<List<PositionResource>> GetPositionsAync()
        {
            try
            {
                var positions = await positionDbContext.Positions.ToListAsync();
                return positions.ToPositionResources();
            }
            catch (Exception ex)
            {
                var errMsg = "Unable to get position";
                Log.Error(errMsg, ex);
                throw new Exception(errMsg, ex);
            }
        }

        public async Task<PositionResource> GetPositionAsync(string id)
        {
            try
            {
                var position = await positionDbContext.Positions.FirstOrDefaultAsync(emp => emp.Id.ToString() == id);
                if (position == null)
                {
                    var errMsg = $"Empployee with id {id} does not exist";
                    throw new NotFoundException(errMsg);
                }
                return position.ToPositionResource();
            }
            catch (NotFoundException ex)
            {
                throw new HttpRequestException(ex.Message, ex, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                Log.Error("Unable to get position", ex);
                throw new HttpRequestException(ex.Message, ex, HttpStatusCode.InternalServerError);
            }
        }
    }
}
