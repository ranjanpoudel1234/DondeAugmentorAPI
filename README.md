DondeAugmentorAPI

i) Install Vagrant
ii) Install Virtual box Oracle
iii) Get latest of DondeDevEnvironment
iv) run vagrant provision, this should install docker.
v) vagrant up, vagrant ssh
vi) docker-compose pull
vii) docker-compose up -d. You should have docker container.

Deployment Notes
# File Upload
i) Make sure the instance gives permission to the App_Data folder for IIS. For now, we use ebExtensions folder's config file that automatically gives that permission to DefaultAppPool.

ii) Add AWS_ACCESS_KEY_ID and AWS_SECRET_ACCESS_KEY to the environment variable. This has to be same as the profile that has access to s3. Might need to be extended for more permission in future.

# RDP
i) Make sure the ec2 instance has the inbound rule in the default security group to be your ip address.

ii) Download the .pem file of the ec2 instance.

iii) Run it from visual studio.

# First Time Environment
i) When we create new environment or we deploy for the first time, remove the contents of ebExtensions file(otherwise it will error) and deploy.

ii) After that, deploy again with ebExtensions not commented, this time
 it will give the right permissions.

# Identity Certificate Generation

1) Open Cmder in admin mode

2) Type OpenSSL

3) `req -x509 -newkey rsa:4096 -sha256 -nodes -keyout augmentUDev.key -out augmentUDev.crt -subj "/CN=augmentuapp.com" -days 365`

4) `pkcs12 -export -out augmentUDev.pfx -inkey augmentUDev.key -in augmentUDev.crt`

5) augmentUDev