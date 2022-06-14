﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace addressbook_web_tests
{
    public class ContactHelper : HelperBase
    {

        public ContactHelper(ApplicationManager manager)
             : base(manager)
        { }

        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.GoToHomePage();
            InitContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            manager.Navigator.ReturnToHomePage();
            return this;
        }

        public ContactHelper Modify(int id, ContactData newData)
        {
            manager.Navigator.GoToHomePage();
            SelectContact(id);
            InitContactModification(id);
            FillContactForm(newData);
            SubmitContactModification();
            manager.Navigator.ReturnToHomePage();
            return this;
        }

        public ContactHelper Remove(int id)
        {
            manager.Navigator.GoToHomePage();
            SelectContact(id);
            RemoveContact();
            CloseContactAlert();
            manager.Navigator.OpenToHomePage();
            return this;
        }

        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.FirstName    );
            Type(By.Name("lastname"), contact.LastName);
            Type(By.Name("mobile"), contact.MobileNumber);
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[21]")).Click();
            return this;
        }
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public ContactHelper InitContactModification(int id)
        {
            driver.FindElement(By.XPath("//img[@alt='Edit']")).Click();
            return this;
        }

        public ContactHelper SelectContact(int id)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + id+1 + "]")).Click();
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }
        public ContactHelper CloseContactAlert()
        {
            driver.SwitchTo().Alert().Accept();
            return this;
        }
        public void CreateIfContactNotExist(ContactData contact)
        {
            if (!IsElementPresent(By.XPath("//img[@alt='Edit']")))
            {
                Create(contact);
            }
        }
        public bool IsContactExist(int id)
        {
            return IsElementPresent(By.XPath("//table[@id='maintable']/tbody/tr[" + ( id + 2 ) + "]/td/input"));
        }

        public List<ContactData> GetContactList()
        {
            List<ContactData> contacts = new List<ContactData>();
            manager.Navigator.OpenToHomePage();
            ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr"));
            foreach (IWebElement element in elements)
            {
                if (element.GetAttribute("name") == "entry")
                {
                    List<IWebElement> tds = element.FindElements(By.CssSelector("td")).ToList();
                    contacts.Add(new ContactData(tds[2].Text, tds[1].Text));
                }
            }
            return contacts;
        }
        public List<ContactData> GetContactsLists()
        {
            {
                List<ContactData> contact_list = new List<ContactData>();
                List<IWebElement> contacts = new List<IWebElement>();

                manager.Navigator.GoToHomePage();

                ICollection<IWebElement> records = driver.FindElements(By.Name("entry"));


                    foreach (IWebElement record in records)
                    {
                        contacts = record.FindElements((By.TagName("td"))).ToList();
                        contact_list.Add(new ContactData(contacts[2].Text, contacts[1].Text));
                    }

                return contact_list;
            }
        }

    }
}