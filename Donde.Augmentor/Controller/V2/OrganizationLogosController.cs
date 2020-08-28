using AutoMapper;
using Donde.Augmentor.Core.Domain;
using Donde.Augmentor.Core.Domain.CustomExceptions;
using Donde.Augmentor.Core.Domain.Enum;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces;
using Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces.IFileService;
using Donde.Augmentor.Web.Attributes;
using Donde.Augmentor.Web.OData;
using Donde.Augmentor.Web.ViewModels.V2.Organization;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;

namespace Donde.Augmentor.Web.Controller.V2
{
    [ApiVersion("2.0")]
    [Authorize]
    [ODataRoutePrefix(ODataConstants.OrganizationLogoRoute)]
    public class OrganizationLogosController : BaseController
    {
        private readonly IOrganizationService _organizationService;
        private readonly IMapper _mapper;
        private readonly IFileProcessingService _fileProcessingService;
        private readonly ILogger<OrganizationLogosController> _logger;
        public readonly DomainSettings _domainSettings;

        public OrganizationLogosController(IOrganizationService organizationService, 
            IMapper mapper,
            IFileProcessingService fileProcessingService, 
            ILogger<OrganizationLogosController> logger,
            DomainSettings domainSettings)
        {
            _organizationService = organizationService;
            _fileProcessingService = fileProcessingService;
            _mapper = mapper;
            _logger = logger;
            _domainSettings = domainSettings;
        }

        [ODataRoute]
        [HttpPost]
        [DisableFormValueModelBinding]
        [RequestSizeLimit(15728640)] // 15 mb
        public async Task<IActionResult> Upload()
        {
            //todo Add authorization check here after user/role etc is added.
            var organizationId = GetCurrentOrganizationIdOrThrow();

            var existingOrganization = await _organizationService.GetOrganizationByIdAsync(organizationId);
            if (existingOrganization == null)
            {
                throw new HttpNotFoundException(ErrorMessages.ObjectNotFound);
            }

            var fileUploadResult = await _fileProcessingService.UploadMediaAsync(Request, MediaTypes.Logo, organizationId);

            if (fileUploadResult.IsFailure)
            {
                _logger.LogError($"Error on file Upload {JsonConvert.SerializeObject(fileUploadResult)}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

            var organization = _mapper.Map(fileUploadResult.Value, existingOrganization);

            var updatedOrganization = await _organizationService.UpdateOrganizationAsync(organizationId, organization);

            var organizationViewModel = _mapper.Map<OrganizationViewModel>(updatedOrganization);

            organizationViewModel.Logo.ThumbnailUrl = GetMediaPath(_domainSettings.GeneralSettings.StorageBasePath,
              _domainSettings.UploadSettings.LogosFolderName, organizationViewModel.Logo.FileId, organizationViewModel.Logo.FileExtension);

            organizationViewModel.Logo.Url = GetMediaPathWithSubFolder(_domainSettings.GeneralSettings.StorageBasePath,
           _domainSettings.UploadSettings.LogosFolderName, _domainSettings.UploadSettings.OriginalImageSubFolderName, organizationViewModel.Logo.FileId, organizationViewModel.Logo.FileExtension);

            return Created(organizationViewModel);
        }
    }
}
