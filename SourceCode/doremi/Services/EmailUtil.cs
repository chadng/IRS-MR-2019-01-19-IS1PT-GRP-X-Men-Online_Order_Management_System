using doremi.DAL;
using doremi.Data;
using doremi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace doremi.Services
{
    public class EmailUtil
    {
        private readonly MyDbContext _context;
        private readonly IEmailSender _emailSender;

        private static EmailUtil _Instance;

        private EmailUtil()
        {


            _context = new MyDbContext();
            _emailSender = MyHttpApplication.MyEmailSender;


        }

        public static EmailUtil GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new EmailUtil();
            }

            return _Instance;
        }


        private int GetGroupIdByName(string GroupName)
        {
            Group group = _context.Group.FirstOrDefault(g => g.GroupName == GroupName);
            return group.GroupId;
        }

        private List<string> GetEmailListByGroupId(int GroupId)
        {
            List<string> emailList = new List<string>();
            var Items = _context.UserProfile.ToList();
            foreach (UserProfile profile in Items)
            {
                if (profile.GroupId == GroupId)
                {
                    emailList.Add(profile.Email);
                }
            }
            return emailList;
        }

        public void SendEmailToGroup(string GroupName, string Subject, string message)
        {
            int GroupId = GetGroupIdByName(GroupName);
            List<string> emailList = GetEmailListByGroupId(GroupId);
            foreach (string email in emailList)
            {
                _emailSender.SendEmailAsync(email, Subject, message);
            }
        }
    }
}