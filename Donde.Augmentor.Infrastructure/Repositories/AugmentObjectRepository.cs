using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Infrastructure.Repositories
{
    public class AugmentObjectRepository : GenericRepository<AugmentObject>, IAugmentObjectRepository
    {
        public AugmentObjectRepository(DondeContext dbContext) : base(dbContext)
        {

        }

        public async Task<AugmentObject> GetAugmentObjectByImageId(Guid imageId)
        {
            return await GetAll().Where(ao => ao.AugmentImageId == imageId).FirstOrDefaultAsync();
        }

//        SELECT "Id", "AvatarId", "AudioId", "AugmentImageId", "Description", "Latitude", "Longitude", "OrganizationId", "AddedDate", "UpdatedDate", "IsActive"
//FROM public."AugmentObjects";

//with index_query as (
//  select
//    st_distance('SRID=4326;POINT('public."Latitude" public."Longitude"')'::geometry, 'SRID=3005;POINT(1011102 450541)') as distance
//    from public."AugmentObjects"
//     order by geom<#> 'SRID=4326;POINT(-72.1235 42.3521)' limit 100
//)
//select* from index_query order by distance limit 10;

//--slidell and covington
//SELECT ST_Distance(
//		'SRID=4326;POINT(30.4755 90.1009)'::geometry,
//		'SRID=4326;POINT(30.2752 89.7812)'::geometry

//    );

//        select
//            st_distance(CONCAT('SRID=4326;POINT(',"Latitude",' ', "Longitude",')')::geometry, 'SRID=4326;POINT(1011102 450541)') as distance
//            from public."AugmentObjects"
    
    
CREATE EXTENSION postgis;
    }
}
