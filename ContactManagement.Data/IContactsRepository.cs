using ContactManagement.Models;

namespace ContactManagement.Data
{
    public interface IContactsRepository
    {
        List<Contact> GetContacts();
        Contact GetContact(int id);
        void AddContact(Contact contact);
        void UpdateContact(Contact contact);
        void DeleteContact(int id);
    }
}
