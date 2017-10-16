using System;
using System.Collections.Generic;
using Journly.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Journly.Models;

namespace UnitTestProject1
{
    [TestClass]
    public class SearchFunctionalityTests
    {
        [TestMethod]
        public void SearchKeyPhrase_KeyPhraseInBody_ReturnCorrectJournal()
        {
            var testList = new List<JournalEntry>();

            var jrn1 = new JournalEntry{Title = "Some Title", EntryBody = "SearchString", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N};
            var jrn2 = new JournalEntry{Title = "Some Title2", EntryBody = "some other entry", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N};
            var jrn3 = new JournalEntry{Title = "Some Title3", EntryBody = "some other entry", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N};
            var jrn4 = new JournalEntry{Title = "Some Title4", EntryBody = "some other entry", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N};
            var jrn5 = new JournalEntry{Title = "Some Title5", EntryBody = "some other entry", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N};

            testList.Add(jrn1);
            testList.Add(jrn2);
            testList.Add(jrn3);
            testList.Add(jrn4);
            testList.Add(jrn5);

            var targetList = new List<JournalEntry>();
            targetList.Add(jrn1);

            JournalEntryController controller = new JournalEntryController();
            var resultsList = controller.FilterByString(testList, "SearchString");

            CollectionAssert.AreEqual(targetList, resultsList);
        }

        [TestMethod]
        public void SearchKeyPhrase_KeyPhraseInTitle_ReturnJournal()
        {
            var testList = new List<JournalEntry>();

            var jrn1 = new JournalEntry { Title = "SearchString", EntryBody = "Some Entry", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };
            var jrn2 = new JournalEntry { Title = "Some Title2", EntryBody = "some other entry", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };
            var jrn3 = new JournalEntry { Title = "Some Title3", EntryBody = "some other entry", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };
            var jrn4 = new JournalEntry { Title = "Some Title4", EntryBody = "some other entry", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };
            var jrn5 = new JournalEntry { Title = "Some Title5", EntryBody = "some other entry", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };

            testList.Add(jrn1);
            testList.Add(jrn2);
            testList.Add(jrn3);
            testList.Add(jrn4);
            testList.Add(jrn5);

            var targetList = new List<JournalEntry>();
            targetList.Add(jrn1);

            JournalEntryController controller = new JournalEntryController();
            var resultsList = controller.FilterByString(testList, "SearchString");

            CollectionAssert.AreEqual(targetList, resultsList);
        }

        [TestMethod]
        public void SearchKeyPhrase_KeyPhraseNotPresent_ReturnNull()
        {
            var testList = new List<JournalEntry>();

            var jrn1 = new JournalEntry { Title = "Some Title1", EntryBody = "some other entry1", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };
            var jrn2 = new JournalEntry { Title = "Some Title2", EntryBody = "some other entry2", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };
            var jrn3 = new JournalEntry { Title = "Some Title3", EntryBody = "some other entry3", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };
            var jrn4 = new JournalEntry { Title = "Some Title4", EntryBody = "some other entry4", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };
            var jrn5 = new JournalEntry { Title = "Some Title5", EntryBody = "some other entry5", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };

            testList.Add(jrn1);
            testList.Add(jrn2);
            testList.Add(jrn3);
            testList.Add(jrn4);
            testList.Add(jrn5);

            var targetList = new List<JournalEntry>();
            targetList.Add(jrn1);

            JournalEntryController controller = new JournalEntryController();
            var resultsList = controller.FilterByString(testList, "SearchString");

            Assert.AreEqual(0, resultsList.Count);
        }

        [TestMethod]
        public void SearchByDateRange_JournalInRangePresent_ReturnCorrectJournal()
        {
            var testList = new List<JournalEntry>();

            var jrn1 = new JournalEntry { Title = "Some Title1", EntryBody = "some other entry1", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };
            var jrn2 = new JournalEntry { Title = "Some Title2", EntryBody = "some other entry2", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };
            var jrn3 = new JournalEntry { Title = "Some Title3", EntryBody = "some other entry3", CreatedOn = new DateTime(2015, 10, 21), Flag = JournalEntry.EntryFlag.N };
            var jrn4 = new JournalEntry { Title = "Some Title4", EntryBody = "some other entry4", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };
            var jrn5 = new JournalEntry { Title = "Some Title5", EntryBody = "some other entry5", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };

            testList.Add(jrn1);
            testList.Add(jrn2);
            testList.Add(jrn3);
            testList.Add(jrn4);
            testList.Add(jrn5);

            var targetList = new List<JournalEntry>();
            targetList.Add(jrn3);

            JournalEntryController controller = new JournalEntryController();
            var resultsList = controller.FilterByDateRange(testList, new DateTime(2015, 10, 5), new DateTime(2015, 11, 21) );

            CollectionAssert.AreEqual(targetList, resultsList);
        }

