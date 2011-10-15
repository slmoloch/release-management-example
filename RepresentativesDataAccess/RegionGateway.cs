using System.Collections.Generic;
using System.Linq;

namespace RepresentativesDataAccess
{
    public class RegionGateway
    {
        public IList<IRegion> GetAll()
        {
            using (var context = new RepDataDataContext())
            {
                return context
                    .Regions
                    .Cast<IRegion>()
                    .ToList();
            }
        }
    }
}
