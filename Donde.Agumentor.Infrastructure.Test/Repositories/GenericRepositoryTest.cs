using System;
using System.Linq;
using System.Threading.Tasks;
using Donde.Augmentor.Core.Domain.DataBuilder;
using Donde.Augmentor.Core.Domain.Helpers;
using Donde.Augmentor.Core.Domain.Interfaces;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces;
using Donde.Augmentor.Infrastructure.Database;
using Donde.Augmentor.Infrastructure.DataBuilder;
using Donde.Augmentor.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Donde.Agumentor.Infrastructure.Test.Repositories
{
    [TestClass]
    public class GenericRepositoryTest : BaseTest
    {
        [TestMethod]
        public void GetAll_ReturnsIQueryableOfEntity()
        {
            using (var context = GetDondeContext())
            {
                new DondeAugmentorDataBuilder().AddAugmentObject().ApplyTo(context);

                var defaultRepository = DefaultGenericRepository(context);

                var result = defaultRepository.GetAllAsNoTracking<AugmentObject>();

                result.ShouldNotBeNull();
                result.ShouldBeAssignableTo<IQueryable<AugmentObject>>();
                result.Count().ShouldBe(1);
            }
        }

        [TestMethod]
        public void GetAll_WithNoResult_ReturnsIQueryableOfEmptyEntity()
        {
            using (var context = GetDondeContext())
            {           
                var defaultRepository = DefaultGenericRepository(context);

                var result = defaultRepository.GetAllAsNoTracking<AugmentObject>();

                result.ShouldNotBeNull();
                result.ShouldBeAssignableTo<IQueryable<AugmentObject>>();
                result.Count().ShouldBe(0);
            }
        }

        [TestMethod]
        public async Task GetByIdAsync_WithMatchingId_ReturnsEntity()
        {
            using (var context = GetDondeContext())
            {
                var augmentObjectId = SequentialGuidGenerator.GenerateComb();
                var audioId = SequentialGuidGenerator.GenerateComb();

                var dondeAugmentorDataBuilder = new DondeAugmentorDataBuilder();
                dondeAugmentorDataBuilder
                    .AddAugmentObject(dondeAugmentorDataBuilder.MakeAugmentObject(augmentObjectId, audioId: audioId))
                    .ApplyTo(context);

                var defaultRepository = DefaultGenericRepository(context);

                var result = await defaultRepository.GetByIdAsync<AugmentObject>(augmentObjectId);

                result.ShouldNotBeNull();
                //result.AudioId.ShouldBe(audioId);                        
            }
        }

        [TestMethod]
        public async Task GetByIdAsync_WithNoMatchingId_ReturnsEmptyEntity()
        {
            using (var context = GetDondeContext())
            {
                var augmentObjectId = SequentialGuidGenerator.GenerateComb();
                var audioId = SequentialGuidGenerator.GenerateComb();

                var dondeAugmentorDataBuilder = new DondeAugmentorDataBuilder();
                dondeAugmentorDataBuilder
                    .AddAugmentObject(dondeAugmentorDataBuilder.MakeAugmentObject(augmentObjectId, audioId: audioId))
                    .ApplyTo(context);

                var defaultRepository = DefaultGenericRepository(context);

                var result = await defaultRepository.GetByIdAsync<AugmentObject>(SequentialGuidGenerator.GenerateComb());

                result.ShouldBeNull();
            }
        }

        [TestMethod]
        public async Task UpdateAsync_WithValidEntity_AddsEntity()
        {
            using (var context = GetDondeContext())
            {
                var augmentObjectId = SequentialGuidGenerator.GenerateComb();
                var audioId = SequentialGuidGenerator.GenerateComb();

                var dondeAugmentorDataBuilder = new DondeAugmentorDataBuilder();
                var augmentObject = dondeAugmentorDataBuilder.MakeAugmentObject(augmentObjectId, audioId: audioId);
                dondeAugmentorDataBuilder
                    .AddAugmentObject(augmentObject)
                    .ApplyTo(context);

                var updatedObject = new AugmentObject
                {
                    Id = augmentObjectId,
                    UpdatedDate = DateTime.Parse("2018/01/28")
                };

                var defaultRepository = DefaultGenericRepository(context);

                var result = await defaultRepository.UpdateAsync(augmentObjectId, updatedObject);

                result.ShouldNotBeNull();
                var updatedEntity = context.AugmentObjects.FirstOrDefault(x => x.Id == augmentObjectId);

                updatedEntity.ShouldNotBeNull();            
            }
        }

        [TestMethod]
        public async Task CreateAsync_WithValidEntity_AddsEntity()
        {
            using (var context = GetDondeContext())
            {
                var augmentObjectId = SequentialGuidGenerator.GenerateComb();
                var audioId = SequentialGuidGenerator.GenerateComb();

                var dondeAugmentorDataBuilder = new DondeAugmentorDataBuilder();
                var newAugmentObject = dondeAugmentorDataBuilder.MakeAugmentObject(augmentObjectId, audioId: audioId);

                var defaultRepository = DefaultGenericRepository(context);

                var result = await defaultRepository.CreateAsync(newAugmentObject);

                result.ShouldNotBeNull();
                context.AugmentObjects.Count().ShouldBe(1);
            }
        }

        private IGenericRepository DefaultGenericRepository(DondeContext dbContext)
        {
            return new GenericRepository(dbContext);
        }
    }
}
