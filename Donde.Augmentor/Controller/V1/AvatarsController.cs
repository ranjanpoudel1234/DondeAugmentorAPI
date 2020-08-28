using AutoMapper;
using AutoMapper.QueryableExtensions;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using Donde.Augmentor.Web.ViewModels.V1;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Donde.Augmentor.Core.Domain;

namespace Donde.Augmentor.Web.Controller.V1
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("avatars")]
    [Authorize]
    public class AvatarsController : BaseController
    {
        private readonly IAvatarService _avatarService;
        private readonly IMapper _mapper;
        private readonly ILogger<AvatarsController> _logger;
        private readonly DomainSettings _domainSettings;

        public AvatarsController(IAvatarService avatarService, 
            IMapper mapper, 
            ILogger<AvatarsController> logger,
            DomainSettings domainSettings)
        {
            _avatarService = avatarService;
            _mapper = mapper;
            _logger = logger;
        }

        [ODataRoute]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(ODataQueryOptions<AvatarViewModel> odataOptions)
        {
            var result = new List<AvatarViewModel>();

            var avatarsQueryablee = _avatarService.GetAvatars();

            var projectedAudios = avatarsQueryablee.ProjectTo<AvatarViewModel>(_mapper.ConfigurationProvider);

            var appliedResults = odataOptions.ApplyTo(projectedAudios);

            var avatarViewModels = appliedResults as IQueryable<AvatarViewModel>;

            if (avatarViewModels != null)
            {
                result = await avatarViewModels.ToListAsync();
            }

            foreach (var avatar in result)
            {
                avatar.Url = GetMediaPathWithSubFolder(_domainSettings.GeneralSettings.StorageBasePath, 
                    _domainSettings.UploadSettings.AvatarFolderName,
                    avatar.OrganizationId.ToString(),
                    avatar.FileId, avatar.Extension);
            }

            return Ok(result);
        }
    }
}
