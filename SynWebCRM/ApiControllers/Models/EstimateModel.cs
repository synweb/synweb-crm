using SynWebCRM.Data;

namespace SynWebCRM.ApiControllers.Models
{
    public class EstimateModel : Estimate
    {
        public override Deal Deal => null;
    }
}