using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AdventureWorksSandbox.Models;
using System.Web.Http.OData;

namespace AdventureWorksSandbox.Controllers
{
    public class PersonODataController : EntitySetController<Person, int>
    {
        private AdventureWorksSandboxContext db = new AdventureWorksSandboxContext();

        [Queryable]
        public override IQueryable<Person> Get()
        {
            return db.People;
        }

        [Queryable]
        protected override Person GetEntityByKey(int key)
        {
            return db.People.FirstOrDefault(e => e.BusinessEntityId == key);
        }


        // GET api/PersonOData
        public IEnumerable<Person> GetPerson()
        {
            return db.People.AsEnumerable();
        }

        // GET api/PersonOData/5
        public Person GetPerson(Int32 id)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return person;
        }

        // PUT api/PersonOData/5
        public HttpResponseMessage PutPerson(Int32 id, Person person)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != person.BusinessEntityId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(person).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/PersonOData
        public HttpResponseMessage PostPerson(Person person)
        {
            if (ModelState.IsValid)
            {
                db.BusinessEntities.Add(person);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, person);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = person.BusinessEntityId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/PersonOData/5
        public HttpResponseMessage DeletePerson(Int32 id)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.BusinessEntities.Remove(person);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, person);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}