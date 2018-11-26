using System;
using System.Collections.Generic;
using ThirdL.Models;

namespace ThirdL.Services
{
    public interface IModelService<T>
    {
        T Authenticate(string username, string password);
        IEnumerable<T> GetAll();
        T GetById(int id);
        T Create(T patient);
        void Update(T patient);
        void Delete(int id);
        bool CheckToken(string token);
    }
}