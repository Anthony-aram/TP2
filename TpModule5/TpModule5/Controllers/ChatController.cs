using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TpModule5.Models;

namespace TpModule5.Controllers
{
    public class ChatController : Controller
    {
        private static List<Chat> chats;
        public List<Chat> lesChats => chats ?? (chats = Chat.GetMeuteDeChats());

        // GET: Chat
        public ActionResult Index()
        {
            return View(lesChats);
        }

        // GET: Chat/Details/5
        public ActionResult Details(int id)
        {
            Chat leChat = lesChats.FirstOrDefault(c => c.Id == id);
            return View(leChat);
        }

        // GET: Chat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Chat/Create
        [HttpPost]
        public ActionResult Create(Chat unChat)
        {
            try
            {
                foreach (Chat chat in lesChats)
                {
                    if(chat.Id == unChat.Id)
                    {
                        unChat.Id++;
                    }
                }
                lesChats.Add(unChat);
            }
            catch
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        // GET: Chat/Edit/5
        public ActionResult Edit(int id)
        {
            Chat leChat = lesChats.FirstOrDefault(c => c.Id == id);
            return View(leChat);
        }

        // POST: Chat/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Chat unChat)
        {
            try
            {
                for (int i = 0; i < lesChats.Count; i++)
                {
                    Chat leChat = lesChats[i];
                    if(leChat.Id == id)
                    {
                        lesChats[i] = unChat;
                    }
                }
            }
            catch
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        // GET: Chat/Delete/5
        public ActionResult Delete(int id)
        {
            Chat leChat = lesChats.FirstOrDefault(c => c.Id == id);
            if(leChat == null)
            {
                return RedirectToAction("Index");
            }
            return View(leChat);
        }

        // POST: Chat/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Chat leChat = lesChats.FirstOrDefault(c => c.Id == id);
                if (leChat != null)
                {
                    lesChats.Remove(leChat);
                }
            }
            catch
            {
                return View();
            }
            return RedirectToAction("Index");
        }
    }
}
