namespace Donde.Augmentor.Web.OData
{
    public class ODataConstants
    {
        public const int MaximumTopAllowed = 100;
        public const string OrganizationRoute = "organizations";
        public const string OrganizationLogoRoute = "organizationLogos";
        public const string SitesRoute = "sites";
        public const string UsersRoute = "users";
        public const string RolesRoute = "roles";
        public const string PermissionsRoute = "permissions";
        public const string AugmentObjectsRoute = "augmentObjects";// this one is for v1. v2 endpoints are being renamed to use target prefix.
        public const string TargetsRoute = "targets";
        public const string AugmentObjectVisitMetricRoute = "augmentObjectVisitMetrics"; // this one and one below this are for v1. v2 endpoints are being renamed to use target prefix.
        public const string AugmentObjectMediaVisitMetricRoute = "augmentObjectMediaVisitMetrics";
        public const string TargetMediaVisitMetricRoute = "targetMediaVisitMetrics";
    }
}
