using RestWithASPNETUdemy.Data.Converter.Implementations;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Hypermedia.Utils;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Repository;

namespace RestWithASPNETUdemy.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _repository;

        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public PageSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var offset = page > 0 ? (page - 1) * pageSize : 0;
            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc") ? "asc" : "desc"; 
            var size = (pageSize < 1) ? 1 : pageSize;

            string query = @" SELECT * FROM PERSON P where 1 = 1";
            if (!string.IsNullOrWhiteSpace(name)) query += $" AND P.name LIKE '%{name}%'";
            query += $" ODER BY P.firstName {sort} limit {size} offset {offset}";

            string countQuery = @" SELECT COUNT(*) FROM Person p WHERE 1 = 1";
            if (!string.IsNullOrWhiteSpace(name)) countQuery = countQuery + $" AND P.name LIKE '%{name}%'";

            var persons = _repository.FindWithPagedSearch(query);
            int totalResults = _repository.GetCount(countQuery);

            return new PageSearchVO<PersonVO> {
                CurrentPage = offset,
                List = _converter.Parse(persons),
                PageSize = size,
                SortDirections = sort,
                TotalResult = totalResults,
            };
        }

        public PersonVO FindByID(long id)
        {
            return _converter.Parse(_repository.FindByID(id));
        }

        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return _converter.Parse(_repository.FindByName(firstName, lastName));
        }

        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }

        // Method responsible for disable a person from an ID
        public PersonVO Disabled(long id)
        {
            var personEntity = _repository.Disable(id);
            return _converter.Parse(personEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        
    }
}
