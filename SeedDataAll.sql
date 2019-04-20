INSERT INTO public."Organizations" ("Id","Name","AddedDate","UpdatedDate","IsActive","Code","EmailAddress","Latitude","Longitude") VALUES 
('36dbc88c-79b1-4810-a688-88ef9e1d7d0b','Southeastern Louisiana University','2019-01-31 00:00:00.000','2019-01-31 00:00:00.000',true,'SELU',NULL,30.5173,-90.4689)
,('a4644de9-c4fd-469b-ae30-d7450c8b2956','Louisiana State University','2019-01-31 00:00:00.000','2019-01-31 00:00:00.000',true,'LSU',NULL,30.4133,-91.18)
;

INSERT INTO public."Audios" ("Id","Name","Url","OrganizationId","AddedDate","UpdatedDate","IsActive","MimeType") VALUES 
('8dfa5fbe-852c-4f6e-813b-4dd7dae4692c','NationalAnthem','https://s3.amazonaws.com/elasticbeanstalk-us-east-1-750996182703/DondeAugmentorAudios/nationalAnthem.mp3','36dbc88c-79b1-4810-a688-88ef9e1d7d0b','2019-01-31 00:00:00.000','2019-01-31 00:00:00.000',true,NULL)
,('5196c027-2df3-4797-8621-02363a91ee96','WallonLilli','https://s3.amazonaws.com/elasticbeanstalk-us-east-1-750996182703/DondeAugmentorAudios/WalloonLilliShort.mp3','36dbc88c-79b1-4810-a688-88ef9e1d7d0b','2019-01-31 00:00:00.000','2019-01-31 00:00:00.000',true,NULL)
;

INSERT INTO public."AugmentImages" ("Id","Name","Url","OrganizationId","AddedDate","UpdatedDate","IsActive","MimeType") VALUES 
('78ea76ee-3822-413b-abc3-81128c8aa0f8','BlackPantherImage','https://s3.amazonaws.com/elasticbeanstalk-us-east-1-750996182703/DondeAugmentorImages/blackpanther.jpg','36dbc88c-79b1-4810-a688-88ef9e1d7d0b','2019-01-31 00:00:00.000','2019-01-31 00:00:00.000',true,NULL)
,('89ea76ee-3822-413b-abc3-81128c8aa0f9','SaintsLogo','https://s3.amazonaws.com/elasticbeanstalk-us-east-1-750996182703/DondeAugmentorImages/saintslogo.png','36dbc88c-79b1-4810-a688-88ef9e1d7d0b','2019-01-31 00:00:00.000','2019-01-31 00:00:00.000',true,NULL)
,('117d6aca-30c3-46b5-9bc1-8cdc21e828c4','SimsLibrary','https://s3.amazonaws.com/elasticbeanstalk-us-east-1-750996182703/DondeAugmentorImages/SimsLibrary_SELU-small.png','36dbc88c-79b1-4810-a688-88ef9e1d7d0b','2019-01-31 00:00:00.000','2019-01-31 00:00:00.000',true,NULL)
,('bed210b3-a336-4e62-86dc-272189bd64a2','FayardHall','https://s3.amazonaws.com/elasticbeanstalk-us-east-1-750996182703/DondeAugmentorImages/fayardHall.jpg','36dbc88c-79b1-4810-a688-88ef9e1d7d0b','2019-01-31 00:00:00.000','2019-01-31 00:00:00.000',true,NULL)
,('cb5fb12d-158e-4159-8685-1a885aa2aaa9','sluLogo','https://s3.amazonaws.com/elasticbeanstalk-us-east-1-750996182703/DondeAugmentorImages/sluLogo.png','36dbc88c-79b1-4810-a688-88ef9e1d7d0b','2019-01-31 00:00:00.000','2019-01-31 00:00:00.000',true,NULL)
,('4894ce42-066f-47c2-9b0f-6f6bba227e8d','lsuLogo','https://s3.amazonaws.com/elasticbeanstalk-us-east-1-750996182703/DondeAugmentorImages/lsuLogo.jpg','a4644de9-c4fd-469b-ae30-d7450c8b2956','2019-01-31 00:00:00.000','2019-01-31 00:00:00.000',true,NULL)
,('ea5e55df-5b8d-4592-b7ff-7f12e9e61e2f','lsuFrontGate','https://s3.amazonaws.com/elasticbeanstalk-us-east-1-750996182703/DondeAugmentorImages/lsuFrontGate.jpg','a4644de9-c4fd-469b-ae30-d7450c8b2956','2019-01-31 00:00:00.000','2019-01-31 00:00:00.000',true,NULL)
,('3dd23eaf-5f0c-4177-8232-89eafc65ff83','lsuTigerStadium','https://s3.amazonaws.com/elasticbeanstalk-us-east-1-750996182703/DondeAugmentorImages/lsuTigerStadium.jpg','a4644de9-c4fd-469b-ae30-d7450c8b2956','2019-01-31 00:00:00.000','2019-01-31 00:00:00.000',true,NULL)
;

