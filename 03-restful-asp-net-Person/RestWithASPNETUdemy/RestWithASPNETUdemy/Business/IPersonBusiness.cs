using RestWithASPNETUdemy.Data.VO;

namespace RestWithASPNETUdemy.Business 
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);

        PersonVO FindByID(long id);

        List<PersonVO> FindAll();

        List<PersonVO> FindByName(string firstName, string lastName);

        PersonVO Update(PersonVO person);

        PersonVO Disabled(long id);

        void Delete(long id);

        
    }
}
