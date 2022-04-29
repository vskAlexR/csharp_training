﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase

    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData contact = new ContactData("aaab2");
            contact.LastName = "bbba2";
            contact.MobileNumber = "77782";

            app.Contacts.CreateIfContactNotExist(contact);

            ContactData newData = new ContactData("aaab");
            newData.LastName = "bbba";
            newData.MobileNumber = "7778";

            app.Contacts.Modify(2, newData);
        }
    }
}