namespace Donde.Augmentor.Web.ViewModels
{
    /// <summary>
    /// Improvement, ideally this should be similar to post with AugmentObjectMedia and AugmentObjectLocation being
    /// the children properties. For that to happen, we will have to update the get queries(which will also be an improvement)
    /// to get the augmentObjects first and then to hydrate media and location information if needed.
    /// </summary>
    public class GeographicalAugmentObjectsViewModel : AugmentObjectViewModel
    {
        public double Distance { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
