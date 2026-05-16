using CarAPI.Entities;
using CarAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarAPiTests.Fake
{
    internal class FakeExistingOwnersRepository : IOwnersRepository
    {
        public bool AddOwner(Owner owner)
        {
            throw new NotImplementedException();
        }

        public List<Owner> GetAllOwners()
        {
            throw new NotImplementedException();
        }

        public Owner? GetOwnerById(int id)
        {
            return new Owner(id, "");
        }
    }

    internal class FakeNotExistingOwnersRepository : IOwnersRepository
    {
        public bool AddOwner(Owner owner)
        {
            throw new NotImplementedException();
        }

        public List<Owner> GetAllOwners()
        {
            throw new NotImplementedException();
        }

        public Owner? GetOwnerById(int id)
        {
            return null;
        }
    }
}
