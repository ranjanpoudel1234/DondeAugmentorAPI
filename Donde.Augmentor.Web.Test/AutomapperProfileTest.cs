using AutoMapper;
using Donde.Augmentor.Web.ViewModels.V1.AugmentObject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Donde.Augmentor.Web.Test
{
    [TestClass]
    public class AutomapperProfileTest
    {
        [TestMethod]
        public void TestAutomapperProfiles()
        {
            var assembliesToBootstrapFrom = new List<Assembly>
            {
                typeof(AugmentObjectViewModel).Assembly,
                typeof(Core.Services.Services.AudioService).Assembly
            };

            var profiles = assembliesToBootstrapFrom.SelectMany(x => x.GetTypes())
                                                    .Where(x => typeof(AutoMapper.Profile).IsAssignableFrom(x));

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(Activator.CreateInstance(profile) as AutoMapper.Profile);
                }
            });

            config.AssertConfigurationIsValid();
        }
    }    
}
