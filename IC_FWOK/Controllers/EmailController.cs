using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IC_FWOK.Controllers
{
    public class EmailController : Controller
    {
        private static List<Email> emails = new List<Email>
        {
            new Email { Id = 1, DateTime = DateTime.Now, Sender = "John Doe", Subject = "Meeting Reminder", Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum." },
            new Email { Id = 2, DateTime = DateTime.Now.AddMinutes(-30), Sender = "Jane Smith", Subject = "Project Update", Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum." },
            // Add more sample emails as needed
        };

        private static List<Directory> directories = new List<Directory>();

        public IActionResult Inbox()
        {
            foreach (var email in emails)
            {
                email.Preview = GeneratePreview(email.Body);
            }
            return View(emails);
        }
        public IActionResult Sent()
        {
            var sentEmails = GetSentEmails(); // Assuming you have a method to get sent emails
            return View(sentEmails);
        }

        private List<Email> GetSentEmails()
        {
            // Implement logic to retrieve sent emails
            return new List<Email>
    {
        new Email { Id = 3, DateTime = DateTime.Now.AddHours(-1), Sender = "John Doe", Subject = "Follow Up", Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum." }
        // Add more sample sent emails as needed
    };
        }


        public IActionResult ViewEmail(int id)
        {
            var email = emails.FirstOrDefault(e => e.Id == id);
            if (email == null)
            {
                return NotFound();
            }
            return View(email);
        }

        [HttpPost]
        public IActionResult Reply(string reply)
        {
            // Handle the reply logic here
            return RedirectToAction("Inbox");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var email = emails.FirstOrDefault(e => e.Id == id);
            if (email != null)
            {
                emails.Remove(email);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public IActionResult Compose()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Compose(string to, string subject, string body)
        {
            // Handle the compose logic here
            // For example, save the email to the database or send it
            return RedirectToAction("Inbox");
        }

        [HttpPost]
        public IActionResult SaveToFavorite(int id)
        {
            var email = emails.FirstOrDefault(e => e.Id == id);
            if (email != null)
            {
                email.IsFavorite = true;
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public IActionResult Directory()
        {
            return View(directories);
        }

        [HttpPost]
        public IActionResult CreateDirectory(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                directories.Add(new Directory { Name = name });
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult UploadFile(int directoryId, IFormFile file)
        {
            var directory = directories.FirstOrDefault(d => d.Id == directoryId);
            if (directory != null && file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    var newFile = new File
                    {
                        Name = file.FileName,
                        Content = memoryStream.ToArray()
                    };
                    directory.AddFile(newFile);
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        private string GeneratePreview(string body)
        {
            if (string.IsNullOrEmpty(body))
            {
                return string.Empty;
            }

            const int previewLength = 100; // Adjust the length as needed
            if (body.Length <= previewLength)
            {
                return body;
            }

            return body.Substring(0, previewLength) + "...";
        }
    }

    public class Email
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; } // Add this property
        public string Subject { get; set; }
        public string Preview { get; set; }
        public string Body { get; set; }
        public bool IsFavorite { get; set; }
    }


    public class Directory
    {
        public int Id { get; set; } // Add this property
        public string Name { get; set; }
        public List<File> Files { get; set; } = new List<File>();

        public void AddFile(File file)
        {
            Files.Add(file);
        }
    }

    public class File
    {
        public string Name { get; set; }
        public byte[] Content { get; set; }
    }
}
