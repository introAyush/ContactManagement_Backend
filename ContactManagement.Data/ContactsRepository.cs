using ContactManagement.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.Data
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "contacts.json");

        public List<Contact> GetContacts()
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
            var jsonData = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<Contact>>(jsonData);
        }

        public Contact GetContact(int id)
        {
            return GetContacts().FirstOrDefault(c => c.Id == id);
        }

        public void AddContact(Contact contact)
        {
            var contacts = GetContacts();
            contact.Id = contacts.Any() ? contacts.Max(c => c.Id) + 1 : 1;
            contacts.Add(contact);
            File.WriteAllText(_filePath, JsonConvert.SerializeObject(contacts));
        }

        public void UpdateContact(Contact contact)
        {
            var contacts = GetContacts();
            var existingContact = contacts.FirstOrDefault(c => c.Id == contact.Id);
            if (existingContact != null)
            {
                existingContact.FirstName = contact.FirstName;
                existingContact.LastName = contact.LastName;
                existingContact.Email = contact.Email;
                File.WriteAllText(_filePath, JsonConvert.SerializeObject(contacts));
            }
        }

        public void DeleteContact(int id)
        {
            var contacts = GetContacts();
            var contact = contacts.FirstOrDefault(c => c.Id == id);
            if (contact != null)
            {
                contacts.Remove(contact);
                File.WriteAllText(_filePath, JsonConvert.SerializeObject(contacts));
            }
        }
    }
}
