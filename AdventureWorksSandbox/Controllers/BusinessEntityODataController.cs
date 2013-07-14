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
    public class BusinessEntityODataController : EntitySetController<BusinessEntity, int>
    {
        private AdventureWorksSandboxContext db = new AdventureWorksSandboxContext();

        public override IQueryable<BusinessEntity> Get()
        {
            return db.BusinessEntities;
        }

        protected override BusinessEntity GetEntityByKey(int key)
        {
            return db.BusinessEntities.FirstOrDefault(e => e.BusinessEntityId == key);
        }



        // GET api/BusinessEntityOData
        public IEnumerable<BusinessEntity> GetBusinessEntity()
        {
            return db.BusinessEntities.AsEnumerable();
        }

        // GET api/BusinessEntityOData/5
        public BusinessEntity GetBusinessEntity(Int32 id)
        {
            BusinessEntity businessentity = db.BusinessEntities.Find(id);
            if (businessentity == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return businessentity;
        }

        // PUT api/BusinessEntityOData/5
        public HttpResponseMessage PutBusinessEntity(Int32 id, BusinessEntity businessentity)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != businessentity.BusinessEntityId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(businessentity).State = EntityState.Modified;

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

        // POST api/BusinessEntityOData
        public HttpResponseMessage PostBusinessEntity(BusinessEntity businessentity)
        {
            if (ModelState.IsValid)
            {
                db.BusinessEntities.Add(businessentity);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, businessentity);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = businessentity.BusinessEntityId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/BusinessEntityOData/5
        public HttpResponseMessage DeleteBusinessEntity(Int32 id)
        {
            BusinessEntity businessentity = db.BusinessEntities.Find(id);
            if (businessentity == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.BusinessEntities.Remove(businessentity);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, businessentity);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}