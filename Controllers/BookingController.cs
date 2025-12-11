using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RailwayManagement.Models;
using System.Data.Entity;
using RailwayManagement.Models;
using System.Windows.Forms;
public class BookingController : Controller
{
    private readonly RailwayDBContext db = new RailwayDBContext();

    [HttpGet]
    public ActionResult Book(int? id)
    {
        if (!id.HasValue)
        {
            return new HttpStatusCodeResult(400, "Train ID is required.");
        }

        var train = db.Trains.FirstOrDefault(t => t.TrainId == id.Value);
        if (train == null)
        {
            return HttpNotFound("Train not found.");
        }

        var model = new Bookings
        {
            TrainId = train.TrainId,
            Train = train,
            Passengers = new List<Passengers>() { new Passengers() }
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Book(Bookings model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }

            var train = db.Trains.FirstOrDefault(t => t.TrainId == model.TrainId);
            if (train != null)
            {
                model.Train = train;
            }
            return View(model);
        }

        try
        {
            model.Pnrnumber = Guid.NewGuid().ToString().Substring(0, 10).ToUpper();
            model.BookingDate = DateTime.Now;

            db.Bookings.Add(model);
            db.SaveChanges();

            return RedirectToAction("BookingConfirmation", new { pnr = model.Pnrnumber });
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error saving booking: " + ex.Message);
            return View(model);
        }
    }

    [HttpGet]
    public ActionResult BookingConfirmation(string pnr)
    {
        if (string.IsNullOrEmpty(pnr))
        {
            return HttpNotFound("PNR is missing.");
        }

        var booking = db.Bookings
            .Include(b => b.Passengers)
            .FirstOrDefault(b => b.Pnrnumber == pnr);

        if (booking == null)
        {
            return HttpNotFound("Booking not found.");
        }

        return View(booking);
    }
}
