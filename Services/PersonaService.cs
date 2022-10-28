using Bicode.Models;
using Microsoft.EntityFrameworkCore;

namespace Bicode.Services
{
    public class PersonaService
    {
        private readonly BI_TESTGENContext _context;

        public PersonaService(BI_TESTGENContext context)
        {
            _context = context;
        }

        public async Task<List<Persona>> GetAsync() =>
            await _context.Personas.ToListAsync();

        public async Task<Persona?> GetAsyncId(int id) =>
            await _context.Personas.FindAsync(id);
        // public async Task<Persona?> FilterNameAsync(string name) =>
        //     await _usersCollection.Find(x => x.Name == name).FirstOrDefaultAsync();
        public async Task CreateAsync(Persona newPersona)
        {  // tengo mis dudas con este  en el return 

            _context.Personas.Add(newPersona);
            await _context.SaveChangesAsync();
        }

        //     _context.Personas.Add(persona);
        //     await _context.SaveChangesAsync();

        // public async Task UpdateAsync(string id, Persona updatePersona) =>
        //     await _usersCollection.ReplaceOneAsync(x => x.UserId == id, updatePersona);
        public async Task UpdateAsync(string id, Persona updatePersona)
        {
            _context.Entry(updatePersona).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Persona persona)
        {
            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync();
        }
        // await _usersCollection.DeleteOneAsync(x => x.UserId == id);


        //         public async Task<List<User>> GetAsync() =>
        //     await _usersCollection.Find(_ => true).ToListAsync();

        // public async Task<User?> GetAsync(string id) =>
        //     await _usersCollection.Find(x => x.UserId == id).FirstOrDefaultAsync();
        // public async Task<User?> FilterNameAsync(string name) =>
        //     await _usersCollection.Find(x => x.Name == name).FirstOrDefaultAsync();
        // public async Task CreateAsync(User newUser) =>
        //     await _usersCollection.InsertOneAsync(newUser);

        // public async Task UpdateAsync(string id, User updateUser) =>
        //     await _usersCollection.ReplaceOneAsync(x => x.UserId == id, updateUser);

        // public async Task RemoveAsync(string id) =>
        //     await _usersCollection.DeleteOneAsync(x => x.UserId == id);

    }

}