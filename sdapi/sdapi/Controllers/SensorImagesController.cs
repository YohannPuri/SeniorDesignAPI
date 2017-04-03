using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using sdapi.Models;

namespace sdapi.Controllers
{
    public class SensorImagesController : ApiController
    {
        private sdapiContext db = new sdapiContext();

        // GET: api/SensorImages
        public IQueryable<SensorImage> GetSensorImages()
        {
            return db.SensorImages;
        }

        // GET: api/SensorImages/5
        [ResponseType(typeof(SensorImage))]
        public async Task<IHttpActionResult> GetSensorImage(int id)
        {
            SensorImage sensorImage = await db.SensorImages.FindAsync(id);
            if (sensorImage == null)
            {
                return NotFound();
            }

            return Ok(sensorImage);
        }

        // GET: api/SensorImages/GetLastSensorImage
        [ResponseType(typeof(SensorImage))]
        public async Task<IHttpActionResult> GetLastSensorImage()
        {
            SensorImage image = db.SensorImages.OrderByDescending(p => p.SensorImageId).FirstOrDefault();
            if (image == null)
            {
                return NotFound();
            }

            return Ok(image);
        }


        // GET: api/SensorImages/GetLastSensorImage
        // TimeStamp = "01/08/2008 14:50:50.42" Must be this format

        [ResponseType(typeof(IQueryable<SensorImage>))]
        public async Task<IHttpActionResult> GetSensorByTime(string TimeStamp)
        {
            DateTime parameter_timestamp;
            if(TimeStamp == null)
            {
                return StatusCode(HttpStatusCode.BadRequest);

            }
            else
            {
                parameter_timestamp = Convert.ToDateTime(TimeStamp);
            }

            return Ok(db.SensorImages.Where(i => (Convert.ToDateTime(i.TimeStamp) > parameter_timestamp)));
        }

        // PUT: api/SensorImages/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSensorImage(int id, SensorImage sensorImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sensorImage.SensorImageId)
            {
                return BadRequest();
            }

            db.Entry(sensorImage).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SensorImageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/SensorImages
        [ResponseType(typeof(SensorImage))]
        public async Task<IHttpActionResult> PostSensorImage(SensorImage sensorImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SensorImages.Add(sensorImage);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = sensorImage.SensorImageId }, sensorImage);
        }

        // DELETE: api/SensorImages/5
        [ResponseType(typeof(SensorImage))]
        public async Task<IHttpActionResult> DeleteSensorImage(int id)
        {
            SensorImage sensorImage = await db.SensorImages.FindAsync(id);
            if (sensorImage == null)
            {
                return NotFound();
            }

            db.SensorImages.Remove(sensorImage);
            await db.SaveChangesAsync();

            return Ok(sensorImage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SensorImageExists(int id)
        {
            return db.SensorImages.Count(e => e.SensorImageId == id) > 0;
        }
    }
}