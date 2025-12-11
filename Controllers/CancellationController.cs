using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RailwayManagement.Models;

namespace RailwayManagement.Controllers
{
    public class CancellationController : Controller
    {
        private readonly RailwayDBContext db = new RailwayDBContext();

        // GET: Cancellation
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CancelTicket(string pnr)
        {
            if (string.IsNullOrEmpty(pnr))
            {
                return HttpNotFound("PNR is missing.");
            }

            var booking = db.Bookings
                .Include(b => b.Train)
                .FirstOrDefault(b => b.Pnrnumber == pnr);

            if (booking == null)
            {
                return HttpNotFound("Booking not found.");
            }

            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmCancellation(string pnr)
        {
            if (string.IsNullOrEmpty(pnr))
            {
                return HttpNotFound("PNR is missing.");
            }

            var booking = db.Bookings
                .Include(b => b.Train)
                .FirstOrDefault(b => b.Pnrnumber == pnr);

            if (booking == null)
            {
                return HttpNotFound("Booking not found.");
            }

            /*if (booking.CancellationStatus)
            {
                TempData["ErrorMessage"] = "This ticket has already been cancelled.";
                return RedirectToAction("CancelTicket", new { pnr = pnr });
            }*/

            if (booking.JourneyDate < DateTime.Now)
            {
                TempData["ErrorMessage"] = "Cannot cancel ticket for past journeys.";
                return RedirectToAction("CancelTicket", new { pnr = pnr });
            }

            decimal cancellationFee = CalculateCancellationFee(booking.TotalAmount, booking.JourneyDate);
            decimal refundAmount = booking.TotalAmount - cancellationFee;

            try
            {
                var CancellationStatus = true;
                booking.BookingStatus = false;
                db.SaveChanges();

                TempData["SuccessMessage"] = $"Ticket cancelled successfully. Refund amount: ₹{refundAmount:F2}";
                return RedirectToAction("BookingConfirmation", "Booking", new { pnr = pnr });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while cancelling the ticket.";
                return RedirectToAction("CancelTicket", new { pnr = pnr });
            }
        }

        private decimal CalculateCancellationFee(decimal totalAmount, DateTime journeyDate)
        {
            var timeBeforeJourney = journeyDate - DateTime.Now;
            decimal cancellationFee = 0;

            if (timeBeforeJourney.TotalHours <= 24)
            {
                cancellationFee = totalAmount * 0.5m;
            }
            else if (timeBeforeJourney.TotalHours <= 48)
            {
                cancellationFee = totalAmount * 0.25m;
            }
            else
            {
                cancellationFee = totalAmount * 0.1m;
            }

            return Math.Round(cancellationFee, 2);
        }
    }
}