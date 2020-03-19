using Dapper;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Infrastructure.Repositories
{
    public class OrganizationRepository : GenericRepository, IOrganizationRepository
    {
        public OrganizationRepository(DondeContext dbContext) : base(dbContext)
        {

        }

        public async Task<Organization> CreateOrganizationAsync(Organization entity)
        {
            return await CreateAsync(entity);
        }

        public async Task<Organization> UpdateOrganizationAsync(Organization entity)
        {
            return await UpdateAsync(entity.Id, entity);
        }

        public IQueryable<Organization> GetOrganizations()
        {
            return GetAll<Organization>();
        }

        public async Task<IEnumerable<Organization>> GetClosestOrganizationByRadius(double latitude, double longitude, int radiusInMeters)
        {
            string objectsByDistanceQuery = $@"with OrganizationWithDistance as 

                    (
                        SELECT 
                        st_distance(ST_Transform(CONCAT('SRID=4326;POINT(',""Longitude"",' ', ""Latitude"",')')::geometry, 3857), ST_Transform('SRID=4326;POINT({longitude} {latitude})':: geometry, 3857)) as Distance,
                        org.""Id"",                     
                        org.""Name"",
                        org.""IsActive"",
                        org.""Code"",
                        org.""EmailAddress"",
                        org.""Latitude"",
                        org.""Longitude""
                                                   
                        from ""Organizations"" org
                    )

                    SELECT 
                        *
                    FROM 
                        OrganizationWithDistance
                    WHERE
                        Distance < @RadiusInMeters
                    ORDER BY
                        Distance";

            var connection = _dbContext.Database.GetDbConnection();

            var result = await connection.QueryAsync<Organization>
            (
                objectsByDistanceQuery,
                new
                {
                    Longitude = longitude,
                    Latitude = latitude,
                    RadiusInMeters = radiusInMeters
                }
            );

            return result;
        }
    }
}