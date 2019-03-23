INSERT INTO public."Organizations" ("Id","Name","AddedDate","UpdatedDate","IsActive") VALUES 
('36dbc88c-79b1-4810-a688-88ef9e1d7d0b','Southeastern Louisiana University','2019-01-31 00:00:00.000','2019-01-31 00:00:00.000',true)
;

INSERT INTO public."Audios" ("Id","Name","Url","OrganizationId","AddedDate","UpdatedDate","IsActive") VALUES 
('8dfa5fbe-852c-4f6e-813b-4dd7dae4692c','NationalAnthem','https://s3.amazonaws.com/elasticbeanstalk-us-east-1-750996182703/DondeAugmentorAudios/nationalAnthem.mp3','36dbc88c-79b1-4810-a688-88ef9e1d7d0b','2019-01-31 00:00:00.000','2019-01-31 00:00:00.000',true)
;

INSERT INTO public."AugmentImages" ("Id","Name","Url","OrganizationId","AddedDate","UpdatedDate","IsActive") VALUES 
('78ea76ee-3822-413b-abc3-81128c8aa0f8','BlackPantherImage','https://s3.amazonaws.com/elasticbeanstalk-us-east-1-750996182703/DondeAugmentorImages/blackpanther.jpg','36dbc88c-79b1-4810-a688-88ef9e1d7d0b','2019-01-31 00:00:00.000','2019-01-31 00:00:00.000',true)
,('89ea76ee-3822-413b-abc3-81128c8aa0f9','SaintsLogo','https://s3.amazonaws.com/elasticbeanstalk-us-east-1-750996182703/DondeAugmentorImages/saintslogo.png','36dbc88c-79b1-4810-a688-88ef9e1d7d0b','2019-01-31 00:00:00.000','2019-01-31 00:00:00.000',true)
;

INSERT INTO public."Avatars" ("Id","Name","Url","OrganizationId","AddedDate","UpdatedDate","IsActive") VALUES 
('f29e9a8e-28a7-4d38-b80c-7cd6bda8842c','Black Panther VRX','https://s3.amazonaws.com/elasticbeanstalk-us-east-1-750996182703/DondeAugmentorAvatars/blackpanther/object_bpanther_anim.vrx','36dbc88c-79b1-4810-a688-88ef9e1d7d0b','2019-01-31 00:00:00.000','2019-01-31 00:00:00.000',true)
;

INSERT INTO public."AugmentObjects" ("Id","AvatarId","AudioId","AugmentImageId","Description","Latitude","Longitude","OrganizationId","AddedDate","UpdatedDate","IsActive", "Title") VALUES 
('c04c64d1-c07f-41e8-89a4-a0df853dfcce','f29e9a8e-28a7-4d38-b80c-7cd6bda8842c','8dfa5fbe-852c-4f6e-813b-4dd7dae4692c','78ea76ee-3822-413b-abc3-81128c8aa0f8','This is description of augment object 1',30.460545,-90.055179,'36dbc88c-79b1-4810-a688-88ef9e1d7d0b','2019-01-31 00:00:00.000','2019-01-31 00:00:00.000',true,'AugmentObjectTitle1')
,('4b2531af-80ea-491b-9718-f15fa81ce468','f29e9a8e-28a7-4d38-b80c-7cd6bda8842c','8dfa5fbe-852c-4f6e-813b-4dd7dae4692c','78ea76ee-3822-413b-abc3-81128c8aa0f8','This is description of augment object 2',30.459139,-90.053741,'36dbc88c-79b1-4810-a688-88ef9e1d7d0b','2019-01-31 00:00:00.000','2019-01-31 00:00:00.000',true,'AugmentObjectTitle2')
;