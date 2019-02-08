using Donde.Augmentor.Web.ViewModels;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donde.Augmentor.Web.Controller
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("audios")]
    public class AudioController : ODataController
    {
       [ODataRoute]
       [HttpGet]
        public async Task<IActionResult> Get(ODataQueryOptions<AudioViewModel> option)
        {           
            return Ok(null);
        }
    }
}
