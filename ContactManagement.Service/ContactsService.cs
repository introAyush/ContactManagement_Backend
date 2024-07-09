using ContactManagement.Data;
using ContactManagement.Models;

namespace ContactManagement.Service
{

    public class ContactsService
    {
        private readonly IContactsRepository _contactsRepository;

        public ContactsService(IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository;
        }

        public List<Contact> GetContacts()
        {
            return _contactsRepository.GetContacts();
        }

        public Contact GetContact(int id)
        {
            return _contactsRepository.GetContact(id);
        }

        public void AddContact(Contact contact)
        {
            var contacts = _contactsRepository.GetContacts();
            if (contacts.Any(c => c.Email == contact.Email))
            {
                throw new System.Exception("A contact with this email already exists.");
            }

            _contactsRepository.AddContact(contact);
        }

        public void UpdateContact(Contact contact)
        {
            var contacts = _contactsRepository.GetContacts();
            var existingContact = contacts.FirstOrDefault(c => c.Id == contact.Id);
            if (existingContact == null)
            {
                throw new System.Exception("Contact not found.");
            }

            if (contacts.Any(c => c.Email == contact.Email && c.Id != contact.Id))
            {
                throw new System.Exception("A contact with this email already exists.");
            }

            _contactsRepository.UpdateContact(contact);
        }

        public void DeleteContact(int id)
        {
            var contact = _contactsRepository.GetContact(id);
            if (contact == null)
            {
                throw new System.Exception("Contact not found.");
            }

            _contactsRepository.DeleteContact(id);
        }
    }
}
