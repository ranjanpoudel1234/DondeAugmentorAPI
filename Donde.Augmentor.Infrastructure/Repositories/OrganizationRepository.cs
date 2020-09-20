using Dapper;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
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

        public IQueryable<Organization> GetOrganizations(bool includeSites = false)
        {
            if (includeSites)
            {
                return GetAllAsNoTracking<Organization>().Include(x => x.Sites);
            }

            return GetAllAsNoTracking<Organization>();
        }

        public Task<Organization> GetOrganizationByIdAsync(Guid organizationId, bool includeSites = false)
        {
            if (includeSites)
            {
                return _dbContext.Organizations.Include(o => o.Sites).SingleOrDefaultAsync(o => o.Id == organizationId);
            }

            return GetByIdAsync<Organization>(organizationId);
        }

        public IQueryable<Organization> GetOrganizationByIds(List<Guid> organizationIds)
        {
            return GetAllAsNoTracking<Organization>().Where(x => organizationIds.Contains(x.Id));
        }

        public async Task<IEnumerable<Organization>> GetClosestOrganizationByRadius(double latitude, double longitude, int radiusInMeters)
        {
            string objectsByDistanceQuery = $@"with OrganizationWithDistance as 

                    (
                        SELECT 
                        st_distance(ST_Transform(CONCAT('SRID=4326;POINT(',""Longitude"",' ', ""Latitude"",')')::geometry, 3857), ST_Transform('SRID=4326;POINT({longitude} {latitude})':: geometry, 3857)) as Distance,
                        org.""Id"",                     
                        org.""Name"",
                        org.""IsDeleted"",
                        org.""Code"",
                        org.""EmailAddress"",
                        org.""Latitude"",
                        org.""Longitude""
                                                   
                        from ""Organizations"" org
                    )

                    SELECT 
                        *
                    FROM 
                        OrganizationWithDistance organization
                    WHERE
                        organization.Distance < @RadiusInMeters
                        and organization.""IsDeleted"" = false
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