using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using auction.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace auction.Controllers
{
    public class AuctionController : Controller
    {
        private AuctionContext _context;

        public AuctionController(AuctionContext context)
        {
            _context = context;
        }
        

        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Item> Items = _context.Items.OrderBy(i => i.EndDate).Include(a => a.Auctions).ThenInclude(u => u.User).ToList();
            ViewBag.Items = Items;
            ViewBag.UserId = (int)HttpContext.Session.GetInt32("id");
            ViewBag.User = _context.Users.SingleOrDefault(u => u.UserId == (int)HttpContext.Session.GetInt32("id"));
            return View("Dashboard");
        }

        [HttpGet]
        [Route("create")]
        public IActionResult CreateAuction()
        {
            if (HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Create");
        }

        [HttpPost]
        [Route("process")]
        public IActionResult Process(RegItem NewItem)
        {
            if(ModelState.IsValid)
            {
                Item item = new Item
                {
                    Name = NewItem.Name,
                    StartBid = NewItem.StartBid,
                    EndDate = Convert.ToDateTime(NewItem.EndDate),
                    Description = NewItem.Description,
                    Creator = _context.Users.SingleOrDefault(u => u.UserId == HttpContext.Session.GetInt32("id")),
                    HighestBid = NewItem.StartBid,
                    HighestBidder = _context.Users.SingleOrDefault(u => u.UserId == HttpContext.Session.GetInt32("id")),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.Items.Add(item);
                _context.SaveChanges();
                Auction auction = new Auction
                {
                    UserId = (int)HttpContext.Session.GetInt32("id"),
                    ItemId = item.ItemId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.Auctions.Add(auction);
                _context.SaveChanges();
                return RedirectToAction("Dashboard", "Auction");
            }
            return View("Create");
        }

        [HttpGet]
        [Route("delete/{iteId}")]
        public IActionResult Delete(int iteId)
        {
            Item iteToDel = _context.Items.SingleOrDefault(i => i.ItemId == iteId);
            _context.Items.Remove(iteToDel);
            _context.SaveChanges();
            return RedirectToAction("Dashboard", "Auction");
        }
        [HttpPost]
        [Route("/item/bid/{iteId}")]
        public IActionResult Bid(double bid, int iteId)
        {
            Item CurrItem = _context.Items.SingleOrDefault(i => i.ItemId == iteId);
            if (CurrItem.HighestBid >= bid)
            {
                ViewBag.BidError = "Bid needs to be higher than current highest";
                return RedirectToAction("ItemInfo", new { iteId = iteId });
            }
            CurrItem.HighestBidder.Balance += CurrItem.HighestBid;
            _context.SaveChanges();
            CurrItem.HighestBid = bid;
            _context.SaveChanges();
            User curr = _context.Users.SingleOrDefault(u => u.UserId == HttpContext.Session.GetInt32("id"));
            CurrItem.HighestBidder = curr;
            _context.SaveChanges();
            curr.Balance -= bid;
            _context.SaveChanges();
            Auction auction = new Auction
                {
                    UserId = (int)HttpContext.Session.GetInt32("id"),
                    ItemId = iteId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
            _context.Auctions.Add(auction);
            _context.SaveChanges();
            return RedirectToAction("Dashboard", "Auction");
        }

        [HttpGet]
        [Route("item/{iteId}")]
        public IActionResult ItemInfo(int iteId)
        {
            Item TheItem = _context.Items.Include(a => a.Auctions).ThenInclude(u => u.User).SingleOrDefault(i => i.ItemId == iteId);
            ViewBag.Item = TheItem;
            ViewBag.UserId = (int)HttpContext.Session.GetInt32("id");
            return View("Item");
        }
    }
}