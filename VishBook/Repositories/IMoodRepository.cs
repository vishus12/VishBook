using System.Collections.Generic;
using VishBook.Controllers;
using VishBook.Models;

namespace VishBook.Repositories
{
    public interface IMoodRepository
    {
        public List<Mood> GetAllMood();

        Mood GetMoodById(int id);
    }
}