        [TestMethod]
        public void SearchByDateRange_JournalsOutOfRange_ReturnNoJournals()
        {
            var testList = new List<JournalEntry>();

            var jrn1 = new JournalEntry { Title = "Some Title1", EntryBody = "some other entry1", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };
            var jrn2 = new JournalEntry { Title = "Some Title2", EntryBody = "some other entry2", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };
            var jrn3 = new JournalEntry { Title = "Some Title3", EntryBody = "some other entry3", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };
            var jrn4 = new JournalEntry { Title = "Some Title4", EntryBody = "some other entry4", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };
            var jrn5 = new JournalEntry { Title = "Some Title5", EntryBody = "some other entry5", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };

            testList.Add(jrn1);
            testList.Add(jrn2);
            testList.Add(jrn3);
            testList.Add(jrn4);
            testList.Add(jrn5);

            var targetList = new List<JournalEntry>();
            targetList.Add(jrn3);

            JournalEntryController controller = new JournalEntryController();
            var resultsList = controller.FilterByDateRange(testList, new DateTime(2015, 10, 5), new DateTime(2015, 11, 21));

            Assert.AreEqual(0, resultsList.Count);
        }

        [TestMethod]
        public void SearchByDate_JournalOnDateExists_ReturnsJournalOnDate()
        {
            var testList = new List<JournalEntry>();

            var jrn1 = new JournalEntry { Title = "Some Title1", EntryBody = "some other entry1", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };
            var jrn2 = new JournalEntry { Title = "Some Title2", EntryBody = "some other entry2", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };
            var jrn3 = new JournalEntry { Title = "Some Title3", EntryBody = "some other entry3", CreatedOn = new DateTime(2015, 10, 21), Flag = JournalEntry.EntryFlag.N };
            var jrn4 = new JournalEntry { Title = "Some Title4", EntryBody = "some other entry4", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };
            var jrn5 = new JournalEntry { Title = "Some Title5", EntryBody = "some other entry5", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };

            testList.Add(jrn1);
            testList.Add(jrn2);
            testList.Add(jrn3);
            testList.Add(jrn4);
            testList.Add(jrn5);

            var targetList = new List<JournalEntry>();
            targetList.Add(jrn3);

            JournalEntryController controller = new JournalEntryController();
            var resultsList = controller.FilterByDate(testList, new DateTime(2015, 10, 21));

            CollectionAssert.AreEqual(targetList, resultsList);
        }

        [TestMethod]
        public void SearchByJournalId_JournalHasEntries_EntriesWithJournalIdAreReturned()
        {
            var testList = new List<JournalEntry>();

            var jrn1 = new JournalEntry {JournalId = 1,Title = "Some Title1", EntryBody = "some other entry1", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };
            var jrn2 = new JournalEntry {JournalId = 2, Title = "Some Title2", EntryBody = "some other entry2", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };
            var jrn3 = new JournalEntry {JournalId = 1, Title = "Some Title3", EntryBody = "some other entry3", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };
            var jrn4 = new JournalEntry {JournalId = 4, Title = "Some Title4", EntryBody = "some other entry4", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };
            var jrn5 = new JournalEntry {JournalId = 4, Title = "Some Title5", EntryBody = "some other entry5", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };

            testList.Add(jrn1);
            testList.Add(jrn2);
            testList.Add(jrn3);
            testList.Add(jrn4);
            testList.Add(jrn5);

            var targetList = new List<JournalEntry>();
            targetList.Add(jrn1);
            targetList.Add(jrn3);

            JournalEntryController controller = new JournalEntryController();
            var resultsList = controller.FilterByJournal(testList, 1);

            CollectionAssert.AreEqual(targetList, resultsList);
        }

        [TestMethod]
        public void SearchByJournalId_JournalHasNoEntries_NoEntriesAreReturned()
        {
            var testList = new List<JournalEntry>();

            var jrn1 = new JournalEntry { JournalId = 2, Title = "Some Title1", EntryBody = "some other entry1", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };
            var jrn2 = new JournalEntry { JournalId = 2, Title = "Some Title2", EntryBody = "some other entry2", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };
            var jrn3 = new JournalEntry { JournalId = 3, Title = "Some Title3", EntryBody = "some other entry3", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };
            var jrn4 = new JournalEntry { JournalId = 4, Title = "Some Title4", EntryBody = "some other entry4", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };
            var jrn5 = new JournalEntry { JournalId = 4, Title = "Some Title5", EntryBody = "some other entry5", CreatedOn = DateTime.Now, Flag = JournalEntry.EntryFlag.N };

            testList.Add(jrn1);
            testList.Add(jrn2);
            testList.Add(jrn3);
            testList.Add(jrn4);
            testList.Add(jrn5);

            var targetList = new List<JournalEntry>();

            JournalEntryController controller = new JournalEntryController();
            var resultsList = controller.FilterByJournal(testList, 1);

            CollectionAssert.AreEqual(targetList, resultsList);
        }
    }
}