INSERT INTO public."Avatars" ("Id","Name","Url","OrganizationId","AddedDate","UpdatedDate","IsActive") VALUES 
('f29e9a8e-28a7-4d38-b80c-7cd6bda8842c','Black Panther VRX','https://s3.amazonaws.com/elasticbeanstalk-us-east-1-750996182703/DondeAugmentorAvatars/blackpanther/object_bpanther_anim.vrx','36dbc88c-79b1-4810-a688-88ef9e1d7d0b','2019-01-31 00:00:00.000','2019-01-31 00:00:00.000',true)
;

INSERT INTO public."AugmentObjects" ("Id","AvatarId","AudioId","AugmentImageId","Description","Latitude","Longitude","OrganizationId","AddedDate","UpdatedDate","IsActive","Title","VideoId") VALUES 
('c04c64d1-c07f-41e8-89a4-a0df853dfcce','f29e9a8e-28a7-4d38-b80c-7cd6bda8842c','8dfa5fbe-852c-4f6e-813b-4dd7dae4692c','78ea76ee-3822-413b-abc3-81128c8aa0f8','Black Panther Image Description',NULL,NULL,'36dbc88c-79b1-4810-a688-88ef9e1d7d0b','2019-01-31 00:00:00.000','2019-01-31 00:00:00.000',true,'BlackPantherImageTItle',NULL)
,('4b2531af-80ea-491b-9718-f15fa81ce468','f29e9a8e-28a7-4d38-b80c-7cd6bda8842c','5196c027-2df3-4797-8621-02363a91ee96','89ea76ee-3822-413b-abc3-81128c8aa0f9','Saints Logo Image Description',NULL,NULL,'36dbc88c-79b1-4810-a688-88ef9e1d7d0b','2019-01-31 00:00:00.000','2019-01-31 00:00:00.000',true,'SaintsLogoImageTitle',NULL)
,('44fedca0-6050-45d2-976a-b4b54199149d',NULL,NULL,'117d6aca-30c3-46b5-9bc1-8cdc21e828c4','SELU Sims Library is an awesome place to find cool book  and study around.',NULL,NULL,'36dbc88c-79b1-4810-a688-88ef9e1d7d0b','2019-01-31 00:00:00.000','2019-01-31 00:00:00.000',true,'Selu SIMS Library','d9fd78c2-bfeb-4ed0-85be-5b866b8657d2')
,('d0f24e8f-56d6-42ad-9167-645c4503eecc',NULL,NULL,'bed210b3-a336-4e62-86dc-272189bd64a2','Selu fayard hall houses tons of computer science and other classes. It also hosts the Integration bee competition and all.',NULL,NULL,'36dbc88c-79b1-4810-a688-88ef9e1d7d0b','2019-01-31 00:00:00.000','2019-01-31 00:00:00.000',true,'Selu Fayard hall','ac920713-4649-47e1-a07e-c637071b77d4')
;


--delete from public."AugmentObjects";
--delete from public."Audios";
--delete from public."Avatars";
--delete from public."AugmentImages";
--delete from public."Organizations";
--delete from public."Users";