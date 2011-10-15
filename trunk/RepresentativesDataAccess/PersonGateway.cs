using System.Collections.Generic;
using System.Linq;

namespace RepresentativesDataAccess
{
    public class PersonGateway
    {
        public IList<IPerson> GetForRegion(int regionId)
        {
            using(var context = new RepDataDataContext())
            {
                return context
                    .Persons
                    .Where(p => p.RegionId == regionId)
                    .Cast<IPerson>()
                    .ToList();
            }
        }

        public void Update(int id, string name, string email, int? actual, int? estimated)
        {
            using (var context = new RepDataDataContext())
            {
                var person = context.Persons.Where(p => p.Id == id).First();

                person.Name = name;
                person.Actual = actual;
                person.Estimated = estimated;
                person.Email = email;

                context.SubmitChanges();
            }
            
        }

        public IPerson ById(int id)
        {
            using (var context = new RepDataDataContext())
            {
                return context.Persons.Where(p => p.Id == id).First();
            }
        }
    }
}